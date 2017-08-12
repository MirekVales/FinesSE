using FinesSE.Outil.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void VerifyText(string locator, string pattern)
            => p.InvokeVoid<VerifyText>(locator, pattern);
    }
}
