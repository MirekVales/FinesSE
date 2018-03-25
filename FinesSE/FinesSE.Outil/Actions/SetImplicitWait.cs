using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;

namespace FinesSE.Outil.Actions
{
    public class SetImplicitWait : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string msTimeout)
            => Context
            .Driver
            .Manage()
            .Timeouts()
            .ImplicitWait
            = TimeSpan.FromMilliseconds(int.Parse(msTimeout));
    }
}