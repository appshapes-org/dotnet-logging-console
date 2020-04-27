using Microsoft.Extensions.Logging;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerOptionsTests
    {
        [Fact]
        public void LogLevelMustReturnExpectedLogLevelWhenSet()
        {
            Assert.Equal(LogLevel.Information, new ConsoleLoggerOptions {LogLevel = LogLevel.Information}.LogLevel);
        }

        [Fact]
        public void LogLevelMustReturnLogLevelNoneWhenNotSet()
        {
            Assert.Equal(LogLevel.None, new ConsoleLoggerOptions().LogLevel);
        }

        [Fact]
        public void ToStringMustReturnExpectedValueWhenCalled()
        {
            Assert.Equal($"{nameof(ConsoleLoggerOptions.LogLevel)}: {LogLevel.Information}", $"{new ConsoleLoggerOptions {LogLevel = LogLevel.Information}}");
        }
    }
}