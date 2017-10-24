using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;
using System.Drawing;

namespace FinesSE.Outil.Actions
{
    public class SetWindowSize : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke(int width, int height)
            => Context
                .Driver
                .Manage()
                .Window
                .Size = new Size(width, height);
    }
}
