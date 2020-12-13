using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Services;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Web.Areas.Identity.Data;
using NorthwindCorp.Web.Filters;
using NorthwindCorp.Web.Middleware;
using NorthwindCorp.Web.Services;
using NorthwindCorp.Web.Services.Interfaces;
using System.IO;

namespace NorthwindCorp.Web
{
  public class Startup
  {
    readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1",
          new OpenApiInfo
          {
            Title = "My API - V1",
            Version = "v1"
          }
        );

        var filePath = Path.Combine(System.AppContext.BaseDirectory, "NorthwindCorp.Web.xml");
        c.IncludeXmlComments(filePath);
      });

      services.AddDbContext<NorthwindContext>();

      services.AddCors(options =>
      {
        options.AddPolicy(name: MyAllowSpecificOrigins,
          builder =>
          {
            builder.WithOrigins("https://localhost:44385", "https://localhost:44319");
          });
      });

      services.AddControllersWithViews(options =>
      {
        options.Filters.Add(typeof(TimeLoggingFilter));
      });
      services.AddRazorPages();

      services.AddSingleton<IConfigurationService, ConfigurationService>();
      services.AddTransient<FormattingService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISupplierService, SupplierService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
      IApplicationBuilder app,
      IWebHostEnvironment env,
      IHostApplicationLifetime lifetime,
      UserManager<NorthwindCorpWebUser> userManager,
      RoleManager<IdentityRole> roleManager)
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
      app.UseCors(MyAllowSpecificOrigins);

      lifetime.ApplicationStopped.Register(ClearCache);

      NorthwindCorpDefaultData.Seed(userManager, roleManager);

      app.UseWhen(context => context.Request.Path.StartsWithSegments("/categories"), appBuilder =>
      {
        appBuilder.UseMiddleware<ImageCacheMiddleware>();
      });

      app.UseStatusCodePages();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthwindCorp API V1");
      });

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "image",
          pattern: "image/{id}",
          defaults: new { controller = "Categories", action = "GetImage" });
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }

    private void ClearCache()
    {
      var cahcePath = Configuration.GetValue<string>("CachePath");
      if (Directory.Exists(cahcePath))
      {

        foreach (var file in Directory.GetFiles(cahcePath))
        {
          File.Delete(file);
        }

      }
    }
  }
}
