using OpenQA.Selenium;
using System;
using System.Drawing;
using System.IO;

namespace FinesSE.Core.WebDriver
{
    public class ScreenshotTaker : IDisposable
    {
        public IJavaScriptExecutor Executor { get; private set; }
        public ITakesScreenshot TakesScreenshot { get; private set; }

        public IWebDriver Driver { get; }
        public Size PageSize { get; }
        public Size ViewSize { get; private set; }
        public double VerticalSnaps { get; private set; }
        public double HorizontalSnaps { get; private set; }
        
        public int InitialOffsetX { get; }
        public int InitialOffsetY { get; }
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }

        public ScreenshotTaker(IWebDriver driver, ScreenshotTakerConfiguration configuration)
        {
            Driver = driver;
            Executor = driver as IJavaScriptExecutor;
            TakesScreenshot = driver as ITakesScreenshot;
            PageSize = driver.PageSize();
            ViewSize = driver.ViewPort();
            ViewSize = ApplyOverlap(PageSize, ViewSize, configuration);

            HorizontalSnaps = Math.Ceiling((double)PageSize.Width / ViewSize.Width);
            VerticalSnaps = Math.Ceiling((double)PageSize.Height / ViewSize.Height);

            InitialOffsetX = OffsetX = Driver.GetOffsetX();
            InitialOffsetY = OffsetY = Driver.GetOffsetY();
        }

        private Size ApplyOverlap(Size pageSize, Size viewSize, ScreenshotTakerConfiguration configuration)
            => Size.Subtract(viewSize, 
                new Size(
                    pageSize.Width < viewSize.Width ? 0 : configuration.ScreenshotTakeHorizontalOverlap,
                    pageSize.Height < viewSize.Height ? 0 : configuration.ScreenshotTakeVerticalOverlap));

        public byte[] TakeImage()
        {
            using (var image = new Bitmap(PageSize.Width, PageSize.Height))
            {
                using (var g = Graphics.FromImage(image))
                {
                    for (var x = 0; x < HorizontalSnaps; x++)
                        for (var y = 0; y < VerticalSnaps; y++)
                        {
                            Driver.SetOffset(x * ViewSize.Width, y * ViewSize.Height);
                            using (var stream = new MemoryStream(TakesScreenshot.GetScreenshot().AsByteArray))
                            using (var bitmap = new Bitmap(stream))
                            {
                                UpdateOffsetX(out int oldOffsetX, out int diffX);
                                UpdateOffsetY(out int oldOffsetY, out int diffY);

                                var imageRectangle = new Rectangle(OffsetX, OffsetY, ViewSize.Width, ViewSize.Height);
                                var horizontalRedundancy = Math.Max(0, imageRectangle.Right - PageSize.Width) * Math.Min(x, 1);
                                var verticalRedundancy = Math.Max(0, imageRectangle.Bottom - PageSize.Height) * Math.Min(y, 1);

                                g.DrawImage(bitmap, OffsetX - horizontalRedundancy, OffsetY - verticalRedundancy);
                            }
                        }
                }
                return image.ToByteArray();
            }
        }

        private void UpdateOffsetX(out int oldOffsetX, out int diffX)
        {
            oldOffsetX = OffsetX;
            OffsetX = Driver.GetOffsetX();
            diffX = OffsetX - oldOffsetX;
        }

        private void UpdateOffsetY(out int oldOffsetY, out int diffY)
        {
            oldOffsetY = OffsetY;
            OffsetY = Driver.GetOffsetY();
            diffY = OffsetY - oldOffsetY;
        }

        public void Dispose()
        {
            Driver.SetOffset(InitialOffsetX, InitialOffsetY);
        }
    }
}
