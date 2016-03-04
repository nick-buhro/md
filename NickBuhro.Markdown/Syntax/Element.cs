namespace NickBuhro.Markdown.Syntax
{
    /// <summary>
    /// Base class for all Abstract Syntax Tree elements.
    /// </summary>
    public abstract class Element
    {
        //public abstract NodeType NodeType { get; }
        
        /// <summary>
        /// Next element of the same parent.
        /// </summary>
        public Element NextSibling { get; set; }

        /// <summary>
        /// First child node. 
        /// </summary>
        public Element Child { get; set; }
    }
}
