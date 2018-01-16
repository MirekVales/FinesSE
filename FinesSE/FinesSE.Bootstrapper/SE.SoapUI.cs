using FinesSE.Outil.SoapUI;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string RunSoapUITests(string testFile, string suiteName)
            => p.Invoke<RunSoapUITests>(testFile, suiteName);

        public void VerifySoapUITests(string testFile, string suiteName)
            => p.InvokeVoid<VerifySoapUITests>(testFile, suiteName);
    }
}
