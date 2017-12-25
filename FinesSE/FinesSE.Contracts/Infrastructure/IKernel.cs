using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IKernel
    {
        IInvocationProxy Proxy { get; }

        IExecutionContext Context { get; }

        T Get<T>(string name);

        TService Get<T, TService>(T value, string name);

        TService Get<T1, T2, TService>(T1 value, T2 value2, string name);

        bool CanGet<T>(string name);

        IEnumerable<ILocator> GetLocators();

        IEnumerable<IParseMethod> GetParserMethods();

        IEnumerable<IWebDriverActivator> GetWebDriverActivators();

        void Initialize();

        void DisposeKernel();
    }
}