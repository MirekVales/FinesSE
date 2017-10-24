using FinesSE.Contracts.Invokable;
using System.Threading.Tasks;

namespace FinesSE.Outil.Actions
{
    public class Pause : IVoidAction
    {
        [EntryPoint]
        public void Invoke(int ms)
            => Task.Delay(ms).Wait();
    }
}
