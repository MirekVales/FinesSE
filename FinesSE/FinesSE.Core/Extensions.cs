using FinesSE.Contracts.Infrastructure;
using LightInject;
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

        public static string FallbackEmptyString(this string value, Func<string> fallback)
            => string.IsNullOrWhiteSpace(value) ? fallback() : value;

        public static bool IsDynamic<T>(this T value)
            where T : struct
        {
            var memberInfo = (typeof(T)).GetMember(value.ToString());
            if (memberInfo.Length == 0)
                throw new Exception($"Member value '{value}' not found");
            return memberInfo[0].GetCustomAttributes(typeof(DynamicAttribute), true).Any();
        }

        public static IEnumerable<T> Split<T>(this T value)
            where T : struct
        {
            return value
                .ToString()
                .Split(',')
                .Select(flag => (T)Enum.Parse(typeof(T), flag))
                .ToList();
        }

        public static byte[] ToByteArray(this Image bitmap)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
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

        public static void RegisterRequiredAssembly(this IServiceRegistry registry, string parentDirectory, string fileName)
        {
            var directory = string.IsNullOrEmpty(parentDirectory)
                ? AppDomain.CurrentDomain.BaseDirectory
                : parentDirectory;

            if (!File.Exists(Path.Combine(directory, fileName)))
                throw new FileNotFoundException("Required component was not found", fileName);

            registry.RegisterAssembly(fileName);
        }
    }
}
