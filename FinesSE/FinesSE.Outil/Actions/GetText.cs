using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetText : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c == 1)
            .Elements
            .First()
            .Text;
    }
}