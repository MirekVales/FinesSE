using System.Collections.Generic;
using LightInject;
using LightInject.Interception;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Linq;
using System;
using System.Reflection;
using log4net;
using FinesSE.Core.Environment;
using log4net.Config;

namespace FinesSE.Core.Injection
{
    public class DefaultKernel<C> : IKernel
        where C : ICompositionRoot, new()
    {
        private readonly ServiceContainer container;
        private ILog coreLog;
        private LogAppender appender;

        public IInvocationProxy Proxy
            => container.GetInstance<IInvocationProxy>();

        public IExecutionContext Context
            => container.GetInstance<IExecutionContext>();

        public DefaultKernel()
        {
            container = new ServiceContainer();
        }

        public void Initialize()
        {
            ProcessList.LoadFromDisk().CleanList();

            container.RegisterInstance(coreLog = LogManager.GetLogger("CoreLog"));
            container.RegisterFrom<C>();
            container.RegisterInstance<IKernel>(this);
            container.Intercept(x => x.ServiceType == typeof(IInvocationProxy), (sf, pd) => DefineProxyType(pd));

            if (container.GetInstance<IConfigurationProvider>().Get(CoreConfiguration.Default).LogToFile)
            {
                appender = new LogAppender(container.GetInstance<IConfigurationProvider>());
                BasicConfigurator.Configure(LogManager.GetRepository(), appender);
            }
        }

        private void DefineProxyType(ProxyDefinition proxyDefinition)
        {
            Func<MethodInfo, Type, bool> predicate =
                (m, t) => m.GetGenericArguments().Any() && t.IsAssignableFrom(m.GetGenericArguments().First());

            proxyDefinition.Implement(() => container.GetInstance<IExecutionContextInterceptor>(), m => predicate(m, typeof(IAction)) || predicate(m, typeof(IVoidAction)));
            proxyDefinition.Implement(() => container.GetInstance<IVoidActionInterceptor>(), m => predicate(m, typeof(IVoidAction)));
            proxyDefinition.Implement(() => container.GetInstance<IActionInterceptor>(), m => predicate(m, typeof(IAction)));
            proxyDefinition.Implement(() => container.GetInstance<ILoggingInterceptor>(), m => predicate(m, typeof(IAction)) || predicate(m, typeof(IVoidAction)));
        }

        public T Get<T>(string name)
            => container.GetInstance<T>(name);

        public bool CanGet<T>(string name)
            => container.CanGetInstance(typeof(T), name);

        public IEnumerable<ILocator> GetLocators()
            => container.GetAllInstances<ILocator>();

        public IEnumerable<IParseMethod> GetParserMethods()
            => container.GetAllInstances<IParseMethod>();

        public IEnumerable<IWebDriverActivator> GetWebDriverActivators()
            => container.GetAllInstances<IWebDriverActivator>();

        public void AddAction<T>(string serviceName) where T : IAction
            => container.Register<IAction, T>(serviceName);

        public void AddVoidAction<T>(string serviceName) where T : IVoidAction
            => container.Register<IVoidAction, T>(serviceName);

        public void DisposeKernel()
        {
            container.Dispose();

            ProcessList.LoadFromDisk().CleanList();

            appender?.Dispose();
        }
    }
}
