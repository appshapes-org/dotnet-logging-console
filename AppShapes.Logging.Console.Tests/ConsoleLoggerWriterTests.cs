using System;
using System.IO;
using System.Text;
using Xunit;

namespace AppShapes.Logging.Console.Tests
{
    public class ConsoleLoggerWriterTests
    {
        [Fact]
        public void WriteMustWriteMessagesToStandardOut()
        {
            TextWriter standardOut = System.Console.Out;
            try
            {
                StringBuilder messages = new StringBuilder();
                System.Console.SetOut(new StringWriter(messages));
                new ConsoleLoggerWriter().Write("Test", null, null);
                new ConsoleLoggerWriter().WriteLine(string.Empty, null, null);
                new ConsoleLoggerWriter().Flush();
                Assert.Contains($"Test{Environment.NewLine}", $"{messages}");
            }
            finally
            {
                System.Console.SetOut(standardOut);
            }
        }
    }
}