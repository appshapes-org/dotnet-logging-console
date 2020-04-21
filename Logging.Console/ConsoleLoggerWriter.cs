using System;

namespace Logging.Console
{
    public class ConsoleLoggerWriter : IConsoleLoggerWriter
    {
        public virtual void Flush()
        {
        }

        public virtual void Write(string message, ConsoleColor? background, ConsoleColor? foreground)
        {
            System.Console.Write(message);
        }

        public virtual void WriteLine(string message, ConsoleColor? background, ConsoleColor? foreground)
        {
            System.Console.WriteLine(message);
        }
    }
}