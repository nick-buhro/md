using NickBuhro.Markdown.Tests.SpecificationParser.Lexing;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Parsing
{
    internal sealed class Parser: IParser
    {
        private readonly ILexer _lexer;

        public Parser()
            : this(new Lexer()) { }

        public Parser(ILexer lexer)
        {
            _lexer = lexer;
        }

        public SpecificationAbstractSyntaxTree Parse(string source)
        {
            var result = new SpecificationAbstractSyntaxTree();
            Header1Node currentHeader1 = null;
            Header2Node currentHeader2 = null;
            ExampleNode currentExample = null;

            foreach (var token in _lexer.Scan(source))
            {
                // Example section
                if (currentExample != null)
                {
                    switch (token.TokenType)
                    {
                        case TokenType.ExampleMarkdownLine:
                            currentExample.MarkdownNodes.Add(new MarkdownExampleNode(token.Lexeme));
                            continue;
                        case TokenType.ExampleHtmlLine:
                            currentExample.HtmlNodes.Add(new HtmlExampleNode(token.Lexeme));
                            continue;
                        case TokenType.ExampleDelim:
                            continue;
                        case TokenType.ExampleEnd:
                            currentExample = null;
                            continue;
                        default:
                            throw new CompilationException();
                    }
                }

                // Header2 section
                if (currentHeader2 != null)
                {
                    switch (token.TokenType)
                    {
                        case TokenType.TextLine:
                            currentHeader2.ChildNodes.Add(new TextNode(token.Lexeme));
                            continue;
                        case TokenType.Header1:
                            currentHeader2 = null;
                            currentHeader1 = new Header1Node(token.Lexeme, token.Value);
                            result.ChildNodes.Add(currentHeader1);
                            continue;
                        case TokenType.Header2:
                            currentHeader2 = new Header2Node(token.Lexeme, token.Value);
                            currentHeader1.ChildNodes.Add(currentHeader2);
                            continue;
                        case TokenType.ExampleStart:
                            currentExample = new ExampleNode();
                            currentHeader2.ChildNodes.Add(currentExample);
                            continue;
                        default:
                            throw new CompilationException();
                    }
                }

                // Header1 section
                if (currentHeader1 != null)
                {
                    switch (token.TokenType)
                    {
                        case TokenType.TextLine:
                            currentHeader1.ChildNodes.Add(new TextNode(token.Lexeme));
                            continue;
                        case TokenType.Header1:
                            currentHeader1 = new Header1Node(token.Lexeme, token.Value);
                            result.ChildNodes.Add(currentHeader1);
                            continue;
                        case TokenType.Header2:
                            currentHeader2 = new Header2Node(token.Lexeme, token.Value);
                            currentHeader1.ChildNodes.Add(currentHeader2);
                            continue;
                        case TokenType.ExampleStart:
                            currentExample = new ExampleNode();
                            currentHeader1.ChildNodes.Add(currentExample);
                            continue;
                        default:
                            throw new CompilationException();
                    }
                }

                // Root section
                switch (token.TokenType)
                {
                    case TokenType.TextLine:
                        result.ChildNodes.Add(new TextNode(token.Lexeme));
                        continue;
                    case TokenType.Header1:
                        currentHeader1 = new Header1Node(token.Lexeme, token.Value);
                        result.ChildNodes.Add(currentHeader1);
                        continue;
                    case TokenType.ExampleStart:
                        currentExample = new ExampleNode();
                        result.ChildNodes.Add(currentExample);
                        continue;
                    default:
                        throw new CompilationException();
                }
            }

            return result;
        }
    }
}
