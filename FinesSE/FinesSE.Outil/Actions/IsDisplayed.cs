using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class IsDisplayed : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements locatedElements)
            => locatedElements
                .ConstraintCount(c => c == 1)
                .Elements
                .First()
                .Displayed
                .ToString();
    }
}
