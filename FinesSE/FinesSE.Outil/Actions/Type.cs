using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class Type : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements, string value)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(
                x => Context
                    .Driver
                    .ExecuteScriptWithArguments(JavascriptCode.SetValue(value), x));
    }
}
