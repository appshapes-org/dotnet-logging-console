using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppShapes.Logging.Console
{
    [ProviderAlias("Console")]
    public class ConsoleLoggerManager : ILoggerProvider
    {
        public ConsoleLoggerManager(IOptionsMonitor<ConsoleLoggerOptions> options) : this(options, new ConsoleLoggerProcessor())
        {
        }

        public ConsoleLoggerManager(IOptionsMonitor<ConsoleLoggerOptions> options, IConsoleLoggerProcessor processor) : this(options, processor, new ConsoleLoggerFormatter())
        {
        }

        public ConsoleLoggerManager(IOptionsMonitor<ConsoleLoggerOptions> options, IConsoleLoggerProcessor processor, IConsoleLoggerFormatter formatter)
        {
            Settings = options.CurrentValue;
            Processor = processor;
            Formatter = formatter;
        }

        public virtual ILogger CreateLogger(string name)
        {
            return Loggers.GetOrAdd(name, GetLogger);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
                Processor.Dispose();
        }

        protected virtual ILogger GetLogger(string name)
        {
            return new ConsoleLogger(name, Processor, Formatter);
        }

        protected ConcurrentDictionary<string, ILogger> Loggers { get; } = new ConcurrentDictionary<string, ILogger>();

        protected ConsoleLoggerOptions Settings { get; }

        private IConsoleLoggerFormatter Formatter { get; }

        private IConsoleLoggerProcessor Processor { get; }
    }
}