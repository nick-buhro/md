using System.Globalization;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Lexing
{
    internal struct Token
    {
        public readonly TokenType TokenType;
        public readonly string Lexeme;

        /// <summary>
        /// Value that represents this token. 
        /// </summary> 
        /// <example>
        /// Src line:   "# Header level 1" 
        /// TokenType:  Header1
        /// Lexeme:     "# Header level 1"
        /// Value:      "Header level 1"
        /// </example>
        public readonly string Value;

        public Token(TokenType type, string lexeme)
            : this(type, lexeme, lexeme) { }

        public Token(TokenType type, string lexeme, string value)
        {
            TokenType = type;
            Lexeme = lexeme;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} ({1})",
                TokenType,
                Lexeme);
        }
    }
}
