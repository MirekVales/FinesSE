using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Outil.Actions
{
    public class DeleteAllCookies : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke()
            => Context
            .Driver
            .Manage()
            .Cookies
            .DeleteAllCookies();
    }
}
