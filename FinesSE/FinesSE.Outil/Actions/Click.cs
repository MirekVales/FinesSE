using System.Collections.Generic;
using System.Linq;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class Click : IVoidAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First());

        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ToList()
            .ForEach(x => x.Click());
    }
}
