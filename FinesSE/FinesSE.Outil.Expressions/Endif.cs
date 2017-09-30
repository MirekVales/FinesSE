using FinesSE.Contracts.Invokable;
using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Outil.Expressions
{
    public class Endif : IWorkflowAction
    {
        public BranchType BranchType => BranchType.Close;

        public bool Evaluate(string command)
            => true;
    }
}
