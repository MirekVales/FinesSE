using FinesSE.Contracts.Infrastructure;
using log4net.Appender;
using log4net.Core;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace FinesSE.Core.Environment
{
    public class LogAppender : AppenderSkeleton, IDisposable
    {
        public string LogPath => configuration.LogPath;

        readonly CoreConfiguration configuration;
        readonly Queue<string> messages = new Queue<string>();
        DateTime lastChange;
        readonly object @lock = new object();

        readonly int MaxBufferSize = 100;

        public LogAppender(IConfigurationProvider provider)
        {
            configuration = provider.Get(CoreConfiguration.Default);
            
            if (!File.Exists(LogPath))
                File.Create(LogPath).Dispose();

            Task.Run(() => Writer());
        }

        override protected void Append(LoggingEvent loggingEvent)
            => Add(loggingEvent);

        void Add(LoggingEvent loggingEvent)
        {
            lock (@lock)
            {
                messages.Enqueue(loggingEvent.RenderedMessage);

                lastChange = DateTime.Now;

                Monitor.Pulse(@lock);
            }
        }

        void Writer()
        {
            lock (@lock)
            {
                while (!disposedValue)
                {
                    if (messages.Count >= MaxBufferSize)
                        WriteToFile();

                    Monitor.Wait(@lock);
                }

                if (messages.Any())
                    WriteToFile();
            }
        }

        void WriteToFile()
        {
            string[] messagesToWriter = null;
            messagesToWriter = messages.ToArray();
            messages.Clear();

            File.AppendAllLines(LogPath, messagesToWriter);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;

                lock (@lock)
                    Monitor.Pulse(@lock);
            }
        }

        public void Dispose()
            => Dispose(true);
    }
}
