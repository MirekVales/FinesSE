using OpenQA.Selenium;
using System;
using System.Drawing;
using System.IO;

namespace FinesSE.Core.WebDriver
{
    public class ScreenshotTaker
    {
        public Size PageSize { get; private set; }
        public Size ViewSize { get; private set; }
        public double VerticalSnaps { get; private set; }
        public double HorizontalSnaps { get; private set; }
        public IJavaScriptExecutor Executor { get; private set; }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }
        public ITakesScreenshot TakesScreenshot { get; private set; }

        public ScreenshotTaker(IWebDriver driver)
        {
            Executor = driver as IJavaScriptExecutor;
            TakesScreenshot = driver as ITakesScreenshot;
            PageSize = driver.PageSize();
            ViewSize = driver.ViewPort();
            HorizontalSnaps = Math.Ceiling((double)PageSize.Width / ViewSize.Width);
            VerticalSnaps = Math.Ceiling((double)PageSize.Height / ViewSize.Height);
            OffsetX = int.Parse(ExecuteScript("return window.pageXOffset;"));
            OffsetY = int.Parse(ExecuteScript("return window.pageYOffset;"));
        }

        public byte[] TakeImage()
        {
            using (var image = new Bitmap(PageSize.Width, PageSize.Height))
            {
                using (var g = Graphics.FromImage(image))
                {
                    for (var x = 0; x < HorizontalSnaps; x++)
                        for (var y = 0; y < VerticalSnaps; y++)
                        {
                            SetOffset(x * ViewSize.Width, y * ViewSize.Height);
                            using (var stream = new MemoryStream(TakesScreenshot.GetScreenshot().AsByteArray))
                            using (var bitmap = new Bitmap(stream))
                                g.DrawImage(bitmap, x * ViewSize.Width, y * ViewSize.Height);
                        }
                }
                SetOffset(OffsetX, OffsetY);
                return image.ToByteArray();
            }
        }

        private void SetOffset(int x, int y)
            => ExecuteScript($"window.pageXOffset = {x};window.pageYOffset = {y}; scrollTo({x},{y});");

        private string ExecuteScript(string command)
            => Executor.ExecuteScript(command) + "";
    }
}
