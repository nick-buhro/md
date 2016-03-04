namespace NickBuhro.Markdown.Syntax
{
    public abstract class EmphasisInline: Inline
    {
        public override string ToString()
        {
            return "emphasis";
        }
    }

    public sealed class NormalEmphasisInline: EmphasisInline { }

    public sealed class StrongEmphasisInline: EmphasisInline { }
}
