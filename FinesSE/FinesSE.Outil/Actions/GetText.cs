using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetText : IAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First());

        public string Invoke(LocatedElements elements)
            => elements.Elements.First().Text;
    }
}
