using FinesSE.Outil.Reports;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public void RunSoapUITests(string testFile, string suiteName)
            => p.InvokeVoid<RunSoapUITests>(testFile, suiteName);
    }
}
