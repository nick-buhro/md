namespace NickBuhro.Markdown.Syntax
{
    public abstract class CodeLeafBlock: LeafBlock
    {
        
    }

    public sealed class IndentedCodeLeafBlock : CodeLeafBlock
    {
        public override string ToString()
        {
            return "indented_code";
        }
    }

    public sealed class FencedCodeLeafBlock : CodeLeafBlock
    {
        public override string ToString()
        {
            return "fenced_code";
        }
    }
}
