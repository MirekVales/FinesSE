using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class GetScreenDiff : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }
        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(string);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>, parameters.ElementAt(1) as string, parameters.Last() as string);

        public string Invoke(IEnumerable<IWebElement> elements, string baseVersionId, string referenceVersionId)
        {
            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, elementId, referenceVersionId);

                return ScreenshotStore.Compare(elementId, baseVersionId, referenceVersionId).ToString();
            }

            return null;
        }
    }
}
