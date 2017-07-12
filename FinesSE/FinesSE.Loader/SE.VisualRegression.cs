using FinesSE.Outil.VisualRegression.Actions;
using FinesSE.Outil.VisualRegression.Assertions;
using System;

namespace FinesSE.Loader
{
    public partial class SE
    {
#warning TBR EContext
        public string SetTopic(string id)
            => se.Invoke<SetTopic>(id);

        public string TakeBaseScreen(string locator)
            => se.Invoke<TakeBaseScreen>(locator);

        public string TakeScreen(string locator)
            => se.Invoke<TakeScreen>(locator, null);

        [Obsolete]
        public string TakeScreen(string locator, string versionId)
            => se.Invoke<TakeScreen>(locator, versionId);

        public object VerifyScreenDiff(string locator, string tolerance)
            => se.Invoke<VerifyScreenDiff>(locator, null, null, tolerance);

        [Obsolete]
        public object VerifyScreenDiff(string locator, string baseVersionId, string referenceVersionId, string tolerance)
            => se.Invoke<VerifyScreenDiff>(locator, baseVersionId, referenceVersionId, tolerance);

        public string GetScreenDiff(string locator)
            => se.Invoke<GetScreenDiff>(locator, null, null);

        [Obsolete]
        public string GetScreenDiff(string locator, string baseVersionId, string referenceVersionId)
            => se.Invoke<GetScreenDiff>(locator, baseVersionId, referenceVersionId);
    }
}
