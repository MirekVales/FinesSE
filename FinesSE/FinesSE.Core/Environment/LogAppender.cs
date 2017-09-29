using FinesSE.Contracts.Infrastructure;
using log4net.Appender;
using log4net.Core;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.Linq;
using log4net.Layout;

namespace FinesSE.Core.Environment
{
    public class LogAppender : AppenderSkeleton, IDisposable
    {
        public string LogPath => configuration.LogPath;

        readonly CoreConfiguration configuration;
        readonly PatternLayout patternLayout;

        readonly Queue<string> messages = new Queue<string>();
        DateTime lastChange;
        readonly object @lock = new object();

        public const int MaxBufferSize = 100;

        public LogAppender(IConfigurationProvider provider)
        {
            configuration = provider.Get(CoreConfiguration.Default);
            patternLayout = new PatternLayout(configuration.LogPattern);

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
                messages.Enqueue(patternLayout.Format(loggingEvent));

                lastChange = DateTime.Now;

                Monitor.Pulse(@lock);
            }
        }

        void Writer()
        {
            while (!disposedValue)
            {
                lock (@lock)
                {
                    if (messages.Count >= MaxBufferSize)
                        WriteToFile();

                    Monitor.Wait(@lock);
                }
            }

            if (messages.Any())
                WriteToFile();
        }

        void WriteToFile()
        {
            string[] messagesToWriter = null;

            lock (@lock)
            {
                messagesToWriter = messages.ToArray();
                messages.Clear();
            }

            File.AppendAllLines(LogPath, messagesToWriter);
        }

        bool disposedValue = false;

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
