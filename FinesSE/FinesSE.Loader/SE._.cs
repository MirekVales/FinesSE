using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Injection;
using FinesSE.Outil.Actions;
using System;

namespace FinesSE.Loader
{
    public partial class SE : IDisposable
    {
        readonly IKernel kernel;
        readonly ISeleneseProxy se;

        public SE()
        {
            kernel = new DefaultKernel<CompositionRoot>();
            kernel.Initialize();
            se = kernel.SeleneseProvider;
        }

        public void SetBrowser(string browser)
            => se.Invoke<SetBrowser>(browser);

        public void Dispose()
        {
            kernel.Context.Dispose();
        }

        ~SE()
            => Dispose();
    }
}
