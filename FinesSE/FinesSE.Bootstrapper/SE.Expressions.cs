using FinesSE.Outil.Expressions;

namespace FinesSE.Bootstrapper
{
    public partial class SE
    {
        public string Do(string command)
            => p.Invoke<Do>(command);

        public void If(string expression)
            => p.InvokeWorkflowExpression<If>(expression);

        public void Endif()
            => p.InvokeWorkflowExpression<Endif>("");
    }
}
