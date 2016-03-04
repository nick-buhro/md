namespace NickBuhro.Markdown.Syntax
{
    public abstract class CodeLeafBlock: LeafBlock
    {
        public override string ToString()
        {
            return "code_block";
        }
    }

    public sealed class IndentedCodeLeafBlock: CodeLeafBlock { }

    public sealed class FencedCodeLeafBlock: CodeLeafBlock { }
}
