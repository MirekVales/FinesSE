namespace FinesSE.Contracts.Exceptions
{
    public class SoapUISuiteException : SlimException
    {
        public SoapUISuiteException(int failed, int passed)
            : base($"SoapUI suite failed. {failed} cases failed, {passed} passed")
        {
        }
    }
}
