using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class Focus : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c == 1)
            .Elements
            .ForEach(x => x.SendKeys(""));
    }
}
