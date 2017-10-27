﻿using FinesSE.Contracts.Exceptions;
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
                var invoker = new Invoker(action);
                var parameters = Parser.Parse(invocationInfo.Arguments.First() as string[], invoker.ParameterTypes);

                Log.Debug($"Invoking void action {typeName} ({string.Join(", ", parameters)})");

                try
                {
                    invoker.Invoke(parameters.ToArray());
                    //action.Invoke(parameters.ToArray());
                }
                catch (Exception e)
                {
                    if (e.InnerException is SlimException)
                    {
                        Log.Warn($"Action {typeName} threw a slim exception: {e.Message}");
                        throw e;
                    }
                    else
                    {
                        Log.Fatal($"Action {typeName} threw exception: {e.Message}");
                        throw new ActionException($"Action {userFriendlyName} threw an exception: {e.Message}");
                    }
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
