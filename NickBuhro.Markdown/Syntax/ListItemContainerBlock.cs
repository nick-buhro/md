namespace NickBuhro.Markdown.Syntax
{
    public abstract class ListItemContainerBlock: ContainerBlock
    {
        public override string ToString()
        {
            return "list_item";
        }
    }

    public sealed class BulletListItemContainerBlock: ListItemContainerBlock { }

    public sealed class OrderedListItemContainerBlock: ListItemContainerBlock { }

}
