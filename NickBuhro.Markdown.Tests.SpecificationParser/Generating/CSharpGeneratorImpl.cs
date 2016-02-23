using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NickBuhro.Markdown.Tests.SpecificationParser.Parsing;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Generating
{
    /// <summary>
    /// Strategy class for one generating.
    /// Generate method should be invoked only once. 
    /// </summary>
    internal sealed class CSharpGeneratorImpl
    {
        private readonly SpecificationAbstractSyntaxTree _ast;

        public CSharpGeneratorImpl(SpecificationAbstractSyntaxTree tree)
        {
            _ast = tree;
        }

        public string Generate()
        {
            
        }
    }
}
