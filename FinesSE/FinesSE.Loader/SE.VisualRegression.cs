using FinesSE.Outil.Actions;
using FinesSE.Outil.Assertions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public string TakeScreen(string locator, string versionId)
            => se.Invoke<TakeScreen>(locator, versionId);

        public object VerifyScreen(string locator, string baseVersionId, string referenceVersionId, string tolerance)
            => se.Invoke<VerifyScreen>(locator, baseVersionId, referenceVersionId, tolerance);

        public string GetScreenDiff(string locator, string baseVersionId, string referenceVersionId)
            => se.Invoke<GetScreenDiff>(locator, baseVersionId, referenceVersionId);
    }
}
