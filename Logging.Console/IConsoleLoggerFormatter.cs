using System;
using Microsoft.Extensions.Logging;

namespace Logging.Console
{
    public interface IConsoleLoggerFormatter
    {
        string GetMessage(string logger, LogLevel level, string message, Exception exception);
    }
}