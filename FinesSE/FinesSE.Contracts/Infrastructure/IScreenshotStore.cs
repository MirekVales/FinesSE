namespace FinesSE.Contracts.Infrastructure
{
    public interface IScreenshotStore
    {
        void Store(byte[] image, string topicId, string objectId, string versionId);

        bool Exists(string topicId, string objectId, string versionId);

        byte[] Get(string topicId, string objectId, string versionId);

        string GetPath(string topicId, string objectId, string versionId);

        double Compare(string topicId, string objectId, string baseVersionId, string referenceVersionId);

        void Clear();
    }
}
