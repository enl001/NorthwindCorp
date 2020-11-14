using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NorthwindCorp.Web.Services
{
  public class ConfigurationService : IConfigurationService
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
      var result = _configuration.GetValue<T>(param);
      _logger.Log(LogLevel.Information, $"read: '{param}' = {result}");
      return result;
    }
  }
}
