using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Core.Injection
{
    public class InvokationProxy : IInvocationProxy
    {
        public string Invoke<T>(params string[] arguments)
            where T : IAction
        {
            return "";
        }

        public void InvokeVoid<T>(params string[] arguments)
            where T : IVoidAction
        {
        }
    }
}
