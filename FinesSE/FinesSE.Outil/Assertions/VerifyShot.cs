using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.Assertions
{
    public class VerifyShot : IVoidAction
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

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as IEnumerable<IWebElement>, parameters.ElementAt(1) as string, parameters.Last() as string);

        public void Invoke(IEnumerable<IWebElement> elements, string baseVersionId, string referenceVersionId)
        {
            foreach (var element in elements)
            {
                var screenshot = element.TakeScreenshot(DriverProvider.Get());
                var elementId = IdentityProvider.GetIdentifier(DriverProvider.Get(), element);
                ScreenshotStore.Store(screenshot, elementId, referenceVersionId);
                
                if (ScreenshotStore.Compare(elementId, baseVersionId, referenceVersionId) > 0)
                {
                    throw new Exception();
                }
            }
        }
    }
}
