using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Injection;
using FinesSE.Outil.Actions;
using System;

namespace FinesSE.Bootstrapper
{
    public partial class SE : IDisposable
    {
        readonly IKernel kernel;
        readonly IInvocationProxy p;

        public SE()
        {
            kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            p = kernel.Proxy;
        }

        public void SetBrowser(string browser)
            => p.Invoke<SetBrowser>(browser);

        public void Dispose()
            => kernel.DisposeKernel();

        ~SE()
            => Dispose();
    }
}
