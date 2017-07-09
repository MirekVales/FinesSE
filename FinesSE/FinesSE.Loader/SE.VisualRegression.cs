using FinesSE.Outil.Actions;
using FinesSE.Outil.Assertions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void TakeShot(string locator, string versionId)
            => se.Invoke<TakeShot>(locator, versionId);

        public void VerifyShot(string locator, string baseVersionId, string referenceVersionId)
            => se.Invoke<VerifyShot>(locator, baseVersionId, referenceVersionId);
    }
}
