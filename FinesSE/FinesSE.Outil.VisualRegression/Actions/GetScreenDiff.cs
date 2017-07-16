using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class GetScreenDiff : IAction
    {
        public IExecutionContext Context { get; set; }

        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements);

        public string Invoke(LocatedElements elements)
        {
            elements.ConstraintCount(c => c > 0);

            var configuration = Context.ConfigurationProvider.Get(Configuration.Default);
            var baseVersionId = configuration.ScreenshotStoreBaseVersionId;
            var referenceVersionId = configuration.ScreenshotStoreReferenceVersionId;

            foreach (var element in elements.Elements)
            {
                var screenshot = element.TakeScreenshot(Context.Driver, Context.ConfigurationProvider);
                var elementId = IdentityProvider.GetIdentifier(Context.Driver, elements, element);
                ScreenshotStore.Store(screenshot, Context.TopicId, elementId, referenceVersionId);

                return ScreenshotStore.Compare(Context.TopicId, elementId, baseVersionId, referenceVersionId).ToString();
            }

            return null;
        }
    }
}
