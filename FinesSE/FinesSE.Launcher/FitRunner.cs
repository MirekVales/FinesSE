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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinesSE.Launcher
{
    public class FitRunner
    {
        readonly IEnumerable<string> assemblies;
        readonly IEnumerable<string> namespaces;
        readonly FormatConvertor convertor;

        public FitRunner(IEnumerable<string> assemblies, IEnumerable<string> namespaces)
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

        string ApplyInterposedArguments(string input)
        {
            const string tdPattern = @"<td>.*?<\/td>";
            const string interposeCharacter = ";";

            var builder = new StringBuilder();
            foreach (var line in input.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                var tdIndex = 0;
                foreach (Match td in Regex.Matches(line, tdPattern))
                {
                    if (tdIndex == 0 && !td.Value.Contains(interposeCharacter))
                    {
                        builder.Append(line);
                        builder.AppendLine();
                        break;
                    }
                    else if (tdIndex == 0)
                        builder.Append("<tr>");

                    builder.Append(td.Value);

                    if (tdIndex != 0)
                        builder.Append(@"<td></td>");

                    tdIndex++;
                }

                builder.Append("</tr>");
                builder.AppendLine();
            }

            return builder.ToString();
        }

        Task<string> ExecuteAsync(string input, string inputAnnotation)
        {
            CreateStoryTest(ApplyInterposedArguments(input), out StoryTestStringWriter writer, out StoryTest storyTest);

            var elapsedTime = new ElapsedTime();
            storyTest.Execute();

            var result = new PageResult(inputAnnotation, writer.Tables, writer.Counts, elapsedTime);
            Console.WriteLine(FormatInfo(result));

            return Task.FromResult(result.Content);
        }

        string FormatInfo(PageResult result)
            => $"{result.ElapsedTime}\t{result.Title}\t{result.TestCounts.Description}";

        void CreateStoryTest(string input, out StoryTestStringWriter writer, out StoryTest storyTest)
        {
            CellProcessorBase processor = CreateProcessor();

            writer = new StoryTestStringWriter(processor);
            storyTest = new StoryTest(processor, writer).WithInput(input);

            var tree = new fitSharp.Slim.Model.SlimTree();

            if (!storyTest.IsExecutable)
                throw new InvalidFormatException("Input content is not in executable format");
        }

        CellProcessorBase CreateProcessor()
        {
            var memory = new TypeDictionary();
            var processor = new CellProcessorBase(memory, memory.GetItem<CellOperators>());
            processor.ApplicationUnderTest.AddAssemblies(assemblies);
            processor.AddNamespace("fitnesse.slim.test");
            namespaces.ToList().ForEach(processor.AddNamespace);
            return processor;
        }
    }
}
