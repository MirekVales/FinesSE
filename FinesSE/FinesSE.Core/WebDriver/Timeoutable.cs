using System;

namespace FinesSE.Core.WebDriver
{
    public class Timeoutable : IDisposable
    {
        public DateTime Started { get; }
        public TimeSpan Timeout { get; }

        public bool Timeouted
            => DateTime.Now - Started > Timeout;

        public Timeoutable(TimeSpan timeout)
        {
            Started = DateTime.Now;
            Timeout = timeout;
        }

        public Timeoutable(TimeSpan? timeout)
        {
            Started = DateTime.Now;
            Timeout = timeout ?? TimeSpan.Zero;
        }

        public void Dispose() { }
    }
}
