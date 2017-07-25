using System;

namespace FinesSE.Launcher
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException(string message) : base(message)
        {
        }
    }
}