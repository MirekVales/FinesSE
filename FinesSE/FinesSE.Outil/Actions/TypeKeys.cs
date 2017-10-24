using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class TypeKeys : IVoidAction
    {
        [EntryPoint]
        public void Invoke(LocatedElements elements, string keys)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(x => x.SendKeys(keys));
    }
}