using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FinesSE.Outil.Actions
{
    public class SetZoom : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(string);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.First() as string);

        public void Invoke(string zoomLevelText)
        {
            var zoomLevel = Regex.Match(zoomLevelText, "\\d{1,3}");
            Context.Driver.SetZoomLevel(int.Parse(zoomLevel.Value));
        }
    }
}
