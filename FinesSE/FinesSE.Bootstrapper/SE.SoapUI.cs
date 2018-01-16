using FinesSE.Outil.Reports;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string RunSoapUITests(string testFile, string suiteName)
            => p.Invoke<RunSoapUITests>(testFile, suiteName);
    }
}
