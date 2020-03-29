using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerFormatterTests
    {
        [Fact]
        public void GetMessageMustLogExceptionWhenExceptionIsNotNull()
        {
            DateTime now = DateTime.UtcNow;
            StubConsoleLoggerFormatter formatter = new StubConsoleLoggerFormatter(now);
            string message = formatter.GetMessage(nameof(ConsoleLoggerFormatterTests), LogLevel.Information, "Test", new Exception("Test"));
            Assert.Matches($@"(?s)^{now:O} Information {nameof(ConsoleLoggerFormatterTests)} \[.*\] Test{Environment.NewLine}System.Exception: Test.*$", message);
        }

        [Fact]
        public void GetMessageMustNotLogExceptionWhenExceptionIsNull()
        {
            DateTime now = DateTime.UtcNow;
            StubConsoleLoggerFormatter formatter = new StubConsoleLoggerFormatter(now);
            string message = formatter.GetMessage(nameof(ConsoleLoggerFormatterTests), LogLevel.Information, "Test", null);
            Assert.Matches($@"(?s)^{now:O} Information {nameof(ConsoleLoggerFormatterTests)} \[.*\] Test$", message);
        }

        [Fact]
        public void GetThreadIdMustReturnManagedThreadIdWhenThreadNameIsNullOrWhitespace()
        {
            Task.Run(() => { Assert.Matches(@"^\d*$", new StubConsoleLoggerFormatter().InvokeGetThreadId()); }).Wait();
        }

        [Fact]
        public void GetThreadIdMustReturnThreadNameWhenThreadNameIsNotEmpty()
        {
            bool result = false;
            Thread thread = new Thread(() =>
            {
                try
                {
                    string threadId = new StubConsoleLoggerFormatter().InvokeGetThreadId();
                    result = threadId == "Test";
                }
                catch
                {
                    result = false;
                }
            }) {IsBackground = true, Name = "Test"};
            thread.Start();
            thread.Join();
            Assert.True(result);
        }

        private class StubConsoleLoggerFormatter : ConsoleLoggerFormatter
        {
            public StubConsoleLoggerFormatter() : this(DateTime.UtcNow)
            {
            }

            public StubConsoleLoggerFormatter(DateTime dateTimeNow)
            {
                DateTimeNow = dateTimeNow;
            }

            public string InvokeGetThreadId()
            {
                return GetThreadId();
            }

            protected override DateTime GetDateTimeNow()
            {
                base.GetDateTimeNow();
                return DateTimeNow;
            }

            private DateTime DateTimeNow { get; }
        }
    }
}