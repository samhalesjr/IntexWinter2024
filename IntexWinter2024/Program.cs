using IntexWinter2024.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace IntexWinter2024
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<IntexWinter2024Context>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:AzureConnection"]);
            });

            builder.Services.AddScoped<IIntexWinter2024Repository, EFIntexWinter2024Repository>();

            builder.Services.AddRazorPages();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Added for user and session management
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IntexWinter2024Context>()
                .AddDefaultTokenProviders();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout settings
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // GDPR cookie policy
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            // Added for user and session management
            app.UseAuthentication();
            app.UseSession();


            app.MapControllerRoute("pagination", "/Browse/{pageNum}", new { Controller = "Home", Action = "Browse", pageNum = 1 });

            // this will improve the browse page's URLs
            app.MapControllerRoute(
                name: "productCategoryAndColor",
                pattern: "/Browse/{productCategory}/{primaryColor}/{pageNum}",
                defaults: new { controller = "Home", action = "Browse", pageNum = 1 }
            );

            app.MapControllerRoute(
                name: "productCategory",
                pattern: "/Browse/{productCategory}/{pageNum}",
                defaults: new { controller = "Home", action = "Browse", pageNum = 1 }
            );

            app.MapControllerRoute(
                name: "primaryColor",
                pattern: "/Browse/{primaryColor}/{pageNum}",
                defaults: new { controller = "Home", action = "Browse", pageNum = 1 }
            );

            app.MapDefaultControllerRoute();

            app.MapRazorPages();
            // Cookie policy
            app.UseCookiePolicy();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy",
                    "default-src 'self'; " +
                    "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://www.google.com https://www.gstatic.com https://www.recaptcha.net; " +
                    "style-src 'self' 'unsafe-inline' https://www.google.com https://www.gstatic.com https://www.recaptcha.net; " +
                    "img-src 'self' https://m.media-amazon.com https://images.brickset.com https://www.lego.com https://www.brickeconomy.com https://www.gstatic.com https://www.google.com https://media.tenor.com; " +
                    "frame-src 'self' https://www.google.com https://www.recaptcha.net; " +
                    "connect-src 'self' https://www.google.com https://recaptcha.google.com https://www.recaptcha.net; " +
                    "font-src 'self' https://fonts.gstatic.com; " +
                    "object-src 'none';");
                await next();
            });





            // url routes for product details
            app.MapControllerRoute("productDetails", "/ProductDetails/{productId?}",
                new { Controller = "Home", Action = "ProductDetails" }, new { productId = @"\d+" });

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
