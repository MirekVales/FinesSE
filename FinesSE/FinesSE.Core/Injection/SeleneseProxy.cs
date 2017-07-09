using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Core.Injection
{
    public class SeleneseProxy : ISeleneseProxy
    {
        public string Invoke<T>(params string[] arguments)
        {
            return "";
        }
    }
}
