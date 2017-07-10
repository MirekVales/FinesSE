using System;

namespace FinesSE.Contracts
{
    [Flags]
    public enum WebDrivers
    {
        Default         = 1,
        AllAvailable    = 2,
        Random          = 4,
        Custom          = 8,

        Chrome          = 16,
        Edge            = 32,
        FireFox         = 64,
        IE              = 128,
        Opera           = 256,
        Safari          = 512
    }
}
