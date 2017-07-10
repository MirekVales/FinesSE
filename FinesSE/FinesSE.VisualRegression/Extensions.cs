using OpenQA.Selenium;

namespace FinesSE.VisualRegression
{
    public static class Extensions
    {
        public static byte[] TakeScreenshot(this IWebElement element, IWebDriver driver)
        {
            return ((ITakesScreenshot)element).GetScreenshot().AsByteArray;
        }
    }
}
