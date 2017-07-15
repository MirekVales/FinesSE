using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Core.Configuration
{
    public class ScreenshotTakerConfiguration : IConfigurationKeys
    {
        public int ScreenshotTakeHorizontalOverlap { get; set; }
        public int ScreenshotTakeVerticalOverlap { get; set; }

        public static ScreenshotTakerConfiguration Default =>
            new ScreenshotTakerConfiguration()
            {
                ScreenshotTakeHorizontalOverlap = 35,
                ScreenshotTakeVerticalOverlap = 35
            };
    }
}
