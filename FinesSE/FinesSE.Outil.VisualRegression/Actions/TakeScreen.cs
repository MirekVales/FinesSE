using System;
using System.Collections.Generic;
using FinesSE.Contracts.Invokable;
using OpenQA.Selenium;
using FinesSE.Contracts.Infrastructure;
using System.Linq;
using FinesSE.Core;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class TakeScreen : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }
        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(IEnumerable<IWebElement>);
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>, parameters.Last() as string);

        public string Invoke(IEnumerable<IWebElement> elements, string versionId)
        {
            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, elementId, versionId);

                return ScreenshotStore.GetPath(elementId, versionId);
            }
            return "";
        }
    }
}
