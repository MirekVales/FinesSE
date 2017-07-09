using System;

namespace FinesSE.Contracts.Exceptions
{
    public class ActionNotFoundException : Exception
    {
        public ActionNotFoundException(string actionTypeName)
            : base($"Action '{actionTypeName}' is not defined")
        { }
    }
}
