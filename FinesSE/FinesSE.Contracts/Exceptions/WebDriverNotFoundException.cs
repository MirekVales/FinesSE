namespace FinesSE.Contracts.Exceptions
{
    public class WebDriverNotFoundException : SlimException
    {
        public WebDriverNotFoundException(WebDrivers driver)
            : base($"Web driver '{driver}' is not declared")
        {
        }
    }
}
