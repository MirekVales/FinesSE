namespace FinesSE.Contracts.Exceptions
{
    public class ActionNotFoundException : SlimException
    {
        public ActionNotFoundException(string actionTypeName)
            : base($"Action '{actionTypeName}' is not defined")
        { }
    }
}
