using System;

namespace FinesSE.Reports.Infrastructure
{
    [Flags]
    public enum Tags
    {
        Category = 1,
        Url = 2,
        Topic = 4
    }
}
