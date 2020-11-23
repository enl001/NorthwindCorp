using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Web.Services.Interfaces;

namespace NorthwindCorp.Web.Filters
{
  public class TimeLoggingFilter : IActionFilter
  {
    private readonly ILogger<TimeLoggingFilter> _logger;
    private readonly Stopwatch _stopwatch;
    private readonly bool _writeToLog;
    private IConfigurationService _configurationService;

    public TimeLoggingFilter(ILogger<TimeLoggingFilter> logger, IConfigurationService configurationService)
    {
      _configurationService = configurationService;
      _logger = logger;
      _stopwatch = new Stopwatch();
      _writeToLog = _configurationService.GetValue<bool>("IsLoggingControllerActions");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (_writeToLog)
      {
        _stopwatch.Start();
        _logger.LogInformation($"----> START Action: {context.ActionDescriptor.DisplayName}, Path: {context.HttpContext.Request.Path}");
      }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (_writeToLog)
      {
        _stopwatch.Stop();
        var time = _stopwatch.ElapsedMilliseconds;
        _logger.LogInformation($"----> STOP Action: {context.ActionDescriptor.DisplayName}, Path: {context.HttpContext.Request.Path}, duration: {time}ms");
      }
    }
  }
}
