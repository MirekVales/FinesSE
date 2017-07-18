using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.VisualRegression;
using FinesSE.VisualRegression.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class InlineScreenDiff : IAction
    {
        public IExecutionContext Context { get; set; }

        public IScreenshotStore ScreenshotStore { get; set; }
        public IWebElementIdentityProvider IdentityProvider { get; set; }
        public IImageInliner ImageInliner { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(LocatedElements);
        }

        public string Invoke(params object[] parameters)
            => Invoke(parameters.First() as LocatedElements);

        public string Invoke(LocatedElements elements)
        {
            elements.ConstraintCount(c => c == 1);

            var configuration = Context.ConfigurationProvider.Get(VisualRegressionConfiguration.Default);
            var deltaVersionId = configuration.ScreenshotStoreDiffVersionId;
            var elementId = IdentityProvider.GetIdentifier(Context.Driver, elements, elements.Elements.First());
            var image = ScreenshotStore.GetPath(Context.TopicId, elementId, deltaVersionId);

            const long BYTE_LIMIT = 0x186A0;
            if (ImageInliner.TryInlineImage(image, "400", BYTE_LIMIT, out string base64))
                return base64;
            else
                return $"Image has reached a size limit of {BYTE_LIMIT} bytes";
        }
}
}
