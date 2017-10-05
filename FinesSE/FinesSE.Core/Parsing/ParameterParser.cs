using FinesSE.Contracts.Infrastructure;

namespace FinesSE.Core.Parsing
{
    public class ParameterParser : BaseParameterParser
    {
        public ParameterParser(IKernel kernel)
        {
            foreach(var parseMethod in kernel.GetParserMethods())
            {
                Set(parseMethod.ParsedType, parseMethod.Invoke);
            }
        }
    }
}