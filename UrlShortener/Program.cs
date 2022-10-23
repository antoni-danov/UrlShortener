using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using UrlShortener.ActionFilters;
using UrlShortener.Models;
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

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            builder.Services.AddDbContext<ApplicationDbContext>(connection =>
            {
                connection.UseSqlServer(builder.Configuration.GetConnectionString("default"));
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
