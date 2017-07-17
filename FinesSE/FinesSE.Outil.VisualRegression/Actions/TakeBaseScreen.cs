using System;
using System.Collections.Generic;
using FinesSE.Contracts.Invokable;
using FinesSE.Contracts.Infrastructure;
using System.Linq;
using FinesSE.VisualRegression;
using FinesSE.Core.WebDriver;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class TakeBaseScreen : IAction
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

            var configuration = Context.ConfigurationProvider.Get(VisualRegressionConfiguration.Default);
            var versionId = configuration.ScreenshotStoreBaseVersionId;

            foreach (var element in elements.Elements)
            {
                var screenshot = element.TakeScreenshot(Context.Driver, Context.ConfigurationProvider);
                var elementId = IdentityProvider.GetIdentifier(Context.Driver, elements, element);
                ScreenshotStore.Store(screenshot, Context.TopicId, elementId, versionId);

                return ScreenshotStore.GetPath(Context.TopicId, elementId, configuration.ScreenshotStoreDiffVersionId);
            }
            return "";
        }
    }
}
