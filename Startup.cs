using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MVCCoreApplication.Context;
using MVCCoreApplication.Logging;
using MVCCoreApplication.MiddleWare;
using MVCCoreApplication.Repository;
using MVCCoreApplication.Services;
using System;
using System.Text;

namespace MVCCoreApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection ITokenGeneratorServiceCollection { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string localSqlConnectionStr =Configuration.GetConnectionString("localSqlConnectionStr");
  
            services.AddControllersWithViews();
            services.AddSession();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddSingleton<UserLogger>();
            services.AddDbContext<UserDbContext>(u => u.UseSqlServer(localSqlConnectionStr));
            ValidateTokenWithParameters(services, Configuration);
            void ValidateTokenWithParameters(IServiceCollection services, IConfiguration configuration)
            {
                var userSecretKey = configuration["JwtValidationDetails:UserApplicationSecretKey"];
                var userIssuer = configuration["JwtValidationDetails:UserIssuer"];
                var userAudience = configuration["JwtValidationDetails:UserAudience"];
                var userSymmetricSecuritKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSecretKey));
                var tokenValidationPrameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = userIssuer,

                    ValidateAudience = true,
                    ValidAudience = userAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = userSymmetricSecuritKey,
                    ValidateLifetime = true

                };
            
            services.AddAuthentication(u =>
            {
                u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(u => u.TokenValidationParameters = tokenValidationPrameters);
        }
            
            
        }

        private void ValidateTokenWithParameters(IServiceCollection serviceCollection, IServiceCollection services, IConfiguration configuration1, object configuration2)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                var tokenInfo = context.Session.GetString("userToken");
                context.Request.Headers.Add("Authorization", "Bearer" + tokenInfo);
                await next();

            });
                
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseDateTimeMiddleware();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=GetAllUsers}/{id?}");
            });
        }
    }
}
