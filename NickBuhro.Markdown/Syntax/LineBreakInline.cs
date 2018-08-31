namespace NickBuhro.Markdown.Syntax
{
    public abstract class LineBreakInline: Inline { }

    public sealed class SoftLineBreakInline : LineBreakInline
    {
        public override string ToString()
        {
            return "softbreak";
        }
    }

    public sealed class HardLineBreakInline : LineBreakInline
    {
        public override string ToString()
        {
            return "linebreak";
        }
    }
}
