﻿using fitSharp.Fit.Fixtures;
using fitSharp.Fit.Model;
using fitSharp.Fit.Operators;
using fitSharp.Fit.Service;
using fitSharp.IO;
using fitSharp.Machine.Engine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinesSE.Launcher
{
    public static class SlimRunner
    {
        public static async Task ExecuteAsync(IEnumerable<string> assemblies, IEnumerable<string> namespaces, FileInfo inputPath, string outputFilePath)
        {
            var result = await Execute(assemblies, namespaces, File.ReadAllText(inputPath.FullName));
            File.WriteAllText(outputFilePath, result);
        }

        private static Task<string> Execute(IEnumerable<string> assemblies, IEnumerable<string> namespaces, string input)
        {
            CellProcessorBase processor = CreateProcessor(assemblies, namespaces);

            var writer = new StoryTestStringWriter(processor);
            var storyTest = new StoryTest(processor, writer)
                .WithInput(new TableConvertor().ConvertToHtmlTables(input));

            if (!storyTest.IsExecutable)
                throw new InvalidFormatException("Input content is not in executable format");

            var elapsedTime = new ElapsedTime();
            storyTest.Execute();

            return Task.FromResult(new PageResult("Result", writer.Tables, writer.Counts, elapsedTime).Content);
        }

        private static CellProcessorBase CreateProcessor(IEnumerable<string> assemblies, IEnumerable<string> namespaces)
        {
            var memory = new TypeDictionary();
            var processor = new CellProcessorBase(memory, memory.GetItem<CellOperators>());
            processor.ApplicationUnderTest.AddAssemblies(assemblies);
            namespaces.ToList().ForEach(processor.AddNamespace);
            return processor;
        }
    }
}
