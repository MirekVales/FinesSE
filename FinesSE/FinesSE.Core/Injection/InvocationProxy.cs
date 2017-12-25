using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Core.Injection
{
    public class InvocationProxy : IInvocationProxy
    {
        public string Invoke<T>(params string[] arguments)
            where T : IStringAction
            => "";

        public void InvokeVoid<T>(params string[] arguments)
            where T : IVoidAction
        {}

        public string InvokeWorkflowExpression<T>(string expression)
            where T : IWorkflowAction
            => "";
    }
}
