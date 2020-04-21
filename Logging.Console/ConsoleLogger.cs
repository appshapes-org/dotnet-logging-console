using System;
using Microsoft.Extensions.Logging;

namespace Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger(string name, IConsoleLoggerProcessor processor, IConsoleLoggerFormatter formatter)
        {
            Name = name;
            Processor = processor;
            Formatter = formatter;
        }

        public virtual IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public virtual bool IsEnabled(LogLevel level)
        {
            return true;
        }

        public virtual void Log<TState>(LogLevel level, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(level))
                return;
            string message = FormatMessage(state, exception, formatter);
            if ((message == "[null]" || string.IsNullOrWhiteSpace(message)) && exception == null)
                return;
            Processor.Process(Formatter.GetMessage(Name, level, message, exception));
        }

        protected virtual string FormatMessage<TState>(TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            return formatter == null ? FormatState(state, exception) : formatter(state, exception);
        }

        protected virtual string FormatState<TState>(TState state, Exception _)
        {
            return $"{state}";
        }

        private IConsoleLoggerFormatter Formatter { get; }

        private string Name { get; }

        private IConsoleLoggerProcessor Processor { get; }
    }
}