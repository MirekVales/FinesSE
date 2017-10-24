using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class DeleteCookieNamed : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string cookieName)
            => Context
            .Driver
            .Manage()
            .Cookies
            .DeleteCookieNamed(cookieName);
    }
}
