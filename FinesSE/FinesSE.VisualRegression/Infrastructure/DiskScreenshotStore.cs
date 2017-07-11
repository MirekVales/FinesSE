using FinesSE.Contracts.Infrastructure;
using FinesSE.VisualRegression.Contracts;
using System.IO;

namespace FinesSE.VisualRegression.Infrastructure
{
    public class DiskScreenshotStore : IScreenshotStore
    {
        private readonly Configuration config;

        public IImageComparer ImageComparer { get; set; }

        public DiskScreenshotStore(IConfigurationProvider configuration)
        {
            config = configuration.Get(Configuration.Default);
        }

        public string GetPath(string objectId, string versionId)
            => Path.Combine(
                config.ScreenshotStorePath, 
                versionId, 
                $"{config.ScreenshotStoreFilePrefix}{objectId}{config.ScreenshotStoreFileExtension}");

        public void Clear()
        {
            Directory.Delete(config.ScreenshotStorePath, true);
            Directory.CreateDirectory(config.ScreenshotStorePath);
        }

        public double Compare(string objectId, string baseVersionId, string referenceVersionId)
        {
            EnsureFileExistence(objectId, baseVersionId);
            EnsureFileExistence(objectId, referenceVersionId);

            return ImageComparer.Compare(GetPath(objectId, baseVersionId), GetPath(objectId, referenceVersionId));
        }

        public void EnsureFileExistence(string objectId, string versionId)
        {
            if (!Exists(objectId, versionId))
                throw new ScreenshotNotFoundException(GetPath(objectId, versionId), versionId);
        }

        public bool Exists(string objectId, string versionId)
            => File.Exists(GetPath(objectId, versionId));

        public byte[] Get(string objectId, string versionId)
            => File.ReadAllBytes(GetPath(objectId, versionId));

        public void Store(byte[] image, string objectId, string versionId)
        {
            var path = GetPath(objectId, versionId);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path.Replace(Path.GetFileName(path),""));

            File.WriteAllBytes(path, image);
        }
    }
}