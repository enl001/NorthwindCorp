using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindCorp.Web.Areas.Identity.Data;
using NorthwindCorp.Web.Data;

[assembly: HostingStartup(typeof(NorthwindCorp.Web.Areas.Identity.IdentityHostingStartup))]
namespace NorthwindCorp.Web.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
  {
    public void Configure(IWebHostBuilder builder)
    {
      builder.ConfigureServices((context, services) =>
      {
        services.AddDbContext<NorthwindCorpWebIdentityContext>(options =>
            options.UseSqlServer(
                context.Configuration.GetConnectionString("NorthwindCorpWebIdentityContextConnection")));

        services.AddDefaultIdentity<NorthwindCorpWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<NorthwindCorpWebIdentityContext>();

        services.Configure<IdentityOptions>(options =>
        {
                // Password settings.
                options.Password.RequireDigit = true;
          options.Password.RequireLowercase = true;
          options.Password.RequireNonAlphanumeric = true;
          options.Password.RequireUppercase = true;
          options.Password.RequiredLength = 6;
          options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
          options.Lockout.MaxFailedAccessAttempts = 10;
          options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
          "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
          options.User.RequireUniqueEmail = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
                // Cookie settings
                options.Cookie.HttpOnly = true;
          options.ExpireTimeSpan = TimeSpan.FromMinutes(10);

          options.LoginPath = "/Identity/Account/Login";
          options.AccessDeniedPath = "/Identity/Account/AccessDenied";
          options.SlidingExpiration = true;
        });
      });
    }
  }
}