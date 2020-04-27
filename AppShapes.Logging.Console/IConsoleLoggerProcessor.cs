using System;

namespace AppShapes.Logging.Console
{
    public interface IConsoleLoggerProcessor : IDisposable
    {
        void Process(string message);
    }
}