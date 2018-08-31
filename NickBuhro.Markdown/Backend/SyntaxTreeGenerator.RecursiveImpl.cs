using System.IO;
using NickBuhro.Markdown.Syntax;

namespace NickBuhro.Markdown.Backend
{
    partial class SyntaxTreeGenerator
    {
        private struct RecursiveImpl
        {
            private readonly Document _sourceDocument;
            private readonly TextWriter _writer;

            public RecursiveImpl(Document sourceDocument, TextWriter writer)
            { 
                _sourceDocument = sourceDocument;
                _writer = writer;
            }

            /// <summary>
            /// Should be invoked only once.
            /// </summary>
            public void Generate()
            {
                _writer.Write(_sourceDocument.ToString());
                Generate(_sourceDocument.Child, 1);
            }

            private void Generate(Element element, int indentLevel)
            {
                if (element == null) return;

                // Write current element value

                _writer.WriteLine();
                for (var i = 0; i < indentLevel; i++)
                    _writer.Write(Indent);
                _writer.Write(element.ToString());

                // Write child element

                Generate(element.Child, indentLevel + 1);

                // Write next sibling element

                Generate(element.NextSibling, indentLevel);
            }
        }
    }
}
