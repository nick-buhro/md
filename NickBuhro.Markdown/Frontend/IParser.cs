using System.IO;
using NickBuhro.Markdown.Syntax;

namespace NickBuhro.Markdown.Frontend
{
    public interface IParser
    {
        Document Parse(TextReader source);
    }
}
