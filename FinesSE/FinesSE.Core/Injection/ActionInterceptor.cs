using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class ActionInterceptor : IActionInterceptor
    {
        public IKernel Kernel { get; set; }
        public IParameterParser Parser { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var typeName = invocationInfo.Method.GetGenericArgumentsName().First();
            if (Kernel.CanGet<IAction>(typeName))
            {
                var action = Kernel.Get<IAction>(typeName);
                var parameters = Parser.Parse(invocationInfo.Arguments.First() as string[], action.GetParameterTypes());
                return action.Invoke(parameters.ToArray());
            }
            else
                throw new ActionNotFoundException(typeName);
        }
    }
}
