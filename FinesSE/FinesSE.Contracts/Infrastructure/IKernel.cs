using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IKernel
    {
        IInvokationProxy SeleneseProvider { get; }

        IExecutionContext Context { get; }

        T Get<T>(string name);

        bool CanGet<T>(string name);

        IEnumerable<ILocator> GetLocators();

        IEnumerable<IParseMethod> GetParserMethods();

        void Initialize();
    }
}