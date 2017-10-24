using FinesSE.Contracts.Invokable;
using FinesSE.Expressions.Contracts;

namespace FinesSE.Outil.Expressions
{
    public class Do : IStringAction
    {
        public IExpressionEngine Engine { get; set; }

        [EntryPoint]
        public string Invoke(string script)
            => Engine.Execute(script) + "";
    }
}
