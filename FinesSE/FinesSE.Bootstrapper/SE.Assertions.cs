using FinesSE.Outil.Assertions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void VerifyText(string locator, string pattern)
            => se.InvokeVoid<VerifyText>(locator, pattern);
    }
}
