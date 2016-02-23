using System.Collections;
using System.Collections.Generic;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Lexing
{
    internal sealed class Lexer: ILexer
    {
        public IEnumerable<Token> Scan(string source)
        {
            return new EnumeratorFactory(source);
        }
        
        private class EnumeratorFactory: IEnumerable<Token>
        {
            private readonly string _src;

            public EnumeratorFactory(string source)
            {
                _src = source;
            }

            public IEnumerator<Token> GetEnumerator()
            {
                return new LexerEnumerator(_src);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
