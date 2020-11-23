using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NorthwindCorp.Web.Services.Interfaces;

namespace NorthwindCorp.Web.Middleware
{
  public class ImageCacheMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IConfigurationService _configurationService;
    private readonly ILogger<ImageCacheMiddleware> _logger;
    private readonly Dictionary<string, string> _imagePathDictionary;
    private Timer _timer;
    private readonly int _timeSpan;
    private readonly object folderLock = new object();

    public ImageCacheMiddleware(
      RequestDelegate next,
      IConfigurationService configurationService,
      ILogger<ImageCacheMiddleware> logger)
    {
      _next = next;
      _configurationService = configurationService;
      _logger = logger;
      _imagePathDictionary = new Dictionary<string, string>();
      _timeSpan = _configurationService.GetValue<int>("CacheTime");
      SetupTimer();
    }

    private void SetupTimer()
    {
      if (_timer != null)
      {
        _timer.Stop();
        _timer.Elapsed -= ClearCache;
        _timer.Dispose();
      }

      _timer = new Timer(_timeSpan * 1000);
      _timer.Elapsed += ClearCache;
      _timer.AutoReset = true;
      _timer.Enabled = true;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      Stream originalBody = context.Response.Body;

      var requestPath = context.Request.Path;
      var returnFile = false;
      var imageData = new byte[] { };

      lock (folderLock)
      {
        if (_imagePathDictionary.ContainsKey(requestPath))
        {
          var path = _imagePathDictionary[requestPath];
          imageData = File.ReadAllBytes(path);
          returnFile = true;
        }
      }

      if (returnFile)
      {
        try
        {
          using (var stream = new MemoryStream())
          {
            stream.Write(imageData, 0, imageData.Length);
            stream.Position = 0;
            await stream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
            
          }
        }
        catch (Exception ex)
        {
          var message = ex.Message;
        }

        SetupTimer();
        return;
      }

      var cahcePath = _configurationService.GetValue<string>("CachePath");

      try
      {
        using (var memStream = new MemoryStream())
        {
          context.Response.Body = memStream;

          await _next(context);

          if (context.Response.ContentType == "image/jpg")
          {
            memStream.Position = 0;
            var image = memStream.ToArray();
            if (!Directory.Exists(cahcePath))
            {
              Directory.CreateDirectory(cahcePath);
            }

            var path = Path.Combine(cahcePath, $"{Guid.NewGuid()}.jpg");

            lock (folderLock)
            {
              if (_imagePathDictionary.Count < _configurationService.GetValue<int>("CacheImageCount"))
              {
                var success = _imagePathDictionary.TryAdd(context.Request.Path.Value, path);
                if (success)
                {
                  File.WriteAllBytes(path, image);
                }
              }
            }
          }

          memStream.Position = 0;
          await memStream.CopyToAsync(originalBody);
        }
      }
      catch (Exception ex)
      {
        var message = ex.Message;
        _logger.LogError(ex, ex.Message);
      }
      finally
      {
        context.Response.Body = originalBody;
      }
    }

    private void ClearCache(Object source, ElapsedEventArgs e)
    {
      var cahcePath = _configurationService.GetValue<string>("CachePath");
      if (!Directory.Exists(cahcePath)) return;

      lock (folderLock)
      {
        _imagePathDictionary.Clear();
        foreach (var file in Directory.GetFiles(cahcePath))
        {
          File.Delete(file);
        }
      }
    }
  }
}