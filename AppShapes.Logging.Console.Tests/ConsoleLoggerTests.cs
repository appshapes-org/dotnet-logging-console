using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerTests
    {
        [Fact]
        public void BeginScopeMustReturnNull()
        {
            Assert.Null(new ConsoleLogger("Test", null, new ConsoleLoggerFormatter()).BeginScope("Test"));
        }

        [Fact]
        public void FormatMessageMustReturnMessageFromFormatterWhenFormatterIsNotNull()
        {
            Assert.Equal("TEST", new StubConsoleLogger("Test", null).InvokeFormatMessage("Test", null, (x, _) => x.ToUpperInvariant()));
        }

        [Fact]
        public void IsEnabledMustReturnTrue()
        {
            Assert.True(new ConsoleLogger("Test", null, new ConsoleLoggerFormatter()).IsEnabled(LogLevel.None));
        }

        [Fact]
        public void LogMustNotProcessMessageWhenIsEnabledReturnsFalse()
        {
            ConsoleLoggerProcessorStub processor = new ConsoleLoggerProcessorStub();
            new StubConsoleLogger("Test", processor, false).Log(LogLevel.Information, 0, "Test", null, null);
            Assert.Empty(processor.Messages);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void LogMustNotProcessMessageWhenMessageIsNullOrWhitespaceAndExceptionIsNull(string state)
        {
            ConsoleLoggerProcessorStub processor = new ConsoleLoggerProcessorStub();
            new ConsoleLogger("Test", processor, new ConsoleLoggerFormatter()).Log(LogLevel.Information, 0, state, null, null);
            Assert.Empty(processor.Messages);
        }

        [Fact]
        public void LogMustProcessMessageWhenExceptionIsNotNull()
        {
            ConsoleLoggerProcessorStub processor = new ConsoleLoggerProcessorStub();
            new ConsoleLogger("Test", processor, new ConsoleLoggerFormatter()).Log(LogLevel.Information, 0, "", new Exception("Exception"), null);
            Assert.Contains(processor.Messages, x => Regex.IsMatch(x, @"Information Test \[\d*\].*Exception", RegexOptions.Singleline));
        }

        [Fact]
        public void LogMustProcessMessageWhenMessageOrExceptionIsNotNullOrEmpty()
        {
            ConsoleLoggerProcessorStub processor = new ConsoleLoggerProcessorStub();
            new ConsoleLogger("Test", processor, new ConsoleLoggerFormatter()).Log(LogLevel.Information, 0, "Test", null, null);
            Assert.Contains(processor.Messages, x => Regex.IsMatch(x, @"Information Test \[\d*\] Test"));
        }

        private class ConsoleLoggerProcessorStub : ConsoleLoggerProcessor
        {
            public List<string> Messages { get; } = new List<string>();

            protected override Thread GetProcessorThread()
            {
                return Thread.CurrentThread;
            }

            protected override void StartProcessorThread(Thread processorThread)
            {
                DisableQueue();
            }

            protected override void WriteMessage(string message)
            {
                Messages.Add(message);
            }
        }

        private class StubConsoleLogger : ConsoleLogger
        {
            private bool itsIsEnabled;

            public StubConsoleLogger(string name, IConsoleLoggerProcessor processor, bool isEnabled = true) : base(name, processor, new ConsoleLoggerFormatter())
            {
                itsIsEnabled = isEnabled;
            }

            public string InvokeFormatMessage<T>(T state, Exception exception, Func<T, Exception, string> formatter)
            {
                return FormatMessage(state, exception, formatter);
            }

            public override bool IsEnabled(LogLevel level)
            {
                return base.IsEnabled(level) && itsIsEnabled;
            }
        }
    }
}