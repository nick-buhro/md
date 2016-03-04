namespace NickBuhro.Markdown.Syntax
{
    public abstract class ListContainerBlock: ContainerBlock
    {
        public override string ToString()
        {
            return "list";
        }
    }

    public sealed class BulletListContainerBlock: ListContainerBlock { }

    public sealed class OrderedListContainerBlock: ListContainerBlock { }
}
