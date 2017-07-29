using System;

namespace FinesSE.Launcher.Infrastructure
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message) : base(message)
        {
        }
    }
}
