namespace NickBuhro.Markdown.Syntax
{
    public abstract class HeadingLeafBlock: LeafBlock
    {
        public override string ToString()
        {
            return "heading";
        }
    }

    public sealed class AtxHeadingLeafBlock: HeadingLeafBlock { }

    public sealed class SetextHeadingLeafBlock: HeadingLeafBlock { }
}
