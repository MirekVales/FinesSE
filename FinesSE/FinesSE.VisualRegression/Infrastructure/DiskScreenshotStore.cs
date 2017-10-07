using FinesSE.Contracts.Infrastructure;
using FinesSE.VisualRegression.Contracts;
using System;
using System.IO;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class DiskScreenshotStore : IScreenshotStore
    {
        private readonly VisualRegressionConfiguration config;

        public IImageComparer ImageComparer { get; set; }

        public DiskScreenshotStore(IConfigurationProvider configuration)
        {
            config = configuration.Get(VisualRegressionConfiguration.Default);
        }

        string GetPath()
            => Path.IsPathRooted(config.ScreenshotStorePath) ?
            config.ScreenshotStorePath :
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.ScreenshotStorePath);

        public string GetPath(string topicId, string objectId, string versionId)
            => Path.Combine(
                GetPath(),
                topicId,
                versionId,
                $"{config.ScreenshotStoreFilePrefix}{objectId}{config.ScreenshotStoreFileExtension}");

        public void Clear()
        {
            Directory.Delete(GetPath(), true);
            Directory.CreateDirectory(GetPath());
        }

        public double Compare(string topicId, string objectId, string baseVersionId, string referenceVersionId)
        {
            EnsureFileExistence(topicId, objectId, baseVersionId);
            EnsureFileExistence(topicId, objectId, referenceVersionId);

            var path1 = GetPath(topicId, objectId, baseVersionId);
            var path2 = GetPath(topicId, objectId, referenceVersionId);
            var diff = ImageComparer.Compare(
                path1,
                path2,
                config.ScreenshotDiffComparedChannels);
            ImageComparer.CreateDiffImage(path1, path2, GetPath(topicId, objectId, config.ScreenshotStoreDiffVersionId));
            return diff;
        }

        public void EnsureFileExistence(string topicId, string objectId, string versionId)
        {
            if (!Exists(topicId, objectId, versionId))
                throw new ScreenshotNotFoundException(GetPath(topicId, objectId, versionId), versionId);
        }

        public bool Exists(string topicId, string objectId, string versionId)
            => File.Exists(GetPath(topicId, objectId, versionId));

        public byte[] Get(string topicId, string objectId, string versionId)
            => File.ReadAllBytes(GetPath(topicId, objectId, versionId));

        public void Store(byte[] image, string topicId, string objectId, string versionId)
        {
            var path = GetPath(topicId, objectId, versionId);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path.Replace(Path.GetFileName(path),""));

            File.WriteAllBytes(path, image);
        }
    }
}