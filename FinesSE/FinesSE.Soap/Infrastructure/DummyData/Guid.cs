using System.Collections.Generic;

namespace FinesSE.Soap.Infrastructure.DummyData
{
    class Guid : IDummyDataProvider
    {
        public IEnumerable<string> Name
        {
            get
            {
                yield return "guid";
                yield return "g";
            }
        }

        public string Get()
            => System.Guid.NewGuid().ToString();
    }
}
