using System.Collections.Generic;
using System.Globalization;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Parsing
{
    #region Abstract syntax types

    internal abstract class Node
    {
        public string Lexeme;

        protected Node(string lexeme)
        {
            Lexeme = lexeme;
        }

        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0} ({1})",
                GetType().Name,
                Lexeme);
        }
    }

    internal abstract class NodeCollection: Node
    {
        public List<Node> ChildNodes = new List<Node>();

        protected NodeCollection(string lexeme)
            : base(lexeme) { }
    }

    internal abstract class HeaderNode : NodeCollection
    {
        public string Caption;

        protected HeaderNode(string lexeme, string caption) : base(lexeme)
        {
            Caption = caption;
        }
    }

    #endregion

    #region Leaf syntax types

    internal sealed class TextNode : Node
    {
        public TextNode(string lexeme) : base(lexeme) { }
    }

    internal sealed class MarkdownExampleNode: Node
    {
        public MarkdownExampleNode(string lexeme) : base(lexeme) { }
    }

    internal sealed class HtmlExampleNode : Node
    {
        public HtmlExampleNode(string lexeme) : base(lexeme) { }
    }

    #endregion

    #region Collection syntax types

    internal sealed class SpecificationAbstractSyntaxTree : NodeCollection
    {
        public SpecificationAbstractSyntaxTree() : base(null) { }
    }

    internal class Header1Node : HeaderNode
    {
        public Header1Node(string lexeme, string caption) : base(lexeme, caption) { }
    }

    internal class Header2Node : HeaderNode
    {
        public Header2Node(string lexeme, string caption) : base(lexeme, caption) { }
    }

    internal class ExampleNode : Node
    {
        public List<MarkdownExampleNode> MarkdownNodes = new List<MarkdownExampleNode>();
        public List<HtmlExampleNode> HtmlNodes = new List<HtmlExampleNode>();

        public ExampleNode() : base(null) { }
    }

    #endregion
}
