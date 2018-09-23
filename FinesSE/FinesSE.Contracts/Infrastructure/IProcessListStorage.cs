using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IProcessListStorage
    {
        void AddProcesses(IEnumerable<ChildProcess> processes);
        int CleanList();
    }
}