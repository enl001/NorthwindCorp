using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace NorthwindCorp.Web
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

      try
      {
        var builder = CreateHostBuilder(args).Build();
        var env = builder.Services.GetService(typeof(IWebHostEnvironment));
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        Log.Information("Starting up application");
        Log.Information($"Base directory: {baseDir}");
        builder.Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Application start-up failed");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
          .UseSerilog()
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
          });
  }
}
