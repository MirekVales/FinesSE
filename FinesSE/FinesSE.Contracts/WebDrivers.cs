using FinesSE.Contracts.Infrastructure;
using System;

namespace FinesSE.Contracts
{
    [Flags]
    public enum WebDrivers
    {
        [Dynamic]
        Default         = 1,
        [Dynamic]
        AllAvailable    = 2,
        [Dynamic]
        Random          = 4,
        Custom          = 8,

        Chrome          = 16,
        Edge            = 32,
        Firefox         = 64,
        IE              = 128,
        Opera           = 256,
        PhantomJS       = 512,
        Remote          = 1024,
        Safari          = 2048
    }
}
