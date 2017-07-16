using FinesSE.Contracts.Invokable;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinesSE.Outil.Actions
{
    public class Pause : IVoidAction
    {
        public IEnumerable<System.Type> GetParameterTypes()
        {
            yield return typeof(int);
        }

        public void Invoke(params object[] parameters)
            => Invoke(parameters.Cast<int>().First());

        public void Invoke(int ms)
            => Task.Delay(ms).Wait();
    }
}
