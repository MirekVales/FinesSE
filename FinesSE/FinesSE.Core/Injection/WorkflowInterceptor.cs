using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using LightInject.Interception;
using log4net;
using System.Linq;

namespace FinesSE.Core.Injection
{
    public class WorkflowInterceptor : IWorkflowInterceptor
    {
        public ILog Log { get; set; }

        public IKernel Kernel { get; set; }
        public IParameterParser Parser { get; set; }

        public IExecutionContext Context { get; set; }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var typeName = invocationInfo.Method.GetGenericArgumentsName().First();
            if (Kernel.CanGet<IWorkflowAction>(typeName))
            {
                var action = Kernel.Get<IWorkflowAction>(typeName);
                var parameter = invocationInfo.Arguments.First() + "";

                Log.Debug($"Invoking workflow action {typeName}");
                if (!action.Evaluate(parameter))
                    return false.ToString();

                Context.AddWorkflowBranch(action.BranchType);
                return true.ToString();
            }
            else
            {
                using (var e = new ActionNotFoundException(typeName))
                    Log.Fatal($"Implementation not found for workflow action {typeName}", e);
            }

            return null;
        }
    }
}
