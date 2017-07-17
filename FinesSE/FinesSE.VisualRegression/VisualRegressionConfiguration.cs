using FinesSE.Contracts.Infrastructure;

namespace FinesSE.VisualRegression
{
    public class VisualRegressionConfiguration : IConfigurationKeys
    {
        public string ScreenshotDiffTolerance { get; set; }
        public string ScreenshotStorePath { get; set; }
        public string ScreenshotStoreFilePrefix { get; set; }
        public string ScreenshotStoreFileExtension { get; set; }
        public string ScreenshotStoreBaseVersionId { get; set; }
        public string ScreenshotStoreReferenceVersionId { get; set; }
        public string ScreenshotStoreDiffVersionId { get; set; }

        public static VisualRegressionConfiguration Default =>
            new VisualRegressionConfiguration()
            {
                ScreenshotDiffTolerance = "1%",
                ScreenshotStorePath = @"C:\ScreenStore",
                ScreenshotStoreFileExtension = ".png",
                ScreenshotStoreFilePrefix = "",
                ScreenshotStoreBaseVersionId = "base",
                ScreenshotStoreReferenceVersionId = "ref",
                ScreenshotStoreDiffVersionId = "diff"
            };
    }
}
