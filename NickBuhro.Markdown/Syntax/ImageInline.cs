namespace NickBuhro.Markdown.Syntax
{
    public abstract class ImageInline: Inline
    { }

    public sealed class InlineImageInline: ImageInline { }

    public sealed class ReferenceImageInline: ImageInline { }
}
