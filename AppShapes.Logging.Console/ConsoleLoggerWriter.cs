namespace AppShapes.Logging.Console
{
    public class ConsoleLoggerWriter : IConsoleLoggerWriter
    {
        public virtual void Flush()
        {
        }

        public virtual void Write(string message)
        {
            System.Console.Write(message);
        }

        public virtual void WriteLine(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}