using FinesSE.Contracts.Infrastructure;
using LightInject.Interception;
using System.Threading.Tasks;

namespace FinesSE.Core.Environment
{
    public class DelayerInterceptor : ICustomInterceptor
    {
        readonly CoreConfiguration configuration;

        public DelayerInterceptor(IConfigurationProvider configurationProvider)
        {
            configuration = configurationProvider.Get(CoreConfiguration.Default);
        }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            if (configuration.DelayerTime.TotalMilliseconds > 0)
                Task.Delay((int)configuration.DelayerTime.TotalMilliseconds).Wait();

            return invocationInfo.Proceed();
        }
    }
}
