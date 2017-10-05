using FinesSE.Contracts.Infrastructure;
using LightInject.Interception;
using log4net;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class LoggingInterceptor : ILoggingInterceptor
    {
        public ILog Log { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            Log.Debug($"Invoking {invocationInfo.Method.Name} with args " +
                $"{string.Join(",", invocationInfo.Arguments.First() as string[])}");

            var response = invocationInfo.Proceed();

            Log.Debug($"Invocation returned {response}");

            return response;
        }
    }
}
