using System;

namespace FinesSE.Launcher.Infrastructure
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException(string message) : base(message)
        {
        }
    }
}