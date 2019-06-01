namespace FinesSE.Bootstrapper
{
    using FinesSE.Outil.SoapUI;

    public partial class SE
    {
        public string GetSoapUICaseRequest(string suiteName, string caseName)
            => p.Invoke<GetSoapUICaseRequest>(suiteName, caseName);

        public string GetSoapUICaseResponse(string suiteName, string caseName)
            => p.Invoke<GetSoapUICaseResponse>(suiteName, caseName);

        public string RunSoapUISuite(string testFile, string suiteName)
            => p.Invoke<RunSoapUISuite>(testFile, suiteName);

        public string VerifySoapUISuite(string testFile, string suiteName)
            => p.Invoke<VerifySoapUISuite>(testFile, suiteName);
    }
}