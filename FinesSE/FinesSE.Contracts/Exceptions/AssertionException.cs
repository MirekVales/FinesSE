using System;

namespace FinesSE.Contracts.Exceptions
{
    public class AssertionException : Exception
    {
        public AssertionException(string expected, string actual, WebDrivers drivers)
            : base()
        { }
    }
}
