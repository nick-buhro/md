namespace NickBuhro.Markdown.Syntax
{
    public abstract class HeadingLeafBlock: LeafBlock { }



    public sealed class AtxHeadingLeafBlock : HeadingLeafBlock
    {
        public override string ToString()
        {
            return "atx_heading";
        }
    }



    public sealed class SetextHeadingLeafBlock : HeadingLeafBlock
    {
        public override string ToString()
        {
            return "setext_heading";
        }
    }
}
