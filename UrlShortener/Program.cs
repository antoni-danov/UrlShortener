using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
using UrlShortener.Models.JwtFeatures;
using UrlShortener.Services;
using UrlShortener.Services.UserService;

namespace UrlShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var customCorsPolicy = "CorsPolicy";
            var builder = WebApplication.CreateBuilder(args);
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var googleAuth = builder.Configuration.GetSection("Authentication:Google");

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            })
            .AddGoogle("google", options =>
            {
                options.ClientId = googleAuth["ClientId"];
                options.ClientSecret = googleAuth["ClientSecret"];
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });

            builder.Services.AddDbContext<ApplicationDbContext>(connection =>
            {
                //connection.UseSqlServer(builder.Configuration.GetConnectionString("default"));
                connection.UseSqlServer(builder.Configuration.GetConnectionString("azure_database"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                  customCorsPolicy,
                  policy => policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });
            builder.Services.AddScoped<IShortService, ShortService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<JwtHandler>();
            builder.Services.AddScoped<ValidationFiltersAttribute>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseStaticFiles();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(customCorsPolicy);
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "url",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller}/{action}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (app.Environment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            app.Run();
        }
    }
}
