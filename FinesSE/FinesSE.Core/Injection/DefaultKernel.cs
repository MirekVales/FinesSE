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
        readonly ServiceContainer container;
        ILog coreLog;
        LogAppender appender;

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
            container.RegisterInstance(coreLog = LogManager.GetLogger("CoreLog"));
            container.RegisterFrom<C>();
            container.RegisterInstance<IKernel>(this);
            container.Intercept(x => x.ServiceType == typeof(IInvocationProxy), (sf, pd) => DefineProxyType(pd));

            if (container.GetInstance<IConfigurationProvider>().Get(CoreConfiguration.Default).LogToFile)
            {
                appender = new LogAppender(container.GetInstance<IConfigurationProvider>());
                BasicConfigurator.Configure(LogManager.GetRepository(), appender);
            }

            container.GetInstance<IProcessListStorage>().CleanList();

            coreLog.Info(new string('=', 10));
            coreLog.Debug($"Kernel initialized");
        }

        void DefineProxyType(ProxyDefinition proxyDefinition)
        {
            Func<MethodInfo, Type, bool> predicate =
                (m, t) => m.GetGenericArguments().Any() && t.IsAssignableFrom(m.GetGenericArguments().First());

            proxyDefinition.Implement(() => container.GetInstance<ILoggingInterceptor>(), m => predicate(m, typeof(IStringAction)) || predicate(m, typeof(IVoidAction)));
            proxyDefinition.Implement(() => container.GetInstance<ICustomInterceptor>(), m => predicate(m, typeof(IAction)) || predicate(m, typeof(IWorkflowAction)));
            proxyDefinition.Implement(() => container.GetInstance<IWorkflowInterceptor>(), m => predicate(m, typeof(IWorkflowAction)));
            proxyDefinition.Implement(() => container.GetInstance<IExecutionContextInterceptor>(), m => predicate(m, typeof(IStringAction)) || predicate(m, typeof(IVoidAction)));
            proxyDefinition.Implement(() => container.GetInstance<IActionInterceptor>(), m => predicate(m, typeof(IStringAction)));
            proxyDefinition.Implement(() => container.GetInstance<IVoidActionInterceptor>(), m => predicate(m, typeof(IVoidAction)));
        }

        public T Get<T>(string name)
            => container.GetInstance<T>(name);

        public TService Get<T, TService>(T value, string name)
            => container.GetInstance<T, TService>(value, name);

        public TService Get<T1, T2, TService>(T1 value, T2 value2, string name)
            => container.GetInstance<T1, T2, TService>(value, value2, name);

        public bool CanGet<T>(string name)
            => container.CanGetInstance(typeof(T), name);

        public IEnumerable<ILocator> GetLocators()
            => container.GetAllInstances<ILocator>();

        public IEnumerable<IParseMethod> GetParserMethods()
            => container.GetAllInstances<IParseMethod>();

        public IEnumerable<IWebDriverActivator> GetWebDriverActivators()
            => container.GetAllInstances<IWebDriverActivator>();

        public void AddAction<T>(string serviceName) where T : IStringAction
            => container.Register<IStringAction, T>(serviceName);

        public void AddVoidAction<T>(string serviceName) where T : IVoidAction
            => container.Register<IVoidAction, T>(serviceName);

        public void DisposeKernel()
        {
            coreLog.Debug($"Disposing kernel");

            container.GetInstance<IProcessListStorage>().CleanList();
            container.Dispose();
            appender?.Dispose();
        }
    }
}
