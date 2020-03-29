using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerExtensionsTests
    {
        [Fact]
        public void AddConsoleLoggerMustAddConsoleLoggerSingletons()
        {
            IServiceCollection services = new ServiceCollection();
            LoggingBuilderStub builder = new LoggingBuilderStub(services);
            Assert.Empty(services);
            builder.AddConsoleLogger();
            Assert.Contains(services, x => x.ServiceType == typeof(ILoggerProviderConfigurationFactory));
            Assert.Contains(services, x => x.ImplementationType == typeof(ConsoleLoggerManager));
            Assert.Contains(services, x => x.ImplementationType == typeof(ConsoleLoggerOptionsSetup));
            Assert.Contains(services, x => x.ImplementationType == typeof(LoggerProviderOptionsChangeTokenSource<ConsoleLoggerOptions, ConsoleLoggerManager>));
        }

        private class LoggingBuilderStub : ILoggingBuilder
        {
            public LoggingBuilderStub(IServiceCollection services)
            {
                Services = services;
            }

            public IServiceCollection Services { get; }
        }
    }
}