using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NickBuhro.Markdown.Tests.SpecificationParser.Lexing;
using NickBuhro.Markdown.Tests.SpecificationParser.Parsing;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Generating
{
    /// <summary>
    /// Strategy class for one generating.
    /// Generate method should be invoked only once. 
    /// </summary>
    internal sealed class CSharpGeneratorStrategy
    {
        private static class Definitions
        {
            public static readonly string Namespace = "NickBuhro.Markdown.Tests";
            public static readonly string ClassNameWithModifiers = "public sealed partial class Specification";
            public static readonly string TestMethodNameFormat = "Example{0:000}";
            public static readonly string HelperMethodName = "ExecuteExampleTest";

            public static readonly string[] Usings =
            {
                //"System",
                //"System.Collections.Generic",
                //"System.Linq",
                //"System.Text",
                //"System.Text.RegularExpressions",
                //"System.Threading.Tasks",
                "Microsoft.VisualStudio.TestTools.UnitTesting"
            };
            
            public static readonly IList<string> ClassAttributes = new List<string>
            {
                "[TestClass]"
            };

            public static readonly IList<string> TestMethodAttributes = new List<string>
            {
                "[TestMethod]",
                "[TestCategory(\"{1}\")]"
            };
        }

        private readonly SpecificationAbstractSyntaxTree _ast;
        
        private CustomStringBuilder _sb;

        public CSharpGeneratorStrategy(SpecificationAbstractSyntaxTree tree)
        {
            _ast = tree;
        }

        public string Generate()
        {
            _sb = new CustomStringBuilder();

            GenerateUsings();
            GenerateNamespace();

            return _sb.ToString();
        }

        private void GenerateUsings()
        {
            if (Definitions.Usings.Length == 0)
                return;

            foreach (var s in Definitions.Usings)
            {
                _sb.Write("using ");
                _sb.Write(s);
                _sb.WriteLine(";");
            }

            _sb.WriteLine();
        }

        private void GenerateNamespace()
        {
            if (string.IsNullOrEmpty(Definitions.Namespace))
            {
                GenerateClass();
                return;
            }

            _sb.Write("namespace ");
            _sb.WriteLine(Definitions.Namespace);
            WriteOpenBracket();
            GenerateClass();
            WriteCloseBracket();
        }

        private void GenerateClass()
        {
            GenerateClassAttributes();
            _sb.WriteLine(Definitions.ClassNameWithModifiers);
            WriteOpenBracket();
            GenerateClassMembers(_ast);
            WriteCloseBracket();
        }

        private void GenerateClassAttributes()
        {
            foreach (var attribute in Definitions.ClassAttributes)
            {
                _sb.WriteLine(attribute);
            }
        }

        private int GenerateClassMembers(NodeCollection root, int nextExampleNumber = 1, string header1 = null, string header2 = null)
        {
            var category = GetCategoryName(header1, header2);

            foreach (var childNode in root.ChildNodes)
            {
                if (childNode is TextNode)
                {
                    WriteComment(childNode.Lexeme);
                    continue;
                }

                var exNode = childNode as ExampleNode;
                if (exNode != null)
                {
                    WriteExample(exNode, nextExampleNumber, category);
                    nextExampleNumber++;
                    continue;
                }

                var h2Node = childNode as Header2Node;
                if (h2Node != null)
                {
                    WriteComment(h2Node.Lexeme);
                    nextExampleNumber = GenerateClassMembers(h2Node, nextExampleNumber, header1, h2Node.Caption);
                    continue;
                }

                var h1Node = childNode as Header1Node;
                if (h1Node != null)
                {
                    WriteComment(h1Node.Lexeme);
                    nextExampleNumber = GenerateClassMembers(h1Node, nextExampleNumber, h1Node.Caption);
                    continue;
                }

                throw new CompilationException();
            }

            return nextExampleNumber;
        }

        private string GetCategoryName(string header1, string header2)
        {
            if (header1 == null) return "";
            if (header2 == null) return header1;
            return header1 + " - " + header2;
        }

        private void WriteOpenBracket()
        {
            _sb.WriteLine("{");
            _sb.IndentLevel++;
        }

        private void WriteCloseBracket()
        {
            _sb.IndentLevel--;
            _sb.WriteLine("}");
        }

        private void WriteComment(string text, bool textIndent = false)
        {
            _sb.Write("// ");
            if (textIndent)
                _sb.Write("    ");
            _sb.WriteLine(text);
        }
        
        private void WriteExample(ExampleNode node, int number, string category)
        {
            WriteMethodAttributes(number, category);

            _sb.Write("public void ");
            _sb.Write(string.Format(Definitions.TestMethodNameFormat, number));
            _sb.WriteLine("()");
            WriteOpenBracket();

            //WriteComment("Example " + number);
            //WriteComment("Section: " + category);
            //WriteComment(null);
            WriteComment("Source:");
            foreach (var s in node.MarkdownNodes)
            {
                WriteComment(s.Lexeme, true);
            }
            WriteComment(null);
            WriteComment("Expected result:");
            foreach (var s in node.HtmlNodes)
            {
                WriteComment(s.Lexeme, true);
            }
            _sb.WriteLine();
            
            _sb.Write(Definitions.HelperMethodName);
            _sb.Write("(");
            _sb.Write(number.ToString());
            _sb.Write(", \"");
            _sb.Write(category);
            _sb.WriteLine("\",");

            _sb.IndentLevel++;

            _sb.Write("\"");
            _sb.Write(Excape(node.MarkdownNodes.Select(n => n.Lexeme)));
            _sb.WriteLine("\",");

            _sb.Write("\"");
            _sb.Write(Excape(node.HtmlNodes.Select(n => n.Lexeme)));
            _sb.WriteLine("\");");

            _sb.IndentLevel--;

            WriteCloseBracket();
        }

        private void WriteMethodAttributes(int number, string category)
        {
            foreach (var attribute in Definitions.TestMethodAttributes)
            {
                _sb.WriteLine(string.Format(attribute, number, category));
            }
        }

        private string Excape(IEnumerable<string> original)
        {
            return string.Join(@"\r\n",
                original.Select(s => s
                    .Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\0", "\\0")
                    .Replace("\a", "\\a")
                    .Replace("\b", "\\b")
                    .Replace("\f", "\\f")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\t", "\\t")
                    .Replace("\v", "\\v")
                    .Replace("→", "\\t")));
        }
    }
}
