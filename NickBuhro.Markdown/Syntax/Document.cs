namespace NickBuhro.Markdown.Syntax
{
    /// <summary>
    /// The root of the abstract syntax tree (AST).
    /// </summary>
    public sealed class Document
    {
        /// <summary>
        /// First child node.
        /// </summary>
        public Element Child { get; set; }

        public override string ToString()
        {
            return "document";
        }
    }
}