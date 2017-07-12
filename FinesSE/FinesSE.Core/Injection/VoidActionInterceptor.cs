using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using log4net;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class VoidActionInterceptor : IVoidActionInterceptor
    {
        public ILog Log { get; set; }

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
            {
                using (var e = new ActionNotFoundException(typeName))
                    Log.Fatal($"Implementation not found for void action {typeName}", e);
            }

            return null;
        }
    }
}
