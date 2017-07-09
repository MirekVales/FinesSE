using FinesSE.Outil.Actions;

namespace FinesSE.Loader
{
    public partial class SE
    {
        public void Click(string locator)
            => se.Invoke<Click>(locator);

        public void ClickAt(string locator, string coordinates)
            => se.Invoke<ClickAt>(locator, coordinates);

        public void Close()
            => kernel.WebDriverProvider.Dispose();

        public string GetText(string locator)
            => se.Invoke<GetText>(locator);

        public void Go(string url)
            => se.Invoke<Go>(url);

        public void Pause(string ms)
            => se.Invoke<Pause>(ms);

        public void TypeKeys(string locator, string keys)
            => se.Invoke<TypeKeys>(locator, keys);
    }
}
