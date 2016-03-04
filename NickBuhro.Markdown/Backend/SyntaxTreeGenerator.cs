using System.IO;
using NickBuhro.Markdown.Syntax;

namespace NickBuhro.Markdown.Backend
{
    /// <summary>
    /// Generates text representation of syntax tree.
    /// </summary>
    public sealed class SyntaxTreeGenerator: IGenerator
    {
        private const string Indent = "  ";

        public void Generate(Document sourceDocument, TextWriter writer)
        {
            writer.WriteLine(sourceDocument.ToString());
            Generate(sourceDocument.Child, writer, 1);
        }

        private static void Generate(Element block, TextWriter writer, int indent)
        {
            WriteIndent(writer, indent);
            writer.Write(block.ToString());
            
            if (block.Child != null)
            {
                writer.WriteLine();
                Generate(block.Child, writer, indent + 1);
            }

            if (block.NextSibling != null)
            {
                writer.WriteLine();
                Generate(block.NextSibling, writer, indent);
            }
        }

        private static void WriteIndent(TextWriter writer, int indentLevel)
        {
            for (var i = 0; i < indentLevel; i++)
            {
                writer.Write(Indent);
            }
        }
    }
}
