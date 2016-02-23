namespace NickBuhro.Markdown.Tests.SpecificationParser.Lexing
{
    internal enum TokenType: byte
    {
        TextLine = 0,
        Header1,
        Header2,
        ExampleStart,
        ExampleMarkdownLine,
        ExampleDelim,
        ExampleHtmlLine,
        ExampleEnd
    }
}
