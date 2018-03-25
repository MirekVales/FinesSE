using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetElementWidth : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c == 1)
            .Elements
            .First()
            .Size
            .Width
            .ToString();
    }
}