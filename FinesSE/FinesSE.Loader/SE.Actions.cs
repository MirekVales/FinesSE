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

        public void Open(string url)
            => se.InvokeVoid<Open>(url);

        public void Pause(string ms)
            => se.InvokeVoid<Pause>(ms);

        public void Refresh()
            => se.InvokeVoid<Refresh>();

        public void Type(string locator, string keys)
            => se.InvokeVoid<Type>(locator, keys);
    }
}
