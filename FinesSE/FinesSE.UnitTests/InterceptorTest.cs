using System;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.Injection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinesSE.Contracts.Exceptions;
using FinesSE.Bootstrapper;
using System.Diagnostics;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Core;

namespace FinesSE.UnitTests
{
    [TestClass]
    public class InterceptorTest
    {
        [TestMethod]
        public void FailsOnUndeclaredAction()
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            Assert.ThrowsException<ActionNotFoundException>(()
                => kernel.Proxy.Invoke<CustomAction>("")
            );
        }

        [TestMethod]
        public void InterceptsAction()
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            kernel.AddAction<CustomAction>(typeof(CustomAction).FullName);
            Assert.ThrowsException<ActionException>(()
                => kernel.Proxy.Invoke<CustomAction>("")
            );
        }

        public class CustomAction : IStringAction
        {
            [EntryPoint]
            public string Invoke(string parameter)
            {
                throw new InvalidOperationException();
            }
        }

        [TestMethod]
        public void ActionReturnsValue()
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            kernel.AddAction<CustomAction3>(typeof(CustomAction3).FullName);

            var value = Guid.NewGuid().ToString();
            Assert.AreEqual(value, kernel.Proxy.Invoke<CustomAction3>(value));
        }

        public class CustomAction3 : IStringAction
        {
            [EntryPoint]
            public string Invoke(string parameter)
                => parameter;
        }

        [TestMethod]
        public void InterceptsVoidAction()
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            kernel.AddVoidAction<CustomAction2>(typeof(CustomAction2).FullName);
            Assert.ThrowsException<ActionException>(()
                => kernel.Proxy.InvokeVoid<CustomAction2>("")
            );
        }

        public class CustomAction2 : IVoidAction
        {
            [EntryPoint]
            public void Invoke(string parameter)
            {
                throw new InvalidOperationException();
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(100)]
        [DataRow(200)]
        [DataRow(300)]
        [DataRow(400)]
        public void CanDelayActions(int minProcessingTime)
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            kernel.AddAction<CustomAction3>(typeof(CustomAction3).FullName);
            var configuration = kernel
                .Get<IConfigurationProvider>("")
                .Get(CoreConfiguration.Default);
            configuration.DelayerTime = TimeSpan.FromMilliseconds(minProcessingTime);

            var watch = Stopwatch.StartNew();
            var value = Guid.NewGuid().ToString();
            Assert.AreEqual(value, kernel.Proxy.Invoke<CustomAction3>(value));
            Assert.IsTrue(watch.ElapsedMilliseconds > minProcessingTime);
        }
    }
}