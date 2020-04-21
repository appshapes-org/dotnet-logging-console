using System;

namespace Logging.Console
{
    public interface IConsoleLoggerProcessor : IDisposable
    {
        void Process(string message);
    }
}