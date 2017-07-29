using FinesSE.Outil.VisualRegression.Actions;
using FinesSE.Outil.VisualRegression.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string GetScreenDiff(string locator)
            => se.Invoke<GetScreenDiff>(locator);

        public string InlineScreenDiff(string locator)
            => se.Invoke<InlineScreenDiff>(locator);

        public void SetTopic(string id)
            => se.InvokeVoid<SetTopic>(id);

        public string TakeBaseScreen(string locator)
            => se.Invoke<TakeBaseScreen>(locator);

        public string TakeScreen(string locator)
            => se.Invoke<TakeScreen>(locator, null);

        public void VerifyCssValid()
            => se.InvokeVoid<VerifyCssValid>();

        public object VerifyScreenDiff(string locator)
            => se.Invoke<VerifyScreenDiff>(locator, null);

        public object VerifyScreenDiff(string locator, string tolerance)
            => se.Invoke<VerifyScreenDiff>(locator, tolerance);
    }
}
