using FinesSE.Outil.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void VerifyEquality(string expected, string actual)
            => p.InvokeVoid<VerifyEquality>(expected, actual);

        public void VerifyText(string locator, string pattern)
            => p.InvokeVoid<VerifyText>(locator, pattern);
    }
}
