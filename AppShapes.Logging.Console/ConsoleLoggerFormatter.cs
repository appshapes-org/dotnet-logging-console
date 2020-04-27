using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace AppShapes.Logging.Console
{
    public class ConsoleLoggerFormatter : IConsoleLoggerFormatter
    {
        public string GetMessage(string logger, LogLevel level, string message, Exception exception)
        {
            return $"{GetDateTimeNow():O} {level} {logger} [{GetThreadId()}] {message}{(exception == null ? string.Empty : $"{Environment.NewLine}{exception}")}";
        }

        protected virtual DateTime GetDateTimeNow()
        {
            return DateTime.UtcNow;
        }

        protected virtual string GetThreadId()
        {
            return string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) ? $"{Thread.CurrentThread.ManagedThreadId}" : Thread.CurrentThread.Name;
        }
    }
}