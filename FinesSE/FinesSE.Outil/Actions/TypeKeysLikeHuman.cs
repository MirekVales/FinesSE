using System;
using System.Threading.Tasks;
using FinesSE.Contracts.Invokable;
using FinesSE.Core.WebDriver;
using OpenQA.Selenium;

namespace FinesSE.Outil.Actions
{
    public class TypeKeysLikeHuman : IVoidAction
    {
        [EntryPoint]
        public void Invoke(LocatedElements elements, string keys)
            => elements
                .ConstraintCount(c => c > 0)
                .Elements
                .ForEach(e => TypeLikeHuman(e, keys));

        void TypeLikeHuman(IWebElement element, string keys)
        {
            var random = new Random(DateTime.Now.Millisecond);
            foreach (var @char in keys.ToCharArray())
            {
                element.SendKeys(@char.ToString());
                Task.Delay(random.Next(50, 550)).Wait();
            }
        }
    }
}