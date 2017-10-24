using System;
using System.Linq;

namespace FinesSE.Contracts.Exceptions
{
    public class MethodNotFoundException : SlimException
    {
        public MethodNotFoundException(string actionName, params Type[] types)
            : base($"Method ({string.Join(",", types.Select(t => t.Name))}) " +
                  $"for action '{actionName}' is not defined")
        { }
    }
}