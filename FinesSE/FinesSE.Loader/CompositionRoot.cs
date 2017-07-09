using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Injection;
using FinesSE.Core.Parsing;
using FinesSE.Core.WebDriver;
using LightInject;

namespace FinesSE.Loader
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.RegisterAssembly("FinesSE.Outil.dll");
            container.RegisterInstance<IWebDriverProvider>(new WebDriverProvider());
            container.Register<IParameterParser, ParameterParser>();
            container.Register<IActionInterceptor, ActionInterceptor>();
            container.Register<ILoggingInterceptor, LoggingInterceptor>();
            container.Register<ISeleneseProxy, SeleneseProxy>();
        }
    }
}
