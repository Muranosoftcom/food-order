using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Contexts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SpreadsheetIntegration.Google;

namespace SampleMvcApp
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
                                db.Users.AddAsync(user);
                                db.SaveChanges();
                            }
                            
                            var claim = new Claim("userId", user.Id.ToString());
                            var claimIdentity =  new ClaimsIdentity();
                            claimIdentity.AddClaim(claim);
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

            services.AddSingleton<IGoogleSpreadsheetProvider, GoogleSpreadsheetProvider>();
            services.AddScoped<FoodOrderContext>();
            services.AddScoped<IRepository, FoodOrderRepository>();
            
            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

//			services.AddSpaStaticFiles(configuration =>
//			{
//				configuration.RootPath = "ClientApp/build";
//			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
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
            });

//            app.UseSpa(spa =>
//			{
//				spa.Options.SourcePath = "ClientApp";
//
//				if (env.IsDevelopment())
//				{
//					spa.UseReactDevelopmentServer(npmScript: "start");
//				}
//			});
        }
    }
}
