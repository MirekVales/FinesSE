namespace FinesSE.Bootstrapper
{
    using FinesSE.Outil.VisualRegression.Actions;
    using FinesSE.Outil.VisualRegression.Assertions;

    public partial class SE
    {
        public string GetScreenDiff(string locator)
            => p.Invoke<GetScreenDiff>(locator);

        public string InlineScreenDiff(string locator)
            => p.Invoke<InlineScreenDiff>(locator);

        public void SetScreenDiffTolerance(string tolerance)
            => p.InvokeVoid<SetScreenDiffTolerance>(tolerance);

        public void SetTopic(string id)
            => p.InvokeVoid<SetTopic>(id);

        public string TakeBaseScreen(string locator)
            => p.Invoke<TakeBaseScreen>(locator);

        public string TakeScreen(string locator)
            => p.Invoke<TakeScreen>(locator, null);

        public string VerifyCssValid()
            => p.Invoke<VerifyCssValid>();

        public object VerifyScreenDiff(string locator)
            => p.Invoke<VerifyScreenDiff>(locator, null);

        public object VerifyScreenDiff(string locator, string tolerance)
            => p.Invoke<VerifyScreenDiff>(locator, tolerance);
    }
}
