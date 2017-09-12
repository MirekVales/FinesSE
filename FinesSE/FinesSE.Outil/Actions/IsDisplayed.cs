using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class IsDisplayed : IAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public string Invoke(params object[] parameters)
            => (parameters.First() as LocatedElements)
                .ConstraintCount(c => c == 1)
                .Elements
                .First()
                .Displayed
                .ToString();
    }
}
