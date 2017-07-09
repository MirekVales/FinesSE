using FinesSE.Outil.Assertions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void VerifyText(string locator, string pattern)
            => se.Invoke<VerifyText>(locator, pattern);
    }
}
