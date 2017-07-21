using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace FinesSE.Core.WebDriver
{
    public static class WebExtensions
    {
        public static LocatedElements AsLocatedElements(this IReadOnlyCollection<IWebElement> collection, ILocator locator, string parameter)
            => new LocatedElements(locator, parameter, collection);

        public static object ExecuteScript(this IWebDriver driver, string script)
            => (driver as IJavaScriptExecutor).ExecuteScript(script);

        public static object ExecuteScriptWithArguments(this IWebDriver driver, string script, params object[] arguments)
            => (driver as IJavaScriptExecutor).ExecuteScript(script, arguments);

        public static T ExecuteScriptWithArguments<T>(this IWebDriver driver, string script, Func<object, T> convert, params object[] arguments)
            => convert((driver as IJavaScriptExecutor).ExecuteScript(script, arguments));

        public static T ExecuteScript<T>(this IWebDriver driver, string script, Func<object, T> convert)
            => convert((driver as IJavaScriptExecutor).ExecuteScript(script));

        public static string[] GetLinkedCssUrls(this IWebDriver driver)
            => driver
            .ExecuteScript(JavascriptCode.GetLinkedCssUrls, Convert.ToString)
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        public static string DownloadResource(this IWebDriver driver, string url)
            => driver.ExecuteScriptWithArguments(JavascriptCode.DownloadResource(url), Convert.ToString);

        public static Size PageSize(this IWebDriver driver)
            => new Size()
            {
                Width = driver.ExecuteScript(JavascriptCode.ReturnPageWidth, Convert.ToInt32),
                Height = driver.ExecuteScript(JavascriptCode.ReturnPageHeight, Convert.ToInt32)
            };

        public static Size ViewPort(this IWebDriver driver)
            => new Size()
            {
                Width = driver.ExecuteScript(JavascriptCode.ReturnViewWidth, Convert.ToInt32),
                Height = driver.ExecuteScript(JavascriptCode.ReturnViewHeight, Convert.ToInt32)
            };

        public static byte[] TakeScreenshot(this IWebElement element, IWebDriver driver, IConfigurationProvider configuration)
        {
            var pageSize = driver.PageSize();
            var browserSize = driver.ViewPort();
            byte[] imageBytes;

            if (pageSize.Width < browserSize.Width && pageSize.Height < browserSize.Height)
                imageBytes = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            else
            {
                using (var screenshotTaker = new ScreenshotTaker(driver, configuration))
                    imageBytes = screenshotTaker.TakeImage();
            }

            using (var stream = new MemoryStream(imageBytes))
            using (var image = new Bitmap(stream))
            {
                var crop = image.Crop(element.Area());
                return crop.ToByteArray();
            }
        }

        public static Rectangle Area(this IWebElement element)
            => new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);

        public static Image Crop(this Image image, Rectangle keptArea)
        {
            if (keptArea.Width * keptArea.Height == 0)
                return null;

            var cropped = new Bitmap(keptArea.Width, keptArea.Height);
            using (var g = Graphics.FromImage(cropped))
            {
                g.DrawImage(image, 0, 0, keptArea, GraphicsUnit.Pixel);
                return cropped;
            }
        }

        public static void SetOffset(this IWebDriver driver, int x, int y)
            => driver.ExecuteScript(JavascriptCode.ScrollTo(x, y));

        public static int GetOffsetX(this IWebDriver driver)
            => driver.ExecuteScript(JavascriptCode.ReturnPageOffsetX, Convert.ToInt32);

        public static int GetOffsetY(this IWebDriver driver)
            => driver.ExecuteScript(JavascriptCode.ReturnPageOffsetY, Convert.ToInt32);
    }
}
