using System;
using System.Collections.Generic;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.Injection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinesSE.Contracts.Exceptions;
using FinesSE.Loader;

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
                => kernel.SeleneseProvider.Invoke<CustomAction>("")
            );
        }

        [TestMethod]
        public void InterceptsAction()
        {
            var kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            kernel.AddAction<CustomAction>("CustomAction");
            Assert.ThrowsException<InvalidOperationException>(()
                => kernel.SeleneseProvider.Invoke<CustomAction>("")
            );
        }

        public class CustomAction : IAction
        {
            public IEnumerable<Type> GetParameterTypes()
            {
                yield return typeof(string);
            }

            public string Invoke(params object[] parameters)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
