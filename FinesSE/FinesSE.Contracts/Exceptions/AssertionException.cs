namespace FinesSE.Contracts.Exceptions
{
    public class AssertionException : SlimException
    {
        public AssertionException(string expected, string actual, WebDrivers drivers)
            : base($"Expected '{expected}' Actual '{actual}' Driver '{drivers}'")
        { }
    }
}
