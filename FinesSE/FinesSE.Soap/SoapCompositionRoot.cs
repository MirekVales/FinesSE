using FinesSE.Soap.Infrastructure;
using LightInject;

namespace FinesSE.Soap
{
    public class VisualRegressionCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<SoapClient>(new PerContainerLifetime());
        }
    }
}
