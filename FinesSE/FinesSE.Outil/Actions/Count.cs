using FinesSE.Contracts.Invokable;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Count : IStringAction
    {
        [EntryPoint]
        public string Invoke(LocatedElements elements)
            => elements.Elements.Count() + "";
    }
}
