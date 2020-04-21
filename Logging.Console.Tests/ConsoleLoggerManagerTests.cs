using System;
using Microsoft.Extensions.Options;
using Xunit;

namespace Logging.Console.Tests
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

        [Fact]
        public void DisposeMustDisposeProcessor()
        {
            MockConsoleLoggerProcessor processor = new MockConsoleLoggerProcessor();
            Assert.Equal(0, processor.DisposeCalled);
            new ConsoleLoggerManager(new MockOptionsMonitor<ConsoleLoggerOptions>(new ConsoleLoggerOptions()), processor).Dispose();
            Assert.Equal(1, processor.DisposeCalled);
        }

        [Fact]
        public void FinalizerMustNotDisposeProcessor()
        {
            MockConsoleLoggerProcessor processor = new MockConsoleLoggerProcessor();
            Assert.Equal(0, processor.DisposeCalled);
            new MockConsoleLoggerManager(new MockOptionsMonitor<ConsoleLoggerOptions>(new ConsoleLoggerOptions()), processor).InvokeFinalize();
            Assert.Equal(0, processor.DisposeCalled);
        }

        public class MockConsoleLoggerManager : ConsoleLoggerManager
        {
            public MockConsoleLoggerManager(IOptionsMonitor<ConsoleLoggerOptions> options, IConsoleLoggerProcessor processor) : base(options, processor)
            {
            }

            public void InvokeFinalize()
            {
                Dispose(false);
            }
        }

        private class MockConsoleLoggerProcessor : IConsoleLoggerProcessor
        {
            public void Dispose()
            {
                ++DisposeCalled;
            }

            public int DisposeCalled { get; private set; }

            public void Process(string message)
            {
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