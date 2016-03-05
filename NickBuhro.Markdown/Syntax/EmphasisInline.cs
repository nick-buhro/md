namespace NickBuhro.Markdown.Syntax
{
    public abstract class EmphasisInline: Inline { }



    public sealed class NormalEmphasisInline : EmphasisInline
    {
        public override string ToString()
        {
            return "emph";
        }
    }



    public sealed class StrongEmphasisInline : EmphasisInline
    {
        public override string ToString()
        {
            return "strong";
        }
    }
}
