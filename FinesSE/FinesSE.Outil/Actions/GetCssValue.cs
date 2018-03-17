using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetCssValue : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements locatedElements, string propertyName)
            => locatedElements
                .ConstraintCount(c => c == 1)
                .Elements
                .First()
                .GetCssValue(propertyName);
    }
}