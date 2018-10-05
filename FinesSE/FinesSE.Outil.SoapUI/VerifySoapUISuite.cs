using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.SoapUI
{
    public class VerifySoapUISuite : IStringAction
    {
        public IKernel Kernel { get; set; }

        [EntryPoint]
        public string Invoke(string pathToTests, string suiteName)
        {
            var runner = Kernel.Get<RunSoapUISuite>("");
            runner.Invoke(pathToTests, suiteName);

            var failed = runner.Results.Count(r => r.status == LogStatus.Fail);
            if (failed > 0)
                throw new SoapUISuiteException(
                    failed, 
                    runner
                        .Results
                        .Count(r => r.status == LogStatus.Pass));

            return "true";
        }
    }
}
