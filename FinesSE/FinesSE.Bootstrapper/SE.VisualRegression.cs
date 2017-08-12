using FinesSE.Outil.VisualRegression.Actions;
using FinesSE.Outil.VisualRegression.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string GetScreenDiff(string locator)
            => p.Invoke<GetScreenDiff>(locator);

        public string InlineScreenDiff(string locator)
            => p.Invoke<InlineScreenDiff>(locator);

        public void SetTopic(string id)
            => p.InvokeVoid<SetTopic>(id);

        public string TakeBaseScreen(string locator)
            => p.Invoke<TakeBaseScreen>(locator);

        public string TakeScreen(string locator)
            => p.Invoke<TakeScreen>(locator, null);

        public void VerifyCssValid()
            => p.InvokeVoid<VerifyCssValid>();

        public object VerifyScreenDiff(string locator)
            => p.Invoke<VerifyScreenDiff>(locator, null);

        public object VerifyScreenDiff(string locator, string tolerance)
            => p.Invoke<VerifyScreenDiff>(locator, tolerance);
    }
}
