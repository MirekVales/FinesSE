using FinesSE.Outil.Actions;
using FinesSE.Outil.Assertions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void TakeShot(string locator, string versionId)
            => se.InvokeVoid<TakeShot>(locator, versionId);

        public void VerifyShot(string locator, string baseVersionId, string referenceVersionId)
            => se.InvokeVoid<VerifyShot>(locator, baseVersionId, referenceVersionId);
    }
}
