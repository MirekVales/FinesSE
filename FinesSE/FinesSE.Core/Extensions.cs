using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FinesSE.Core
{
    public static class Extensions
    {
        public static LocatedElements AsLocatedElements(this IReadOnlyCollection<IWebElement> collection, ILocator locator, string parameter)
            => new LocatedElements(locator, parameter, collection);

        public static IEnumerable<string> GetGenericArgumentsName(this MethodInfo methodInfo)
            => methodInfo
            .GetGenericArguments()
            .Select(x => x.Name);

        public static object ExecuteScript(this IWebDriver driver, string script)
            => (driver as IJavaScriptExecutor).ExecuteScript(script);

        public static object ExecuteScriptWithArguments(this IWebDriver driver, string script, params object[] arguments)
            => (driver as IJavaScriptExecutor).ExecuteScript(script, arguments);

        public static T ExecuteScript<T>(this IWebDriver driver, string script, Func<object, T> convert)
            => convert((driver as IJavaScriptExecutor).ExecuteScript(script));

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

        public static byte[] ToByteArray(this Image bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;
                return memoryStream.ToArray();
            }
        }

        public static Bitmap ToBitmap(this byte[] array)
        {
            var stream = new MemoryStream(array);
            return new Bitmap(stream);
        }

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point p);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        private static void CloseWindow(IntPtr hwnd)
            => SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

        public static void CloseWindow(this IWebDriver driver)
        {
            Point Scrapheap = new Point(-2751, -2751);

            try
            {
                driver.Manage().Window.Position = Scrapheap;
            }
            catch (WebDriverException)
            {
                return;
            }

            var window = WindowFromPoint(Scrapheap);
            if (window == IntPtr.Zero)
                return;
            CloseWindow(window);

        }
    }
}
