using FinesSE.Contracts.Exceptions;
using FinesSE.Outil.Actions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void Click(string locator)
            => se.InvokeVoid<Click>(locator);

        public void ClickAt(string locator, string coordinates)
            => se.InvokeVoid<ClickAt>(locator, coordinates);

        public void Close()
            => kernel.WebDriverProvider.Dispose();

        public string GetText(string locator)
            => se.Invoke<GetText>(locator);

        public void Go(string url)
            => se.InvokeVoid<Go>(url);

        public void Pause(string ms)
            => se.InvokeVoid<Pause>(ms);

        public void TypeKeys(string locator, string keys)
            => se.InvokeVoid<TypeKeys>(locator, keys);
    }
}
