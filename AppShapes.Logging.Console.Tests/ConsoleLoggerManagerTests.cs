using System;
using Microsoft.Extensions.Options;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerManagerTests
    {
        [Fact]
        public void CreateLoggerMustReturnConsoleLogger()
        {
            using (ConsoleLoggerManager consoleLoggerManager = new ConsoleLoggerManager(new MockOptionsMonitor<ConsoleLoggerOptions>(new ConsoleLoggerOptions())))
            {
                Assert.NotNull(consoleLoggerManager.CreateLogger("Test"));
            }
        }

        private class MockOptionsMonitor<T> : IOptionsMonitor<T>
        {
            public MockOptionsMonitor(T value)
            {
                CurrentValue = value;
            }

            public T CurrentValue { get; }

            public T Get(string name)
            {
                return CurrentValue;
            }

            public IDisposable OnChange(Action<T, string> listener)
            {
                return null;
            }
        }
    }
}