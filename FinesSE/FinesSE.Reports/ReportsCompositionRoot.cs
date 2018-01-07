using FinesSE.Contracts.Infrastructure;
using FinesSE.Reports.Infrastructure;
using LightInject;

namespace FinesSE.Reports
{
    public class ReportsCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<IReportBuilder, ReportBuilder>(new PerContainerLifetime());
        }
    }
}
