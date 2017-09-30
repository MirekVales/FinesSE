using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Contracts.Invokable
{
    public interface IWorkflowAction
    {
        BranchType BranchType { get; }

        bool Evaluate(string command);
    }
}
