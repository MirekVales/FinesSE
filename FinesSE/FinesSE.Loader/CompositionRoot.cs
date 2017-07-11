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
            //container.RegisterInstance<IWebDriverProvider>(new WebDriverProvider());
            container.Register<IParameterParser, ParameterParser>();
            container.Register<IVoidActionInterceptor, VoidActionInterceptor>();
            container.Register<IActionInterceptor, ActionInterceptor>();
            container.Register<ILoggingInterceptor, LoggingInterceptor>();
            container.Register<ISeleneseProxy, SeleneseProxy>();
            container.Register<IWebElementIdentityProvider, IdProvider>();
            container.Register<IScreenshotStore, DiskScreenshotStore>();
            container.Register<IImageComparer, MagickImageComparer>();
        }
    }
}
