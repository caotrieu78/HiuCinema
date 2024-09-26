using BookingCinema.Models.MoMo;
using BookingCinema.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using BookingCinema.Services;
using BookingCinema.Services;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();

        // Configure MoMo options
        services.Configure<MomoOptionModel>(Configuration.GetSection("MomoAPI"));

       
        // Register MoMo service
        services.AddScoped<IMomoService, MomoService>();

        // Add HttpContextAccessor
        services.AddHttpContextAccessor();

        // Configure cookie authentication
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "YourCookieName";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login"; // Corrected login path
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie expiration time
            });

        // Require authorization for Admin folder
        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/Admin");
        });

        // Add authorization
        services.AddAuthorization();

        // Register DAO services
        services.AddScoped<UserDAO>();
        services.AddScoped<MovieDAO>();

        // Add session
        services.AddSession();

        // Configure Entity Framework DbContext
        services.AddDbContext<CinemaContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("Cinema"));
        });
        services.AddSingleton<IVnPayService, VnPayService>();

        // Register BookingService

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Admin",
                pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            endpoints.MapRazorPages();

            endpoints.MapControllerRoute(
                name: "SelectSeat",
                pattern: "/Shared/SelectSeat", // Razor Page or Controller Action path
                defaults: new { page = "/Shared/SelectSeat" });
        });
    }
}
