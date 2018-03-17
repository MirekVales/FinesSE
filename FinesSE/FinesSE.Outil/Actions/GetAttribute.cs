using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetAttribute : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements locatedElements, string attributeName)
            => locatedElements
                .ConstraintCount(c => c == 1)
                .Elements
                .First()
                .GetAttribute(attributeName)
                .ToString();
    }
}
