using FinesSE.Contracts.Infrastructure;
using System.Collections.Generic;

namespace FinesSE.Core.Environment
{
    public class ProcessList
    {
        public IEnumerable<ChildProcess> Processes { get; set; }

        public ProcessList()
        {
            Processes = new ChildProcess[0];
        }
    }
}