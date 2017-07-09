using System.Collections.Generic;
using LightInject;
using LightInject.Interception;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Core.Injection
{
    public class DefaultKernel<C> : IKernel
        where C : ICompositionRoot, new()
    {
        private readonly ServiceContainer container;

        public ISeleneseProxy SeleneseProvider
            => container.GetInstance<ISeleneseProxy>();

        public IWebDriverProvider WebDriverProvider
            => container.GetInstance<IWebDriverProvider>();

        public DefaultKernel()
        {
            container = new ServiceContainer();
        }

        public void Initialize()
        {
            container.RegisterFrom<C>();
            container.RegisterInstance<IKernel>(this);
            container.Intercept(x => x.ServiceType == typeof(ISeleneseProxy), (sf, pd) => DefineProxyType(pd));
        }

        private void DefineProxyType(ProxyDefinition proxyDefinition)
        {
            proxyDefinition.Implement(() => container.GetInstance<IActionInterceptor>());
            proxyDefinition.Implement(() => container.GetInstance<ILoggingInterceptor>());
        }

        public T Get<T>(string name)
            => container.GetInstance<T>(name);

        public bool CanGet<T>(string name)
            => container.CanGetInstance(typeof(T), name);

        public IEnumerable<ILocator> GetLocators()
            => container.GetAllInstances<ILocator>();

        public void AddAction<T>(string serviceName) where T : IAction
            => container.Register<IAction, T>(serviceName);
    }
}
