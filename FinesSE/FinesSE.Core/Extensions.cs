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
        public static IEnumerable<string> GetGenericArgumentsName(this MethodInfo methodInfo)
            => methodInfo
            .GetGenericArguments()
            .Select(x => x.Name);

        public static byte[] TakeScreenshot(this IWebElement element, IWebDriver driver)
        {
            var imageBytes = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            var image = new Bitmap(new MemoryStream(imageBytes));
            var crop = image.Crop(element.Area());
            return crop.ToByteArray();
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
