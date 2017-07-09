using System;

namespace FinesSE.Contracts.Exceptions
{
    public class WebDriverNotFoundException : Exception
    {
        public WebDriverNotFoundException(WebDrivers driver)
            : base($"Web driver '{driver}' is not declared")
        {
        }
    }
}
