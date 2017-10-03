using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using log4net;
using System;
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
            var userFriendlyName = typeName.Split('.').Last();
            if (Kernel.CanGet<IVoidAction>(typeName))
            {
                var action = Kernel.Get<IVoidAction>(typeName);
                var parameters = Parser.Parse(invocationInfo.Arguments.First() as string[], action.GetParameterTypes());

                Log.Debug($"Invoking void action {typeName} ({string.Join(", ", parameters)})");

                try
                {
                    action.Invoke(parameters.ToArray());
                }
                catch (SlimException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    Log.Fatal($"Action {typeName} threw exception: {e.Message}");
                    throw new ActionException($"Action {userFriendlyName} threw an exception: {e.Message}");
                }
            }
            else
            {
                using (var e = new ActionNotFoundException(userFriendlyName))
                    Log.Fatal($"Implementation not found for void action {typeName}", e);
            }

            return null;
        }
    }
}
