using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NorthwindCorp.Core.Repository.Data;
using NorthwindCorp.Core.Repository.Services;
using NorthwindCorp.Core.Repository.Services.Interfaces;
using NorthwindCorp.Core.Services;
using NorthwindCorp.Core.Services.Interfaces;
using NorthwindCorp.Web.Services;

namespace NorthwindCorp.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddSwaggerGen();

      services.AddDbContext<NorthwindContext>();

      services.AddControllersWithViews();

      services.AddSingleton<IConfigurationService, ConfigurationService>();
      services.AddTransient<IFormattingService, FormattingService>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<ISupplierService, SupplierService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseStatusCodePages();

      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthwindCorp API V1");
      });


      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
