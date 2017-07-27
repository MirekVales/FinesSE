using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class Highlight : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<LocatedElements>().First());

        public void Invoke(LocatedElements elements)
            => elements
            .ConstraintCount(c => c > 0)
            .Elements
            .ForEach(x => Context.Driver.ExecuteScriptWithArguments(ChangeHighlightScript, x));

        private readonly string ChangeHighlightScript = JavascriptCode.SetStyle("background-color: yellow;");
    }
}
