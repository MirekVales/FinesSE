using FinesSE.Contracts.Infrastructure;
using LightInject.Interception;

namespace FinesSE.Core.Injection
{
    public class LoggingInterceptor : ILoggingInterceptor
    {
        public object Invoke(IInvocationInfo invocationInfo)
        {
            return invocationInfo.Proceed();
        }
    }
}
