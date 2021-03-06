﻿namespace FinesSE.Bootstrapper
{
    using FinesSE.Contracts.Infrastructure;
    using FinesSE.Core;
    using FinesSE.Core.Configuration;
    using FinesSE.Core.Environment;
    using FinesSE.Core.Injection;
    using FinesSE.Core.Parsing;
    using FinesSE.Core.WebDriver;
    using FinesSE.Expressions;
    using FinesSE.Reports;
    using FinesSE.VisualRegression;
    using LightInject;
    using System.IO;
    using System.Reflection;

    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            var directory = GetLocation();

            container.RegisterRequiredAssembly(directory, "FinesSE.Drivers.dll");
            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.dll");

            container.Register<IConfigurationProvider, ConfigurationProvider>(new PerContainerLifetime());
            container.Register<IProcessListStorage, ProcessListStorage>(new PerContainerLifetime());
            container.Register<IWebDriverProvider, WebDriverProvider>(new PerContainerLifetime());
            container.Register<IParameterParser, ParameterParser>();
            container.Register<IWorkflowInterceptor, WorkflowInterceptor>();
            container.Register<IExecutionContextInterceptor, ExecutionContextInterceptor>();
            container.Register<IVoidActionInterceptor, VoidActionInterceptor>();
            container.Register<IActionInterceptor, ActionInterceptor>();
            container.Register<ILoggingInterceptor, LoggingInterceptor>();
            container.Register<IExecutionContext, ExecutionContext>(new PerContainerLifetime());
            container.Register<IInvocationProxy, InvocationProxy>();
            container.Register<IInvoker, Invoker>("Invoker", new PerContainerLifetime());

            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.VisualRegression.dll");
            container.RegisterFrom<VisualRegressionCompositionRoot>();

            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.Expressions.dll");
            container.RegisterFrom<ExpressionsCompositionRoot>();

            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.Reports.dll");
            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.SoapUI.dll");
            container.RegisterRequiredAssembly(directory, "FinesSE.Outil.Soap.dll");

            container.RegisterFrom<ReportsCompositionRoot>();
        }

        string GetLocation()
        {
            var uncLocation = Assembly.GetAssembly(typeof(CompositionRoot)).Location;
            return Path.GetDirectoryName(uncLocation);
        }
    }
}