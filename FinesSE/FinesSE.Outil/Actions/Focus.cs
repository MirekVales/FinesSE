using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Focus : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First());

        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c == 1)
            .Elements
            .ToList()
            .ForEach(x => x.SendKeys(""));
    }
}
