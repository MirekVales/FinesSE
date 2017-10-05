using FinesSE.Contracts.Invokable;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IInvocationProxy
    {
        string Invoke<T>(params string[] arguments) where T : IAction;
        void InvokeVoid<T>(params string[] arguments) where T : IVoidAction;
        string InvokeWorkflowExpression<T>(string expression) where T : IWorkflowAction;
    }
}
