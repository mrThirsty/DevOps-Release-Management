using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ReleaseManagement.Framework.Data.Model;
using ReleaseManagement.Framework.Interfaces;

namespace ReleaseManagement.Framework.Logging
{
    public class ReleaseManagementLogger : IRMLogger
    {
        public ReleaseManagementLogger(ILogDataService service, IHttpContextAccessor context)
        {
            _service = service;
            _httpContext = context;
        }

        private readonly ILogDataService _service;
        private readonly IHttpContextAccessor _httpContext;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log(string area, LogLevel logLevel, Exception exception, string message)
        {
            try
            {
                string user = "system", userId = "0";

                if(_httpContext.HttpContext != null)
                {
                    var objectId = _httpContext.HttpContext.User.FindFirst(ReleaseConstants.Security.Claims.UserId);
                    user = _httpContext.HttpContext.User.Identity.Name;
                    userId = objectId != null? objectId.Value : "-1";
                }

                Log logItem = new Log()
                {
                    Level = logLevel.ToString(),
                    Category = area,
                    Message = $"{message}: {exception.StackTrace }",
                    User = user,
                    UserId = userId,
                    Timestamp = DateTime.Now
                };

                _service.Save(logItem);
            }catch(Exception ex)
            {

            }
        }

        public void Log(string area, LogLevel logLevel, string message)
        {
            try
            {
                string user = "system", userId = "0";

                if(_httpContext.HttpContext != null)
                {
                    var objectId = _httpContext.HttpContext.User.FindFirst(ReleaseConstants.Security.Claims.UserId);
                    user = _httpContext.HttpContext.User.Identity.Name;
                    userId = objectId != null? objectId.Value : "-1";
                }

                Log logItem = new Log()
                {
                    Level = logLevel.ToString(),
                    Category = area,
                    Message = message,
                    User = user,
                    UserId = userId,
                    Timestamp = DateTime.Now
                };

                    _service.Save(logItem);
            }catch(Exception ex)
            {

            }
        }

        public void LogError(string area, Exception ex, string msg)
        {
            Log(area, LogLevel.Error, ex, msg);
        }
    }
}