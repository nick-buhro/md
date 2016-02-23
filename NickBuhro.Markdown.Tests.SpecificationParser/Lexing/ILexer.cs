using System.Collections.Generic;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Lexing
{
    internal interface ILexer
    {
        IEnumerable<Token> Scan(string source);
    }
}
