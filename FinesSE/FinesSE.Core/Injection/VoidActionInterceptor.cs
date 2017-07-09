using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class VoidActionInterceptor : IVoidActionInterceptor
    {
        public IKernel Kernel { get; set; }
        public IParameterParser Parser { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var typeName = invocationInfo.Method.GetGenericArgumentsName().First();
            if (Kernel.CanGet<IVoidAction>(typeName))
            {
                var action = Kernel.Get<IVoidAction>(typeName);
                var parameters = Parser.Parse(invocationInfo.Arguments.First() as string[], action.GetParameterTypes());
                action.Invoke(parameters.ToArray());
                return null;
            }
            else
                throw new ActionNotFoundException(typeName);
        }
    }
}
