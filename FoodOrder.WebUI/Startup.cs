using System;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using FoodOrder.Persistence;
using FoodOrder.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FoodOrder.SpreadsheetIntegration;
using FoodOrder.SpreadsheetIntegration.Google;
using FoodOrder.WebUI.App;
using Swashbuckle.AspNetCore.Swagger;

namespace FoodOrder.WebUI {
    public class Startup {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var authOptions = new AuthenticationSettings(Configuration.GetSection("AuthenticationSettings"));
            IDatabaseConfig dbConfig = new DatabaseConfig(Configuration.GetSection("FoodOrderDatabase"));
            var stringBuilder = new PostgresDbConnectionStringBuilder(dbConfig);
            var dbContextConnectionBuilder = new PostgresDbContextConnectionBuilder(stringBuilder);
            services.AddDbContext<FoodOrderDbContext>(options => 
                FoodOrderDbContext.ConfigureDbContextOptionsBuilder(options, dbContextConnectionBuilder));

            
            
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
            services.AddScoped<FoodOrderDbContext>();
            services.AddScoped<IFoodOrderDbContext, FoodOrderDbContext>();
            services.AddScoped<IRepository, FoodOrderRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<ICalendarService, CalendarService>();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FoodOrderDbContext dbContext) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
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
            
            dbContext.Database.EnsureCreated();
        }
    }
}