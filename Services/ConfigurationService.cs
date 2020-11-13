using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NorthwindCorp.Services
{
  public class ConfigurationService
  {
    private ILogger<ConfigurationService> _logger;
    private IConfiguration _configuration;

    public ConfigurationService(ILogger<ConfigurationService> logger, IConfiguration configuration)
    {
      _configuration = configuration;
      _logger = logger;
    }
    public T GetValue<T>(string param)
    {
      var result =  _configuration.GetValue<T>(param);
      _logger.Log(LogLevel.Information, $"read: '{param}' = {result}");
      return result;
    }

    public string GetConnectionString(string param)
    {
      var result = _configuration.GetConnectionString(param);
      _logger.Log(LogLevel.Information, $"read connection string: '{param}' = {result}");
      return result;
    }
  }
}
