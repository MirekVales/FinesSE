using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class Clear : IVoidAction
    {
        [EntryPoint]
        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(x => x.Clear());
    }
}