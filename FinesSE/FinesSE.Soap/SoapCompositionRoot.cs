using FinesSE.Soap.Infrastructure;
using FinesSE.Soap.Infrastructure.DummyData;
using LightInject;

namespace FinesSE.Soap
{
    public class VisualRegressionCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<IDummyDataProcessor, DummyDataProcessor>(new PerContainerLifetime());
            container.Register<SoapClient>(new PerContainerLifetime());
        }
    }
}
