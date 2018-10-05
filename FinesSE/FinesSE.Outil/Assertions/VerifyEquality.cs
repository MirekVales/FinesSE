﻿using FinesSE.Contracts;
using FinesSE.Contracts.Exceptions;
using FinesSE.Contracts.Invokable;
using System.Collections.Generic;

namespace FinesSE.Outil.Assertions
{
    public class VerifyEquality : IStringAction, IReportable
    {
        public string Name { get; } = "Verify Equality";
        public string Description { get; }
        public IEnumerable<string> Category { get; } = new[] { IdTag.ReportableCategory };

        [EntryPoint]
        public string Invoke(string expected, string actual)
        {
            if (expected != actual)
                throw new AssertionException(expected, actual, WebDrivers.Default);

            return "true";
        }
    }
}
