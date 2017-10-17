using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FinesSE.Outil.Actions
{
    public class SetWindowSize : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(int);
            yield return typeof(int);
        }

        public void Invoke(params object[] parameters)
            => Invoke((int)parameters.First(), (int)parameters.Last());

        public void Invoke(int width, int height)
            => Context
                .Driver
                .Manage()
                .Window
                .Size = new Size(width, height);
    }
}
