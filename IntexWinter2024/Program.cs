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

            app.MapRazorPages();
            // Cookie policy
            app.UseCookiePolicy();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy",
                    "default-src 'self'; " +
                    "script-src 'self'; " +
                    "style-src 'self' 'unsafe-inline'; " +
                    "img-src 'self' m.media-amazon.com images.brickset.com www.lego.com www.brickeconomy.com;");
                await next();
            });

            // url routes for product details
            app.MapControllerRoute("productDetails", "/ProductDetails/{productId?}",
                new { Controller = "Home", Action = "ProductDetails" }, new { productId = @"\d+" });

            app.MapControllerRoute("cart", "/Cart/{subtotal}",
                new { Controller = "Order", Action = "Checkout" });
            
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
