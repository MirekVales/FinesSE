using System;

namespace FinesSE.Contracts.Exceptions
{
    public abstract class SlimException : Exception, IDisposable
    {
        public string InnerMessage { get; }

        public SlimException(string message)
            : base($"message:<<{message}>>")
            => InnerMessage = message;

        public void Dispose()
        {
            throw this;
        }
    }
}