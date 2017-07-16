using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class SetTopic : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke((string)parameters.First());

        public void Invoke(string id)
            => Context.SetTopicId(id);
    }
}
