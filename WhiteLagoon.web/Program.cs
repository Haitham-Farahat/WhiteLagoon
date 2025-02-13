using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Syncfusion.Licensing;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Application.Services.Implementation;
using WhiteLagoon.Application.Services.Interface;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Infrastructure.Repository;
using static System.Formats.Asn1.AsnWriter;

namespace WhiteLagoon.web
{
    public class Program
    {
        public static object StripeConfigratioin { get; private set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
             options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.AccessDeniedPath = "/Account/AccessDenied";
                option.LogoutPath = "/Account/Login";
            });
            builder.Services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 6;
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            var app = builder.Build();
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Secretkey").Get<string>();

            SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetSection("Syncfusion:LicenseKey").Get<string>());
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            SeedDatabase();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
            void SeedDatabase()
            {
                using (var scope = app.Services.CreateScope()) { 
                   
                   var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    dbInitializer.Initialize();
                } 
            }
        }
    }
}
