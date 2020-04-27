using System;
using Microsoft.Extensions.Logging;

namespace AppShapes.Logging.Console
{
    public interface IConsoleLoggerFormatter
    {
        string GetMessage(string logger, LogLevel level, string message, Exception exception);
    }
}