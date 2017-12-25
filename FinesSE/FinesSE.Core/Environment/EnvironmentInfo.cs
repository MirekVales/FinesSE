using System.Collections;
using System.Collections.Generic;
using E = System.Environment;

namespace FinesSE.Core.Environment
{
    public static class EnvironmentInfo
    {
        public static IEnumerable<(string key, string value)> GetInfo()
        {
            foreach (DictionaryEntry pair in E.GetEnvironmentVariables())
                yield return (pair.Key + "", pair.Value + "");
        }
    }
}
