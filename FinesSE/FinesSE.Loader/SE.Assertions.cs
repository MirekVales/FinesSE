using FinesSE.Outil.Assertions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void VerifyText(string locator, string pattern)
            => se.InvokeVoid<VerifyText>(locator, pattern);
    }
}
