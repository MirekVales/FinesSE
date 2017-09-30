using FinesSE.Expressions.Contracts;
using LightInject;

namespace FinesSE.Expressions
{
    public class ExpressionsCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<IExpressionEngine, ExpressionEngine>(new PerContainerLifetime());
        }
    }
}
