using FinesSE.Contracts.Infrastructure;
using FinesSE.Core;
using FinesSE.Core.Configuration;
using FinesSE.Core.Injection;
using FinesSE.Core.Parsing;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;
using LightInject;
using System.IO;
using System.Reflection;

namespace FinesSE.Bootstrapper
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            var directory = GetLocation();

            container.RegisterRequiredAssembly(directory, "FinesSE.Drivers.dll");
            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.dll");

            container.Register<IConfigurationProvider, ConfigurationProvider>(new PerContainerLifetime());
            container.Register<IWebDriverProvider, WebDriverProvider>(new PerContainerLifetime());
            container.Register<IParameterParser, ParameterParser>();
            container.Register<IExecutionContextInterceptor, ExecutionContextInterceptor>();
            container.Register<IVoidActionInterceptor, VoidActionInterceptor>();
            container.Register<IActionInterceptor, ActionInterceptor>();
            container.Register<ILoggingInterceptor, LoggingInterceptor>();
            container.Register<IExecutionContext, ExecutionContext>(new PerContainerLifetime());
            container.Register<IInvocationProxy, InvokationProxy>();

            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.VisualRegression.dll");
            container.RegisterFrom<VisualRegressionCompositionRoot>();
        }

        private string GetLocation()
        {
            var uncLocation = Assembly.GetAssembly(typeof(CompositionRoot)).Location;
            return Path.GetDirectoryName(uncLocation);
        }
    }
}
