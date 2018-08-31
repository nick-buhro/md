namespace NickBuhro.Markdown.Tests.SpecificationParser.Parsing
{
    interface IParser
    {
        SpecificationAbstractSyntaxTree Parse(string source);
    }
}
