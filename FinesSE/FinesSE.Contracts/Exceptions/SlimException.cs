using System;

namespace FinesSE.Contracts.Exceptions
{
    public abstract class SlimException : Exception, IDisposable
    {
        public SlimException(string message)
            : base($"message:<<{message}>>")
        { }

        public void Dispose()
        {
            throw this;
        }
    }
}
