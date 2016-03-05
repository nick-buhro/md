namespace NickBuhro.Markdown.Syntax
{
    public abstract class LinkInline : Inline
    {
        public override string ToString()
        {
            return "link";
        }
    }

    public sealed class InlineLinkInline: LinkInline { }

    public sealed class ReferenceLinkInline: LinkInline { }

    public sealed class AutoLinkInline: LinkInline { }

    public sealed class EmailAutoLinkInline: LinkInline { }
}
