using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;

namespace FinesSE.Core.Injection
{
    public class SeleneseProxy : ISeleneseProxy
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
