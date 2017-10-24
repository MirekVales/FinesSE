using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using FinesSE.VisualRegression;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class GetScreenDiff : IStringAction
    {
        public IExecutionContext Context { get; set; }

        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }

        [EntryPoint]
        public string Invoke(LocatedElements elements)
        {
            elements.ConstraintCount(c => c > 0);

            var configuration = Context.ConfigurationProvider.Get(VisualRegressionConfiguration.Default);
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
