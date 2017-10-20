using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using log4net;
using System;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class ActionInterceptor : IActionInterceptor
    {
        public ILog Log { get; set; }

        public IKernel Kernel { get; set; }
        public IParameterParser Parser { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var typeName = invocationInfo.Method.GetGenericArgumentsName().First();
            var userFriendlyName = typeName.Split('.').Last();
            if (Kernel.CanGet<IAction>(typeName))
            {
                var action = Kernel.Get<IAction>(typeName);
                var parameters = Parser.Parse(
                    invocationInfo.Arguments.First() as string[],
                    action.GetParameterTypes());

                Log.Debug($"Invoking action {typeName} ({string.Join(", ", parameters)})");

                try
                {
                    return action.Invoke(parameters.ToArray());
                }
                catch (SlimException e)
                {
                    Log.Warn($"Action {typeName} threw a slim exception: {e.Message}");
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
                using (var e = new ActionNotFoundException(typeName))
                    Log.Fatal($"Implementation not found for action {typeName}", e);
            }

            return null;
        }
    }
}
