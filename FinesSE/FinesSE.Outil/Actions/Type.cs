using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Type : IVoidAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements, parameters.Last() as string);

        public void Invoke(LocatedElements elements, string keys)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(x => x.SendKeys(keys));
    }
}
