using System.IO;
using NickBuhro.Markdown.Syntax;

namespace NickBuhro.Markdown.Backend
{
    public interface IGenerator
    {
        void Generate(Document sourceDocument, TextWriter writer);
    }
}
