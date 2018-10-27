using System;
using Domain.Contexts;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Security.Claims;
using BusinessLogic.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using SpreadsheetIntegration.Google;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.SpaServices.Webpack;
using SpreadsheetIntegration;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => HostingEnvironment.IsProduction();
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.AddDbContext<FoodOrderContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FoodOrderDatabase")));

            // Add authentication services
            services.AddAuthentication(o =>
                {
                    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    googleOptions.SaveTokens = true;
                    googleOptions.CallbackPath = "/login";
                    googleOptions.Events = new OAuthEvents
                    {
                        OnTicketReceived = async ctx =>
                        {
                            // Relate to local users
                            string email = ctx.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                            var userName = ctx.Principal.Identity.Name;
                            var db = ctx.HttpContext.RequestServices.GetRequiredService<FoodOrderContext>();
                            User user = await db.Users.FirstOrDefaultAsync(u => string.Compare(u.Email, email, StringComparison.CurrentCultureIgnoreCase) == 0);
                            if (user == null)
                            {
                                user = new User { Email = email, FirstName = userName };
                                await db.Users.AddAsync(user);
                                db.SaveChanges();
                            }
                            
                            var claim = new Claim("userId", user.Id.ToString());
                            var isAdminClaim = new Claim("isAdmin", user.IsAdmin.ToString());
                            var claimIdentity =  new ClaimsIdentity();
                            claimIdentity.AddClaim(claim);
                            claimIdentity.AddClaim(isAdminClaim);
                            ctx.Principal.AddIdentity(claimIdentity);
                        } 
                    };
                })
                .AddCookie(o =>
                {
                    o.LoginPath = "/login";
                    o.LogoutPath = "/logout";
                    o.ExpireTimeSpan = TimeSpan.FromHours(2);
                });

            services.AddSingleton<IAsyncSpreadsheetProvider, GoogleSpreadsheetProvider>();
            services.AddScoped<FoodOrderContext>();
            services.AddScoped<IRepository, FoodOrderRepository>();
            services.AddScoped<IFoodService, FoodService>();
            
            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com" }
                });
            });
//			 services.AddSpaStaticFiles(configuration =>
//			{
//				configuration.RootPath = "ClientApp/build";
//			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					ProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp"),
					HotModuleReplacement = true
				});
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
//            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
