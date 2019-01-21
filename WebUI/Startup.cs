using System;
using Domain.Contexts;
using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpreadsheetIntegration.Google;
using Swashbuckle.AspNetCore.Swagger;
using SpreadsheetIntegration;
using WebUI.Infrastructure;
using WebUI.Models;

namespace WebUI {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            AuthenticationSettings authOptions = new AuthenticationSettings(Configuration.GetSection("AuthenticationSettings"));
            
            string connectionString = FoodOrderContext.GetConnectionString(
                Configuration["FoodOrderDatabase:DatabaseName"],
                Configuration["FoodOrderDatabase:UserId"],
                Configuration["FoodOrderDatabase:Password"],
                Configuration["FoodOrderDatabase:ServerName"]
            );
            
            services.AddDbContext<FoodOrderContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
                options.ClaimsIdentity.UserIdClaimType = "id";
                options.ClaimsIdentity.UserNameClaimType = "fullName";
            });
            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = authOptions.JwtIssuer,
                        ValidAudience = authOptions.JwtAudience,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            services.AddSingleton<IAsyncSpreadsheetProvider, GoogleSpreadsheetProvider>();
            services.AddScoped<FoodOrderContext>();
            services.AddScoped<IRepository, FoodOrderRepository>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddSingleton(authOptions);
            services.AddScoped<UserManager>();

            // Add framework services.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact {Name = "Talking Dotnet", Email = "contact@talkingdotnet.com", Url = "www.talkingdotnet.com"}
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller}/{action}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}