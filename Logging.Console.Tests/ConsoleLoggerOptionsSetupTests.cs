using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Logging.Console.Tests
{
    public class ConsoleLoggerOptionsSetupTests
    {
        [Fact]
        public void ConstructorMustSetConfigurationActionWhenConfigurationIsNotNull()
        {
            ILoggerProviderConfiguration<ConsoleLoggerManager> config = new LoggerProviderConfigurationStub<ConsoleLoggerManager>(new FakeConfiguration());
            Assert.NotNull(new ConsoleLoggerOptionsSetup(config).Action);
        }

        [Fact]
        public void ConstructorMustThrowExceptionWhenProviderConfigurationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ConsoleLoggerOptionsSetup(null));
        }

        private class FakeConfiguration : IConfiguration
        {
            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public IConfigurationSection GetSection(string key)
            {
                throw new NotImplementedException();
            }

            public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }

        private class LoggerProviderConfigurationStub<T> : ILoggerProviderConfiguration<T>
        {
            public LoggerProviderConfigurationStub(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }
        }
    }
}