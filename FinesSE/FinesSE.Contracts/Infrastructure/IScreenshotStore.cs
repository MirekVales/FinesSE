namespace FinesSE.Contracts.Infrastructure
{
    public interface IScreenshotStore
    {
        void Store(byte[] image, string objectId, string versionId);

        bool Exists(string objectId, string versionId);

        byte[] Get(string objectId, string versionId);

        string GetPath(string objectId, string versionId);

        double Compare(string objectId, string baseVersionId, string referenceVersionId);

        void Clear();
    }
}
