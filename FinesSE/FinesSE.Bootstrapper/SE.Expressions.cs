namespace FinesSE.Bootstrapper
{
    using FinesSE.Outil.Expressions;

    public partial class SE
    {
        public string Do(string command)
            => p.Invoke<Do>(command);

        public string If(string expression)
            => p.InvokeWorkflowExpression<If>(expression);

        public void Endif()
            => p.InvokeWorkflowExpression<Endif>("");

        public string Run(string filePath, string arguments)
            => p.Invoke<Run>(filePath, arguments);

        public void Import(string path)
            => p.InvokeVoid<Import>(path);
    }
}