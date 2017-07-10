using FinesSE.Contracts.Infrastructure;
using System.IO;

namespace FinesSE.VisualRegression
{
    public class DiskScreenshotStore : IScreenshotStore
    {
        private readonly string BasePath;
        private readonly string Prefix;
        private readonly string Extension = ".png";

        public IImageComparer ImageComparer { get; set; }

        public DiskScreenshotStore()
        {
            BasePath = @"C:\ScreenStore";
            Prefix = "screen_";
        }

        public string GetPath(string objectId, string versionId)
            => Path.Combine(BasePath, versionId, $"{Prefix}{objectId}{Extension}");

        public void Clear()
        {
            Directory.Delete(BasePath, true);
            Directory.CreateDirectory(BasePath);
        }

        public double Compare(string objectId, string baseVersionId, string referenceVersionId)
        {
            if (!Exists(objectId, baseVersionId) || !Exists(objectId, referenceVersionId))
                throw new System.Exception();

            return ImageComparer.Compare(GetPath(objectId, baseVersionId), GetPath(objectId, referenceVersionId));
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