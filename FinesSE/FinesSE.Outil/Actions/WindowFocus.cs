using FinesSE.Contracts.Infrastructure;
using FinesSE.Contracts.Invokable;

namespace FinesSE.Outil.Actions
{
    public class WindowFocus : IVoidAction
    {
        public IExecutionContext Context { get; set; }

        [EntryPoint]
        public void Invoke()
            => Context
            .Driver
            .SwitchTo()
            .Window(Context.Driver.CurrentWindowHandle);
    }
}
