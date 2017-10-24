using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.Actions
{
    public class Highlight : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(x => Context.Driver.ExecuteScriptWithArguments(ChangeHighlightScript, x));

        readonly string ChangeHighlightScript = JavascriptCode.SetStyle("background-color: yellow;");
    }
}
