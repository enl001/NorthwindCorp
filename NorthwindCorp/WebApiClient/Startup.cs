using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApiClient
{

  public class Startup
  {
    //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      //services.AddCors(options =>
      //{
      //  options.AddPolicy(name: MyAllowSpecificOrigins,
      //    builder =>
      //    {
      //      builder.WithOrigins("https://localhost:44385", "https://localhost:44319");
      //    });
      //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      app.UseHttpsRedirection();
      app.UseDefaultFiles();
      app.UseStaticFiles();
      //app.UseCors(MyAllowSpecificOrigins);
    }
  }
}
