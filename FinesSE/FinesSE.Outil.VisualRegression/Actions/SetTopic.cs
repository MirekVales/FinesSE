using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinesSE.Outil.VisualRegression.Actions
{
#warning TBR EContext
    public class SetTopic : IAction
    {
        public IWebDriverProvider DriverProvider { get; set; }

        public IEnumerable<Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public string Invoke(params object[] parameters)
            => Invoke((string)parameters.First());

        public string Invoke(string id)
        {
            DriverProvider.TopicId = id;
            return $"Topic set to '{id}'";
        }
    }
}
