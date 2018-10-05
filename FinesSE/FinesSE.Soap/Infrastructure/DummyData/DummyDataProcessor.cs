using Bogus;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FinesSE.Soap.Infrastructure.DummyData
{
    public class DummyDataProcessor : IDummyDataProcessor
    {
        readonly HashSet<IDummyDataProvider> providers;
        readonly Faker faker;
        
        public DummyDataProcessor()
        {
            providers = new HashSet<IDummyDataProvider>()
            {
                new Guid()
            };
            faker = new Faker();
        }

        bool TryGetMatchingProvider(string name, out IDummyDataProvider provider)
            => (provider = providers.FirstOrDefault(p => p.Name.Contains(name.ToLower()))) != null;

        public string ProcessMessage(string content)
        {
            const string Pattern = @"\$=\{(.+?)\}(?!([\{, \}]))";
            Match match;
            while ((match = Regex.Match(content, Pattern)).Success)
            {
                var newValue = "";
                if (TryGetMatchingProvider(match.Groups[1].Value, out IDummyDataProvider provider))
                    newValue = provider.Get();
                else
                    newValue = faker.Parse(match.Groups[1].Value);

                content = content.Remove(match.Index, match.Length);
                content = content.Insert(match.Index, newValue);
            }

            return content;
        }
    }
}
