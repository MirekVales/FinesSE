using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            throw new NotImplementedException();
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

            driver.Manage().Window.Position = Scrapheap;
            var window = WindowFromPoint(Scrapheap);
            if (window == IntPtr.Zero)
                return;

            CloseWindow(window);
        }
    }
}
