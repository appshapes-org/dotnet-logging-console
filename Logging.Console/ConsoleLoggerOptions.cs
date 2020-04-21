using Microsoft.Extensions.Logging;

namespace Logging.Console
{
    public class ConsoleLoggerOptions
    {
        public virtual LogLevel LogLevel { get; set; } = LogLevel.None;

        public override string ToString()
        {
            return $"{nameof(LogLevel)}: {LogLevel}";
        }
    }
}