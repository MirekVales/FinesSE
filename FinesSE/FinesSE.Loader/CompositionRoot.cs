using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Configuration;
using FinesSE.Core.Injection;
using FinesSE.Core.Parsing;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;
using LightInject;

namespace FinesSE.Loader
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.RegisterAssembly("FinesSE.Outil.dll");

            container.Register<IConfigurationProvider, ConfigurationProvider>();
            container.Register<IWebDriverProvider, WebDriverProvider>(new PerContainerLifetime());
            container.Register<IParameterParser, ParameterParser>();
            container.Register<IExecutionContextInterceptor, ExecutionContextInterceptor>();
            container.Register<IVoidActionInterceptor, VoidActionInterceptor>();
            container.Register<IActionInterceptor, ActionInterceptor>();
            container.Register<ILoggingInterceptor, LoggingInterceptor>();
            container.Register<IExecutionContext, ExecutionContext>(new PerContainerLifetime());
            container.Register<ISeleneseProxy, SeleneseProxy>();

            container.RegisterAssembly("FinesSE.Outil.VisualRegression.dll");
            container.RegisterFrom<VisualRegressionCompositionRoot>();
        }
    }
}
