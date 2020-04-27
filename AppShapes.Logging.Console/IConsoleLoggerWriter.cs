namespace AppShapes.Logging.Console
{
    public interface IConsoleLoggerWriter
    {
        void Flush();

        void Write(string message);

        void WriteLine(string message);
    }
}