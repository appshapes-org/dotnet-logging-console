using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace Logging.Console
{
    public class ConsoleLoggerProcessor : IConsoleLoggerProcessor
    {
        public ConsoleLoggerProcessor() : this(new ConsoleLoggerWriter())
        {
        }

        public ConsoleLoggerProcessor(IConsoleLoggerWriter console)
        {
            Console = console ?? throw new ArgumentNullException(nameof(console), $"{nameof(console)} cannot be null");
            ProcessorThread = GetProcessorThread();
            StartProcessorThread(ProcessorThread);
        }

        public void Dispose()
        {
            try
            {
                TerminateQueuing();
                TerminateProcessing();
            }
            catch (ThreadStateException ex)
            {
                HandleThreadStateException(ex);
            }
        }

        public virtual void Process(string message)
        {
            if (IsQueueEnabled)
            {
                Enqueue(message);
                return;
            }

            WriteMessage(message);
        }

        protected virtual void AddToQueue(string message)
        {
            Queue.Add(message);
        }

        protected virtual void DisableQueue()
        {
            try
            {
                Trace.TraceInformation($"{nameof(DisableQueue)} called");
                TerminateQueuing();
            }
            catch (Exception ex)
            {
                Trace.TraceInformation($"[{nameof(DisableQueue)}] {ex.Message}");
            }
        }

        protected virtual void Enqueue(string message)
        {
            try
            {
                AddToQueue(message);
            }
            catch (InvalidOperationException)
            {
            }
        }

        protected virtual Thread GetProcessorThread()
        {
            return new Thread(ProcessMessages) {IsBackground = true, Name = "ConsoleLoggerProcessor"};
        }

        protected virtual void HandleThreadStateException(ThreadStateException exception)
        {
        }

        protected virtual bool IsQueueEnabled => !Queue.IsAddingCompleted;

        protected virtual void ProcessMessages()
        {
            try
            {
                WriteMessages();
            }
            catch
            {
                DisableQueue();
            }
        }

        protected Thread ProcessorThread { get; set; }

        protected BlockingCollection<string> Queue { get; } = new BlockingCollection<string>(1024);

        protected virtual void StartProcessorThread(Thread processorThread)
        {
            processorThread.Start();
        }

        protected virtual void TerminateProcessing()
        {
            ProcessorThread.Join(1500);
        }

        protected virtual void TerminateQueuing()
        {
            Queue.CompleteAdding();
        }

        protected virtual void WriteMessage(string message)
        {
            Console.WriteLine(message, null, null);
        }

        protected virtual void WriteMessages()
        {
            foreach (string message in Queue.GetConsumingEnumerable())
                WriteMessage(message);
        }

        private IConsoleLoggerWriter Console { get; }
    }
}