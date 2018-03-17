using FinesSE.Outil.SoapUI;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string GetSoapUICaseResult(string suiteName, string caseName)
            => p.Invoke<RunSoapUISuite>(suiteName, caseName);

        public string RunSoapUISuite(string testFile, string suiteName)
            => p.Invoke<RunSoapUISuite>(testFile, suiteName);

        public void VerifySoapUISuite(string testFile, string suiteName)
            => p.InvokeVoid<VerifySoapUISuite>(testFile, suiteName);
    }
}
