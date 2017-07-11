using FinesSE.Contracts.Infrastructure;
using FinesSE.VisualRegression.Infrastructure;
using LightInject;

namespace FinesSE.VisualRegression
{
    public class VisualRegressionCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<IWebElementIdentityProvider, IdProvider>();
            container.Register<IScreenshotStore, DiskScreenshotStore>();
            container.Register<IImageComparer, MagickImageComparer>();
        }
    }
}
