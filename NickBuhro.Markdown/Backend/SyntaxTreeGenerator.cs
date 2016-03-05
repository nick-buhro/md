using System.IO;
using NickBuhro.Markdown.Syntax;

namespace NickBuhro.Markdown.Backend
{
    /// <summary>
    /// Generates text representation of syntax tree.
    /// </summary>
    public sealed partial class SyntaxTreeGenerator: IGenerator
    {
        private const string Indent = "  ";

        public void Generate(Document sourceDocument, TextWriter writer)
        {
            var impl = new RecursiveImpl(sourceDocument, writer);
            impl.Generate();
        }
    }
}
