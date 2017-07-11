using FinesSE.Contracts.Exceptions;

namespace FinesSE.VisualRegression.Contracts
{
    public class ScreenshotNotFoundException : SlimException
    {
        public ScreenshotNotFoundException(string fileName, string version)
            : base($"Saved screenshot not found. Version '{version}' File name '{fileName}'")
        {
        }
    }
}
