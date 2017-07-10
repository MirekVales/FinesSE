using System;

namespace FinesSE.Contracts.Exceptions
{
    public abstract class SlimException : Exception
    {
        public SlimException(string message)
            : base($"message:<<{message}>>")
        { }
    }
}
