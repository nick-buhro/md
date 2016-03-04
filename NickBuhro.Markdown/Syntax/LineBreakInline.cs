namespace NickBuhro.Markdown.Syntax
{
    public abstract class LineBreakInline: Inline { }

    public sealed class SoftLineBreakInline: LineBreakInline { }

    public sealed class HardLineBreakInline: LineBreakInline { }
}
