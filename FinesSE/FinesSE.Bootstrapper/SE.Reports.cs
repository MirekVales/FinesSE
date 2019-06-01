namespace FinesSE.Bootstrapper
{
    using FinesSE.Outil.Reports;

    public partial class SE
    {
        public string StartTest(string name, string description)
            => p.Invoke<StartTest>(name, description);

        public void LogTestInfo(string id, string status, string description)
            => p.InvokeVoid<LogTestInfo>(id, status, description);

        public void EndTest(string id, string status, string description)
            => p.InvokeVoid<EndTest>(id, status, description);
    }
}
