using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.WebDriver;
using LightInject.Interception;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class ExecutionContextInterceptor : IExecutionContextInterceptor
    {
        public IExecutionContext Context { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            if (!Context.Drivers.Any())
                return invocationInfo.Proceed();

            var coreConfiguration = Context.
                ConfigurationProvider.
                Get(CoreConfiguration.Default);

            object lastResult = null;
            foreach (var driver in Context.Drivers)
            {
                Context.SetBrowser(driver);
                lastResult = invocationInfo.Proceed();

                Context
                    .Driver
                    .WaitForDocumentCompleteness(coreConfiguration.WaitForDocumentCompleteState);
            }

            return lastResult;
        }
    }
}
