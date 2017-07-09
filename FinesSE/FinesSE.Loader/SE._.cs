using FinesSE.Contracts.Infrastructure;
using FinesSE.Core.Injection;
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

        public void Dispose()
        {
            kernel.WebDriverProvider.Dispose();
        }

        ~SE()
            => Dispose();
    }
}
