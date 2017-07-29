using FinesSE.Launcher.Formats;
using System;

namespace FinesSE.Launcher.Infrastructure
{
    public class UnsupportedFormatException : Exception
    {
        public UnsupportedFormatException(TableFormat format) 
            : base($"Format '{format}' is not supported")
        {
        }
    }
}
