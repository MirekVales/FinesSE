using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Core.WebDriver
{
    public class ScreenshotTakerConfiguration : IConfigurationKeys
    {
        public int Margin { get; set; }
        public int ScreenshotTakeHorizontalOverlap { get; set; }
        public int ScreenshotTakeVerticalOverlap { get; set; }

        public static ScreenshotTakerConfiguration Default =>
            new ScreenshotTakerConfiguration()
            {
                Margin = 5,
                ScreenshotTakeHorizontalOverlap = 35,
                ScreenshotTakeVerticalOverlap = 35
            };
    }
}
