using FinesSE.Contracts.Infrastructure;
using FinesSE.Expressions.Contracts;
using FinesSE.Outil.Expressions;
using LightInject;

namespace FinesSE.Expressions
{
    public class ExpressionsCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<IExpressionEngine, ExpressionEngine>(new PerContainerLifetime());
            container.Register<ICustomInterceptor, InterpolationInterceptor>(new PerContainerLifetime());
        }
    }
}
