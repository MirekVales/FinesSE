﻿using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class Forward : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke()
            => Context.Driver.Navigate().Forward();
    }
}