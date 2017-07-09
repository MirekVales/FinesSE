using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Contracts.Infrastructure
{
    public interface IKernel
    {
        ISeleneseProxy SeleneseProvider { get; }

        IWebDriverProvider WebDriverProvider { get; }

        T Get<T>(string name);

        bool CanGet<T>(string name);

        IEnumerable<ILocator> GetLocators();

        void AddAction<T>(string serviceName) where T : IAction;

        void Initialize();
    }
}