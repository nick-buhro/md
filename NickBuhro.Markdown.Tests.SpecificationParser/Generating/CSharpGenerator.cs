using NickBuhro.Markdown.Tests.SpecificationParser.Parsing;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Generating
{
    internal sealed class CSharpGenerator: IGenerator
    {
        private readonly IParser _parser;

        public CSharpGenerator()
            : this (new Parser()) { }

        public CSharpGenerator(IParser parser)
        {
            _parser = parser;
        }
        
        public string Generate(string source)
        {
            var ast = _parser.Parse(source);
            var g = new CSharpGeneratorImpl(ast);
            return g.Generate();
        }
    }
}
