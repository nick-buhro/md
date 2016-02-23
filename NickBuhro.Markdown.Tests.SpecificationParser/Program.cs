using System;
using System.IO;
using System.Net;
using System.Text;
using NickBuhro.Markdown.Tests.SpecificationParser.Generating;

namespace NickBuhro.Markdown.Tests.SpecificationParser
{
    internal sealed class Program
    {
        private const string SpecSource = @"https://raw.githubusercontent.com/jgm/CommonMark/master/spec.txt";

        public static void Main()
        {
            var source = GetSource();
            
            var gen = new CSharpGenerator();
            var result = gen.Generate(source);
            File.WriteAllText("Specification.generated.cs", result);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static string GetSource()
        {
            using (var client = new WebClient() {Encoding = Encoding.UTF8})
            {
                return client.DownloadString(SpecSource);
            }
        }
    }
}
