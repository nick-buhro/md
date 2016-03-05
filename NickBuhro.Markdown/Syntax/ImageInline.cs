namespace NickBuhro.Markdown.Syntax
{
    public abstract class ImageInline : Inline
    {
        public override string ToString()
        {
            return "image";
        }
    }

    public sealed class InlineImageInline: ImageInline { }

    public sealed class ReferenceImageInline: ImageInline { }
}
