using FinesSE.Contracts.Invokable;

namespace FinesSE.Contracts.Infrastructure
{
    public interface ISeleneseProxy
    {
        string Invoke<T>(params string[] arguments) where T : IAction;
        void InvokeVoid<T>(params string[] arguments) where T : IVoidAction;
    }
}
