using FinesSE.Contracts.Exceptions;

namespace FinesSE.VisualRegression.Contracts
{
    public class ScreenshotNotFoundException : SlimException
    {
        public ScreenshotNotFoundException(string fileName, string version)
            : base($"Screenshot version '{version}' not found. Expected location: '{fileName}'")
        {
        }
    }
}
