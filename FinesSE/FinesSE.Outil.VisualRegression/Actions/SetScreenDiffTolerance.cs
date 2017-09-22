using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.VisualRegression;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
    public class SetScreenDiffTolerance : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IConfigurationProvider ConfigurationProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => ConfigurationProvider
            .Get(VisualRegressionConfiguration.Default)
            .ScreenshotDiffTolerance = parameters.First() as string;
    }
}
