using System;
using Microsoft.Extensions.Logging;

namespace ReleaseManagement.Framework.Interfaces
{
    public interface IRMLogger
    {
        void Log(string area, LogLevel logLevel, string message);
        void Log(string area, LogLevel logLevel, Exception exception, string message);
        void LogError(string area, Exception ex, string msg);
    }
}
