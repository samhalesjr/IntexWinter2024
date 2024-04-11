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
            
            // this will improve the browse page's URLs
            app.MapControllerRoute("pageNumAndType", "/Browse/{productCategory}/{pageNum}", new { Controller = "Home", Action = "Browse" });
            app.MapControllerRoute("pagination", "/Browse/{pageNum}", new {Controller = "Home", Action = "Browse", pageNum = 1});
            app.MapControllerRoute("productCategory", "/Browse/{productCategory}", new { Controller = "Home", Action = "Browse", pageNum = 1 });
            
            app.MapDefaultControllerRoute();

            app.MapRazorPages();

            app.Run();
        }
    }
}
