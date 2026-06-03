using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using simu2.Data;

namespace simu2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default")
                );
            });

            // Identity
            builder.Services
                .AddIdentityCore<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;

                    // Password
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;

                    // User
                    options.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            // MVC
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";

                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Error page
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Static files
            app.UseStaticFiles();

            // Routing
            app.UseRouting();

            // Auth
            app.UseAuthentication();
            app.UseAuthorization();

            // Areas
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );

            // Default
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}