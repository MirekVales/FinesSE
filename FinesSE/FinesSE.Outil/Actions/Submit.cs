using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Submit : IVoidAction
    {
        [EntryPoint]
        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c == 1)
            .Elements
            .First()
            .Submit();
    }
}