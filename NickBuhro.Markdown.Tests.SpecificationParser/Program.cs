using System;
using System.Net;
using System.Text;
using NickBuhro.Markdown.Tests.SpecificationParser.Lexing;

namespace NickBuhro.Markdown.Tests.SpecificationParser
{
    internal sealed class Program
    {
        private const string SpecSource = @"https://raw.githubusercontent.com/jgm/CommonMark/master/spec.txt";

        public static void Main()
        {
            var source = GetSource();
            var lexer = new Lexer();
            foreach (var token in lexer.Scan(source))
            {
                Console.WriteLine(token);
            }
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
