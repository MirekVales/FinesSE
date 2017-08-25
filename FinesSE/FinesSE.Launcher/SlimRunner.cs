using FinesSE.Launcher.Formats;
using FinesSE.Launcher.Infrastructure;
using fitSharp.Fit.Fixtures;
using fitSharp.Fit.Model;
using fitSharp.Fit.Operators;
using fitSharp.Fit.Service;
using fitSharp.IO;
using fitSharp.Machine.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinesSE.Launcher
{
    public class SlimRunner
    {
        private readonly IEnumerable<string> assemblies;
        private readonly IEnumerable<string> namespaces;
        private readonly FormatConvertor convertor;

        public SlimRunner(IEnumerable<string> assemblies, IEnumerable<string> namespaces)
        {
            this.assemblies = assemblies;
            this.namespaces = namespaces;
            convertor = new FormatConvertor();
        }

        public async Task ExecuteAsync(FileInfo inputPath, TableFormat inputFormat, string outputFilePath)
        {
            var inputData = File.ReadAllText(inputPath.FullName);
            var result = await ExecuteAsync(convertor.Convert(inputData, inputFormat), System.IO.Path.GetFileName(inputPath.FullName));
            File.WriteAllText(outputFilePath, result);
        }

        public async Task<string> ExecuteAsync(string inputData, TableFormat inputFormat, string inputAnnotation)
        {
            return await ExecuteAsync(convertor.Convert(inputData, inputFormat), inputAnnotation);
        }

        private Task<string> ExecuteAsync(string input, string inputAnnotation)
        {
            CreateStoryTest(input, out StoryTestStringWriter writer, out StoryTest storyTest);

            var elapsedTime = new ElapsedTime();
            storyTest.Execute();

            var result = new PageResult(inputAnnotation, writer.Tables, writer.Counts, elapsedTime);
            Console.WriteLine(FormatInfo(result));

            return Task.FromResult(result.Content);
        }

        private string FormatInfo(PageResult result)
            => $"{result.ElapsedTime}\t{result.Title}\t{result.TestCounts.Description}";

        private void CreateStoryTest(string input, out StoryTestStringWriter writer, out StoryTest storyTest)
        {
            CellProcessorBase processor = CreateProcessor();

            writer = new StoryTestStringWriter(processor);
            storyTest = new StoryTest(processor, writer).WithInput(input);

            if (!storyTest.IsExecutable)
                throw new InvalidFormatException("Input content is not in executable format");
        }

        private CellProcessorBase CreateProcessor()
        {
            var memory = new TypeDictionary();
            var processor = new CellProcessorBase(memory, memory.GetItem<CellOperators>());
            processor.ApplicationUnderTest.AddAssemblies(assemblies);
            namespaces.ToList().ForEach(processor.AddNamespace);
            return processor;
        }
    }
}
