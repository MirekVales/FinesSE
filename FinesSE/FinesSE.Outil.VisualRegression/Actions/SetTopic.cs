using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class SetTopic : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(string id)
            => Context.SetTopicId(id);
    }
}
