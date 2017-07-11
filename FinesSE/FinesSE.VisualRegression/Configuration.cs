﻿using FinesSE.Contracts.Infrastructure;

namespace FinesSE.VisualRegression
{
    public class Configuration : IConfigurationKeys
    {
        public string ScreenshotStorePath { get; set; }
        public string ScreenshotStoreFilePrefix { get; set; }
        public string ScreenshotStoreFileExtension { get; set; }
        public string ScreenshotStoreBaseVersionId { get; set; }
        public string ScreenshotStoreReferenceVersionId { get; set; }

        public static Configuration Default =>
            new Configuration()
            {
                ScreenshotStorePath = @"C:\ScreenStore",
                ScreenshotStoreFileExtension = ".png",
                ScreenshotStoreFilePrefix = "",
                ScreenshotStoreBaseVersionId = "base",
                ScreenshotStoreReferenceVersionId = "ref"
            };
    }
}
