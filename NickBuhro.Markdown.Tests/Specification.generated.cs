using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NickBuhro.Markdown.Tests
{
    [TestClass]
    public sealed partial class Specification
    {
        // ---
        // title: CommonMark Spec
        // author: John MacFarlane
        // version: 0.24
        // date: '2016-01-12'
        // license: '[CC-BY-SA 4.0](http://creativecommons.org/licenses/by-sa/4.0/)'
        // ...
        // 
        // # Introduction
        // 
        // ## What is Markdown?
        // 
        // Markdown is a plain text format for writing structured documents,
        // based on conventions used for indicating formatting in email and
        // usenet posts.  It was developed in 2004 by John Gruber, who wrote
        // the first Markdown-to-HTML converter in perl, and it soon became
        // widely used in websites.  By 2014 there were dozens of
        // implementations in many languages.  Some of them extended basic
        // Markdown syntax with conventions for footnotes, definition lists,
        // tables, and other constructs, and some allowed output not just in
        // HTML but in LaTeX and many other formats.
        // 
        // ## Why is a spec needed?
        // 
        // John Gruber's [canonical description of Markdown's
        // syntax](http://daringfireball.net/projects/markdown/syntax)
        // does not specify the syntax unambiguously.  Here are some examples of
        // questions it does not answer:
        // 
        // 1.  How much indentation is needed for a sublist?  The spec says that
        //     continuation paragraphs need to be indented four spaces, but is
        //     not fully explicit about sublists.  It is natural to think that
        //     they, too, must be indented four spaces, but `Markdown.pl` does
        //     not require that.  This is hardly a "corner case," and divergences
        //     between implementations on this issue often lead to surprises for
        //     users in real documents. (See [this comment by John
        //     Gruber](http://article.gmane.org/gmane.text.markdown.general/1997).)
        // 
        // 2.  Is a blank line needed before a block quote or heading?
        //     Most implementations do not require the blank line.  However,
        //     this can lead to unexpected results in hard-wrapped text, and
        //     also to ambiguities in parsing (note that some implementations
        //     put the heading inside the blockquote, while others do not).
        //     (John Gruber has also spoken [in favor of requiring the blank
        //     lines](http://article.gmane.org/gmane.text.markdown.general/2146).)
        // 
        // 3.  Is a blank line needed before an indented code block?
        //     (`Markdown.pl` requires it, but this is not mentioned in the
        //     documentation, and some implementations do not require it.)
        // 
        //     ``` markdown
        //     paragraph
        //         code?
        //     ```
        // 
        // 4.  What is the exact rule for determining when list items get
        //     wrapped in `<p>` tags?  Can a list be partially "loose" and partially
        //     "tight"?  What should we do with a list like this?
        // 
        //     ``` markdown
        //     1. one
        // 
        //     2. two
        //     3. three
        //     ```
        // 
        //     Or this?
        // 
        //     ``` markdown
        //     1.  one
        //         - a
        // 
        //         - b
        //     2.  two
        //     ```
        // 
        //     (There are some relevant comments by John Gruber
        //     [here](http://article.gmane.org/gmane.text.markdown.general/2554).)
        // 
        // 5.  Can list markers be indented?  Can ordered list markers be right-aligned?
        // 
        //     ``` markdown
        //      8. item 1
        //      9. item 2
        //     10. item 2a
        //     ```
        // 
        // 6.  Is this one list with a thematic break in its second item,
        //     or two lists separated by a thematic break?
        // 
        //     ``` markdown
        //     * a
        //     * * * * *
        //     * b
        //     ```
        // 
        // 7.  When list markers change from numbers to bullets, do we have
        //     two lists or one?  (The Markdown syntax description suggests two,
        //     but the perl scripts and many other implementations produce one.)
        // 
        //     ``` markdown
        //     1. fee
        //     2. fie
        //     -  foe
        //     -  fum
        //     ```
        // 
        // 8.  What are the precedence rules for the markers of inline structure?
        //     For example, is the following a valid link, or does the code span
        //     take precedence ?
        // 
        //     ``` markdown
        //     [a backtick (`)](/url) and [another backtick (`)](/url).
        //     ```
        // 
        // 9.  What are the precedence rules for markers of emphasis and strong
        //     emphasis?  For example, how should the following be parsed?
        // 
        //     ``` markdown
        //     *foo *bar* baz*
        //     ```
        // 
        // 10. What are the precedence rules between block-level and inline-level
        //     structure?  For example, how should the following be parsed?
        // 
        //     ``` markdown
        //     - `a long code span can contain a hyphen like this
        //       - and it can screw things up`
        //     ```
        // 
        // 11. Can list items include section headings?  (`Markdown.pl` does not
        //     allow this, but does allow blockquotes to include headings.)
        // 
        //     ``` markdown
        //     - # Heading
        //     ```
        // 
        // 12. Can list items be empty?
        // 
        //     ``` markdown
        //     * a
        //     *
        //     * b
        //     ```
        // 
        // 13. Can link references be defined inside block quotes or list items?
        // 
        //     ``` markdown
        //     > Blockquote [foo].
        //     >
        //     > [foo]: /url
        //     ```
        // 
        // 14. If there are multiple definitions for the same reference, which takes
        //     precedence?
        // 
        //     ``` markdown
        //     [foo]: /url1
        //     [foo]: /url2
        // 
        //     [foo][]
        //     ```
        // 
        // In the absence of a spec, early implementers consulted `Markdown.pl`
        // to resolve these ambiguities.  But `Markdown.pl` was quite buggy, and
        // gave manifestly bad results in many cases, so it was not a
        // satisfactory replacement for a spec.
        // 
        // Because there is no unambiguous spec, implementations have diverged
        // considerably.  As a result, users are often surprised to find that
        // a document that renders one way on one system (say, a github wiki)
        // renders differently on another (say, converting to docbook using
        // pandoc).  To make matters worse, because nothing in Markdown counts
        // as a "syntax error," the divergence often isn't discovered right away.
        // 
        // ## About this document
        // 
        // This document attempts to specify Markdown syntax unambiguously.
        // It contains many examples with side-by-side Markdown and
        // HTML.  These are intended to double as conformance tests.  An
        // accompanying script `spec_tests.py` can be used to run the tests
        // against any Markdown program:
        // 
        //     python test/spec_tests.py --spec spec.txt --program PROGRAM
        // 
        // Since this document describes how Markdown is to be parsed into
        // an abstract syntax tree, it would have made sense to use an abstract
        // representation of the syntax tree instead of HTML.  But HTML is capable
        // of representing the structural distinctions we need to make, and the
        // choice of HTML for the tests makes it possible to run the tests against
        // an implementation without writing an abstract syntax tree renderer.
        // 
        // This document is generated from a text file, `spec.txt`, written
        // in Markdown with a small extension for the side-by-side tests.
        // The script `tools/makespec.py` can be used to convert `spec.txt` into
        // HTML or CommonMark (which can then be converted into other formats).
        // 
        // In the examples, the `→` character is used to represent tabs.
        // 
        // # Preliminaries
        // 
        // ## Characters and lines
        // 
        // Any sequence of [characters] is a valid CommonMark
        // document.
        // 
        // A [character](@) is a Unicode code point.  Although some
        // code points (for example, combining accents) do not correspond to
        // characters in an intuitive sense, all code points count as characters
        // for purposes of this spec.
        // 
        // This spec does not specify an encoding; it thinks of lines as composed
        // of [characters] rather than bytes.  A conforming parser may be limited
        // to a certain encoding.
        // 
        // A [line](@) is a sequence of zero or more [characters]
        // other than newline (`U+000A`) or carriage return (`U+000D`),
        // followed by a [line ending] or by the end of file.
        // 
        // A [line ending](@) is a newline (`U+000A`), a carriage return
        // (`U+000D`) not followed by a newline, or a carriage return and a
        // following newline.
        // 
        // A line containing no characters, or a line containing only spaces
        // (`U+0020`) or tabs (`U+0009`), is called a [blank line](@).
        // 
        // The following definitions of character classes will be used in this spec:
        // 
        // A [whitespace character](@) is a space
        // (`U+0020`), tab (`U+0009`), newline (`U+000A`), line tabulation (`U+000B`),
        // form feed (`U+000C`), or carriage return (`U+000D`).
        // 
        // [Whitespace](@) is a sequence of one or more [whitespace
        // characters].
        // 
        // A [Unicode whitespace character](@) is
        // any code point in the Unicode `Zs` class, or a tab (`U+0009`),
        // carriage return (`U+000D`), newline (`U+000A`), or form feed
        // (`U+000C`).
        // 
        // [Unicode whitespace](@) is a sequence of one
        // or more [Unicode whitespace characters].
        // 
        // A [space](@) is `U+0020`.
        // 
        // A [non-whitespace character](@) is any character
        // that is not a [whitespace character].
        // 
        // An [ASCII punctuation character](@)
        // is `!`, `"`, `#`, `$`, `%`, `&`, `'`, `(`, `)`,
        // `*`, `+`, `,`, `-`, `.`, `/`, `:`, `;`, `<`, `=`, `>`, `?`, `@`,
        // `[`, `\`, `]`, `^`, `_`, `` ` ``, `{`, `|`, `}`, or `~`.
        // 
        // A [punctuation character](@) is an [ASCII
        // punctuation character] or anything in
        // the Unicode classes `Pc`, `Pd`, `Pe`, `Pf`, `Pi`, `Po`, or `Ps`.
        // 
        // ## Tabs
        // 
        // Tabs in lines are not expanded to [spaces].  However,
        // in contexts where indentation is significant for the
        // document's structure, tabs behave as if they were replaced
        // by spaces with a tab stop of 4 characters.
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example001()
        {
            // Source:
            //     →foo→baz→→bim
            // 
            // Expected result:
            //     <pre><code>foo→baz→→bim
            //     </code></pre>
            
            ExecuteExampleTest(1, "Preliminaries - Tabs",
                "\tfoo\tbaz\t\tbim",
                "<pre><code>foo\tbaz\t\tbim\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example002()
        {
            // Source:
            //       →foo→baz→→bim
            // 
            // Expected result:
            //     <pre><code>foo→baz→→bim
            //     </code></pre>
            
            ExecuteExampleTest(2, "Preliminaries - Tabs",
                "  \tfoo\tbaz\t\tbim",
                "<pre><code>foo\tbaz\t\tbim\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example003()
        {
            // Source:
            //         a→a
            //         ὐ→a
            // 
            // Expected result:
            //     <pre><code>a→a
            //     ὐ→a
            //     </code></pre>
            
            ExecuteExampleTest(3, "Preliminaries - Tabs",
                "    a\ta\r\n    ὐ\ta",
                "<pre><code>a\ta\r\nὐ\ta\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example004()
        {
            // Source:
            //       - foo
            //     
            //     →bar
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(4, "Preliminaries - Tabs",
                "  - foo\r\n\r\n\tbar",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<p>bar</p>\r\n</li>\r\n</ul>");
        }
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example005()
        {
            // Source:
            //     - foo
            //     
            //     →→bar
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>  bar
            //     </code></pre>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(5, "Preliminaries - Tabs",
                "- foo\r\n\r\n\t\tbar",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<pre><code>  bar\r\n</code></pre>\r\n</li>\r\n</ul>");
        }
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example006()
        {
            // Source:
            //     >→→foo
            // 
            // Expected result:
            //     <blockquote>
            //     <pre><code>  foo
            //     </code></pre>
            //     </blockquote>
            
            ExecuteExampleTest(6, "Preliminaries - Tabs",
                ">\t\tfoo",
                "<blockquote>\r\n<pre><code>  foo\r\n</code></pre>\r\n</blockquote>");
        }
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example007()
        {
            // Source:
            //     -→→foo
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <pre><code>  foo
            //     </code></pre>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(7, "Preliminaries - Tabs",
                "-\t\tfoo",
                "<ul>\r\n<li>\r\n<pre><code>  foo\r\n</code></pre>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example008()
        {
            // Source:
            //         foo
            //     →bar
            // 
            // Expected result:
            //     <pre><code>foo
            //     bar
            //     </code></pre>
            
            ExecuteExampleTest(8, "Preliminaries - Tabs",
                "    foo\r\n\tbar",
                "<pre><code>foo\r\nbar\r\n</code></pre>");
        }
        // 
        [TestMethod]
        [TestCategory("Preliminaries - Tabs")]
        public void Example009()
        {
            // Source:
            //      - foo
            //        - bar
            //     → - baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(9, "Preliminaries - Tabs",
                " - foo\r\n   - bar\r\n\t - baz",
                "<ul>\r\n<li>foo\r\n<ul>\r\n<li>bar\r\n<ul>\r\n<li>baz</li>\r\n</ul>\r\n</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // 
        // ## Insecure characters
        // 
        // For security reasons, the Unicode character `U+0000` must be replaced
        // with the REPLACEMENT CHARACTER (`U+FFFD`).
        // 
        // # Blocks and inlines
        // 
        // We can think of a document as a sequence of
        // [blocks](@)---structural elements like paragraphs, block
        // quotations, lists, headings, rules, and code blocks.  Some blocks (like
        // block quotes and list items) contain other blocks; others (like
        // headings and paragraphs) contain [inline](@) content---text,
        // links, emphasized text, images, code, and so on.
        // 
        // ## Precedence
        // 
        // Indicators of block structure always take precedence over indicators
        // of inline structure.  So, for example, the following is a list with
        // two items, not a list with one item containing a code span:
        // 
        [TestMethod]
        [TestCategory("Blocks and inlines - Precedence")]
        public void Example010()
        {
            // Source:
            //     - `one
            //     - two`
            // 
            // Expected result:
            //     <ul>
            //     <li>`one</li>
            //     <li>two`</li>
            //     </ul>
            
            ExecuteExampleTest(10, "Blocks and inlines - Precedence",
                "- `one\r\n- two`",
                "<ul>\r\n<li>`one</li>\r\n<li>two`</li>\r\n</ul>");
        }
        // 
        // 
        // This means that parsing can proceed in two steps:  first, the block
        // structure of the document can be discerned; second, text lines inside
        // paragraphs, headings, and other block constructs can be parsed for inline
        // structure.  The second step requires information about link reference
        // definitions that will be available only at the end of the first
        // step.  Note that the first step requires processing lines in sequence,
        // but the second can be parallelized, since the inline parsing of
        // one block element does not affect the inline parsing of any other.
        // 
        // ## Container blocks and leaf blocks
        // 
        // We can divide blocks into two types:
        // [container block](@)s,
        // which can contain other blocks, and [leaf block](@)s,
        // which cannot.
        // 
        // # Leaf blocks
        // 
        // This section describes the different kinds of leaf block that make up a
        // Markdown document.
        // 
        // ## Thematic breaks
        // 
        // A line consisting of 0-3 spaces of indentation, followed by a sequence
        // of three or more matching `-`, `_`, or `*` characters, each followed
        // optionally by any number of spaces, forms a
        // [thematic break](@).
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example011()
        {
            // Source:
            //     ***
            //     ---
            //     ___
            // 
            // Expected result:
            //     <hr />
            //     <hr />
            //     <hr />
            
            ExecuteExampleTest(11, "Leaf blocks - Thematic breaks",
                "***\r\n---\r\n___",
                "<hr />\r\n<hr />\r\n<hr />");
        }
        // 
        // 
        // Wrong characters:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example012()
        {
            // Source:
            //     +++
            // 
            // Expected result:
            //     <p>+++</p>
            
            ExecuteExampleTest(12, "Leaf blocks - Thematic breaks",
                "+++",
                "<p>+++</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example013()
        {
            // Source:
            //     ===
            // 
            // Expected result:
            //     <p>===</p>
            
            ExecuteExampleTest(13, "Leaf blocks - Thematic breaks",
                "===",
                "<p>===</p>");
        }
        // 
        // 
        // Not enough characters:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example014()
        {
            // Source:
            //     --
            //     **
            //     __
            // 
            // Expected result:
            //     <p>--
            //     **
            //     __</p>
            
            ExecuteExampleTest(14, "Leaf blocks - Thematic breaks",
                "--\r\n**\r\n__",
                "<p>--\r\n**\r\n__</p>");
        }
        // 
        // 
        // One to three spaces indent are allowed:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example015()
        {
            // Source:
            //      ***
            //       ***
            //        ***
            // 
            // Expected result:
            //     <hr />
            //     <hr />
            //     <hr />
            
            ExecuteExampleTest(15, "Leaf blocks - Thematic breaks",
                " ***\r\n  ***\r\n   ***",
                "<hr />\r\n<hr />\r\n<hr />");
        }
        // 
        // 
        // Four spaces is too many:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example016()
        {
            // Source:
            //         ***
            // 
            // Expected result:
            //     <pre><code>***
            //     </code></pre>
            
            ExecuteExampleTest(16, "Leaf blocks - Thematic breaks",
                "    ***",
                "<pre><code>***\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example017()
        {
            // Source:
            //     Foo
            //         ***
            // 
            // Expected result:
            //     <p>Foo
            //     ***</p>
            
            ExecuteExampleTest(17, "Leaf blocks - Thematic breaks",
                "Foo\r\n    ***",
                "<p>Foo\r\n***</p>");
        }
        // 
        // 
        // More than three characters may be used:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example018()
        {
            // Source:
            //     _____________________________________
            // 
            // Expected result:
            //     <hr />
            
            ExecuteExampleTest(18, "Leaf blocks - Thematic breaks",
                "_____________________________________",
                "<hr />");
        }
        // 
        // 
        // Spaces are allowed between the characters:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example019()
        {
            // Source:
            //      - - -
            // 
            // Expected result:
            //     <hr />
            
            ExecuteExampleTest(19, "Leaf blocks - Thematic breaks",
                " - - -",
                "<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example020()
        {
            // Source:
            //      **  * ** * ** * **
            // 
            // Expected result:
            //     <hr />
            
            ExecuteExampleTest(20, "Leaf blocks - Thematic breaks",
                " **  * ** * ** * **",
                "<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example021()
        {
            // Source:
            //     -     -      -      -
            // 
            // Expected result:
            //     <hr />
            
            ExecuteExampleTest(21, "Leaf blocks - Thematic breaks",
                "-     -      -      -",
                "<hr />");
        }
        // 
        // 
        // Spaces are allowed at the end:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example022()
        {
            // Source:
            //     - - - -    
            // 
            // Expected result:
            //     <hr />
            
            ExecuteExampleTest(22, "Leaf blocks - Thematic breaks",
                "- - - -    ",
                "<hr />");
        }
        // 
        // 
        // However, no other characters may occur in the line:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example023()
        {
            // Source:
            //     _ _ _ _ a
            //     
            //     a------
            //     
            //     ---a---
            // 
            // Expected result:
            //     <p>_ _ _ _ a</p>
            //     <p>a------</p>
            //     <p>---a---</p>
            
            ExecuteExampleTest(23, "Leaf blocks - Thematic breaks",
                "_ _ _ _ a\r\n\r\na------\r\n\r\n---a---",
                "<p>_ _ _ _ a</p>\r\n<p>a------</p>\r\n<p>---a---</p>");
        }
        // 
        // 
        // It is required that all of the [non-whitespace characters] be the same.
        // So, this is not a thematic break:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example024()
        {
            // Source:
            //      *-*
            // 
            // Expected result:
            //     <p><em>-</em></p>
            
            ExecuteExampleTest(24, "Leaf blocks - Thematic breaks",
                " *-*",
                "<p><em>-</em></p>");
        }
        // 
        // 
        // Thematic breaks do not need blank lines before or after:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example025()
        {
            // Source:
            //     - foo
            //     ***
            //     - bar
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <hr />
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            
            ExecuteExampleTest(25, "Leaf blocks - Thematic breaks",
                "- foo\r\n***\r\n- bar",
                "<ul>\r\n<li>foo</li>\r\n</ul>\r\n<hr />\r\n<ul>\r\n<li>bar</li>\r\n</ul>");
        }
        // 
        // 
        // Thematic breaks can interrupt a paragraph:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example026()
        {
            // Source:
            //     Foo
            //     ***
            //     bar
            // 
            // Expected result:
            //     <p>Foo</p>
            //     <hr />
            //     <p>bar</p>
            
            ExecuteExampleTest(26, "Leaf blocks - Thematic breaks",
                "Foo\r\n***\r\nbar",
                "<p>Foo</p>\r\n<hr />\r\n<p>bar</p>");
        }
        // 
        // 
        // If a line of dashes that meets the above conditions for being a
        // thematic break could also be interpreted as the underline of a [setext
        // heading], the interpretation as a
        // [setext heading] takes precedence. Thus, for example,
        // this is a setext heading, not a paragraph followed by a thematic break:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example027()
        {
            // Source:
            //     Foo
            //     ---
            //     bar
            // 
            // Expected result:
            //     <h2>Foo</h2>
            //     <p>bar</p>
            
            ExecuteExampleTest(27, "Leaf blocks - Thematic breaks",
                "Foo\r\n---\r\nbar",
                "<h2>Foo</h2>\r\n<p>bar</p>");
        }
        // 
        // 
        // When both a thematic break and a list item are possible
        // interpretations of a line, the thematic break takes precedence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example028()
        {
            // Source:
            //     * Foo
            //     * * *
            //     * Bar
            // 
            // Expected result:
            //     <ul>
            //     <li>Foo</li>
            //     </ul>
            //     <hr />
            //     <ul>
            //     <li>Bar</li>
            //     </ul>
            
            ExecuteExampleTest(28, "Leaf blocks - Thematic breaks",
                "* Foo\r\n* * *\r\n* Bar",
                "<ul>\r\n<li>Foo</li>\r\n</ul>\r\n<hr />\r\n<ul>\r\n<li>Bar</li>\r\n</ul>");
        }
        // 
        // 
        // If you want a thematic break in a list item, use a different bullet:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Thematic breaks")]
        public void Example029()
        {
            // Source:
            //     - Foo
            //     - * * *
            // 
            // Expected result:
            //     <ul>
            //     <li>Foo</li>
            //     <li>
            //     <hr />
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(29, "Leaf blocks - Thematic breaks",
                "- Foo\r\n- * * *",
                "<ul>\r\n<li>Foo</li>\r\n<li>\r\n<hr />\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // ## ATX headings
        // 
        // An [ATX heading](@)
        // consists of a string of characters, parsed as inline content, between an
        // opening sequence of 1--6 unescaped `#` characters and an optional
        // closing sequence of any number of unescaped `#` characters.
        // The opening sequence of `#` characters must be followed by a
        // [space] or by the end of line. The optional closing sequence of `#`s must be
        // preceded by a [space] and may be followed by spaces only.  The opening
        // `#` character may be indented 0-3 spaces.  The raw contents of the
        // heading are stripped of leading and trailing spaces before being parsed
        // as inline content.  The heading level is equal to the number of `#`
        // characters in the opening sequence.
        // 
        // Simple headings:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example030()
        {
            // Source:
            //     # foo
            //     ## foo
            //     ### foo
            //     #### foo
            //     ##### foo
            //     ###### foo
            // 
            // Expected result:
            //     <h1>foo</h1>
            //     <h2>foo</h2>
            //     <h3>foo</h3>
            //     <h4>foo</h4>
            //     <h5>foo</h5>
            //     <h6>foo</h6>
            
            ExecuteExampleTest(30, "Leaf blocks - ATX headings",
                "# foo\r\n## foo\r\n### foo\r\n#### foo\r\n##### foo\r\n###### foo",
                "<h1>foo</h1>\r\n<h2>foo</h2>\r\n<h3>foo</h3>\r\n<h4>foo</h4>\r\n<h5>foo</h5>\r\n<h6>foo</h6>");
        }
        // 
        // 
        // More than six `#` characters is not a heading:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example031()
        {
            // Source:
            //     ####### foo
            // 
            // Expected result:
            //     <p>####### foo</p>
            
            ExecuteExampleTest(31, "Leaf blocks - ATX headings",
                "####### foo",
                "<p>####### foo</p>");
        }
        // 
        // 
        // At least one space is required between the `#` characters and the
        // heading's contents, unless the heading is empty.  Note that many
        // implementations currently do not require the space.  However, the
        // space was required by the
        // [original ATX implementation](http://www.aaronsw.com/2002/atx/atx.py),
        // and it helps prevent things like the following from being parsed as
        // headings:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example032()
        {
            // Source:
            //     #5 bolt
            //     
            //     #hashtag
            // 
            // Expected result:
            //     <p>#5 bolt</p>
            //     <p>#hashtag</p>
            
            ExecuteExampleTest(32, "Leaf blocks - ATX headings",
                "#5 bolt\r\n\r\n#hashtag",
                "<p>#5 bolt</p>\r\n<p>#hashtag</p>");
        }
        // 
        // 
        // A tab will not work:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example033()
        {
            // Source:
            //     #→foo
            // 
            // Expected result:
            //     <p>#→foo</p>
            
            ExecuteExampleTest(33, "Leaf blocks - ATX headings",
                "#\tfoo",
                "<p>#\tfoo</p>");
        }
        // 
        // 
        // This is not a heading, because the first `#` is escaped:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example034()
        {
            // Source:
            //     \## foo
            // 
            // Expected result:
            //     <p>## foo</p>
            
            ExecuteExampleTest(34, "Leaf blocks - ATX headings",
                "\\## foo",
                "<p>## foo</p>");
        }
        // 
        // 
        // Contents are parsed as inlines:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example035()
        {
            // Source:
            //     # foo *bar* \*baz\*
            // 
            // Expected result:
            //     <h1>foo <em>bar</em> *baz*</h1>
            
            ExecuteExampleTest(35, "Leaf blocks - ATX headings",
                "# foo *bar* \\*baz\\*",
                "<h1>foo <em>bar</em> *baz*</h1>");
        }
        // 
        // 
        // Leading and trailing blanks are ignored in parsing inline content:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example036()
        {
            // Source:
            //     #                  foo                     
            // 
            // Expected result:
            //     <h1>foo</h1>
            
            ExecuteExampleTest(36, "Leaf blocks - ATX headings",
                "#                  foo                     ",
                "<h1>foo</h1>");
        }
        // 
        // 
        // One to three spaces indentation are allowed:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example037()
        {
            // Source:
            //      ### foo
            //       ## foo
            //        # foo
            // 
            // Expected result:
            //     <h3>foo</h3>
            //     <h2>foo</h2>
            //     <h1>foo</h1>
            
            ExecuteExampleTest(37, "Leaf blocks - ATX headings",
                " ### foo\r\n  ## foo\r\n   # foo",
                "<h3>foo</h3>\r\n<h2>foo</h2>\r\n<h1>foo</h1>");
        }
        // 
        // 
        // Four spaces are too much:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example038()
        {
            // Source:
            //         # foo
            // 
            // Expected result:
            //     <pre><code># foo
            //     </code></pre>
            
            ExecuteExampleTest(38, "Leaf blocks - ATX headings",
                "    # foo",
                "<pre><code># foo\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example039()
        {
            // Source:
            //     foo
            //         # bar
            // 
            // Expected result:
            //     <p>foo
            //     # bar</p>
            
            ExecuteExampleTest(39, "Leaf blocks - ATX headings",
                "foo\r\n    # bar",
                "<p>foo\r\n# bar</p>");
        }
        // 
        // 
        // A closing sequence of `#` characters is optional:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example040()
        {
            // Source:
            //     ## foo ##
            //       ###   bar    ###
            // 
            // Expected result:
            //     <h2>foo</h2>
            //     <h3>bar</h3>
            
            ExecuteExampleTest(40, "Leaf blocks - ATX headings",
                "## foo ##\r\n  ###   bar    ###",
                "<h2>foo</h2>\r\n<h3>bar</h3>");
        }
        // 
        // 
        // It need not be the same length as the opening sequence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example041()
        {
            // Source:
            //     # foo ##################################
            //     ##### foo ##
            // 
            // Expected result:
            //     <h1>foo</h1>
            //     <h5>foo</h5>
            
            ExecuteExampleTest(41, "Leaf blocks - ATX headings",
                "# foo ##################################\r\n##### foo ##",
                "<h1>foo</h1>\r\n<h5>foo</h5>");
        }
        // 
        // 
        // Spaces are allowed after the closing sequence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example042()
        {
            // Source:
            //     ### foo ###     
            // 
            // Expected result:
            //     <h3>foo</h3>
            
            ExecuteExampleTest(42, "Leaf blocks - ATX headings",
                "### foo ###     ",
                "<h3>foo</h3>");
        }
        // 
        // 
        // A sequence of `#` characters with anything but [spaces] following it
        // is not a closing sequence, but counts as part of the contents of the
        // heading:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example043()
        {
            // Source:
            //     ### foo ### b
            // 
            // Expected result:
            //     <h3>foo ### b</h3>
            
            ExecuteExampleTest(43, "Leaf blocks - ATX headings",
                "### foo ### b",
                "<h3>foo ### b</h3>");
        }
        // 
        // 
        // The closing sequence must be preceded by a space:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example044()
        {
            // Source:
            //     # foo#
            // 
            // Expected result:
            //     <h1>foo#</h1>
            
            ExecuteExampleTest(44, "Leaf blocks - ATX headings",
                "# foo#",
                "<h1>foo#</h1>");
        }
        // 
        // 
        // Backslash-escaped `#` characters do not count as part
        // of the closing sequence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example045()
        {
            // Source:
            //     ### foo \###
            //     ## foo #\##
            //     # foo \#
            // 
            // Expected result:
            //     <h3>foo ###</h3>
            //     <h2>foo ###</h2>
            //     <h1>foo #</h1>
            
            ExecuteExampleTest(45, "Leaf blocks - ATX headings",
                "### foo \\###\r\n## foo #\\##\r\n# foo \\#",
                "<h3>foo ###</h3>\r\n<h2>foo ###</h2>\r\n<h1>foo #</h1>");
        }
        // 
        // 
        // ATX headings need not be separated from surrounding content by blank
        // lines, and they can interrupt paragraphs:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example046()
        {
            // Source:
            //     ****
            //     ## foo
            //     ****
            // 
            // Expected result:
            //     <hr />
            //     <h2>foo</h2>
            //     <hr />
            
            ExecuteExampleTest(46, "Leaf blocks - ATX headings",
                "****\r\n## foo\r\n****",
                "<hr />\r\n<h2>foo</h2>\r\n<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example047()
        {
            // Source:
            //     Foo bar
            //     # baz
            //     Bar foo
            // 
            // Expected result:
            //     <p>Foo bar</p>
            //     <h1>baz</h1>
            //     <p>Bar foo</p>
            
            ExecuteExampleTest(47, "Leaf blocks - ATX headings",
                "Foo bar\r\n# baz\r\nBar foo",
                "<p>Foo bar</p>\r\n<h1>baz</h1>\r\n<p>Bar foo</p>");
        }
        // 
        // 
        // ATX headings can be empty:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - ATX headings")]
        public void Example048()
        {
            // Source:
            //     ## 
            //     #
            //     ### ###
            // 
            // Expected result:
            //     <h2></h2>
            //     <h1></h1>
            //     <h3></h3>
            
            ExecuteExampleTest(48, "Leaf blocks - ATX headings",
                "## \r\n#\r\n### ###",
                "<h2></h2>\r\n<h1></h1>\r\n<h3></h3>");
        }
        // 
        // 
        // ## Setext headings
        // 
        // A [setext heading](@) consists of one or more
        // lines of text, each containing at least one [non-whitespace
        // character], with no more than 3 spaces indentation, followed by
        // a [setext heading underline].  The lines of text must be such
        // that, were they not followed by the setext heading underline,
        // they would be interpreted as a paragraph:  they cannot be
        // interpretable as a [code fence], [ATX heading][ATX headings],
        // [block quote][block quotes], [thematic break][thematic breaks],
        // [list item][list items], or [HTML block][HTML blocks].
        // 
        // A [setext heading underline](@) is a sequence of
        // `=` characters or a sequence of `-` characters, with no more than 3
        // spaces indentation and any number of trailing spaces.  If a line
        // containing a single `-` can be interpreted as an
        // empty [list items], it should be interpreted this way
        // and not as a [setext heading underline].
        // 
        // The heading is a level 1 heading if `=` characters are used in
        // the [setext heading underline], and a level 2 heading if `-`
        // characters are used.  The contents of the heading are the result
        // of parsing the preceding lines of text as CommonMark inline
        // content.
        // 
        // In general, a setext heading need not be preceded or followed by a
        // blank line.  However, it cannot interrupt a paragraph, so when a
        // setext heading comes after a paragraph, a blank line is needed between
        // them.
        // 
        // Simple examples:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example049()
        {
            // Source:
            //     Foo *bar*
            //     =========
            //     
            //     Foo *bar*
            //     ---------
            // 
            // Expected result:
            //     <h1>Foo <em>bar</em></h1>
            //     <h2>Foo <em>bar</em></h2>
            
            ExecuteExampleTest(49, "Leaf blocks - Setext headings",
                "Foo *bar*\r\n=========\r\n\r\nFoo *bar*\r\n---------",
                "<h1>Foo <em>bar</em></h1>\r\n<h2>Foo <em>bar</em></h2>");
        }
        // 
        // 
        // The content of the header may span more than one line:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example050()
        {
            // Source:
            //     Foo *bar
            //     baz*
            //     ====
            // 
            // Expected result:
            //     <h1>Foo <em>bar
            //     baz</em></h1>
            
            ExecuteExampleTest(50, "Leaf blocks - Setext headings",
                "Foo *bar\r\nbaz*\r\n====",
                "<h1>Foo <em>bar\r\nbaz</em></h1>");
        }
        // 
        // 
        // The underlining can be any length:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example051()
        {
            // Source:
            //     Foo
            //     -------------------------
            //     
            //     Foo
            //     =
            // 
            // Expected result:
            //     <h2>Foo</h2>
            //     <h1>Foo</h1>
            
            ExecuteExampleTest(51, "Leaf blocks - Setext headings",
                "Foo\r\n-------------------------\r\n\r\nFoo\r\n=",
                "<h2>Foo</h2>\r\n<h1>Foo</h1>");
        }
        // 
        // 
        // The heading content can be indented up to three spaces, and need
        // not line up with the underlining:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example052()
        {
            // Source:
            //        Foo
            //     ---
            //     
            //       Foo
            //     -----
            //     
            //       Foo
            //       ===
            // 
            // Expected result:
            //     <h2>Foo</h2>
            //     <h2>Foo</h2>
            //     <h1>Foo</h1>
            
            ExecuteExampleTest(52, "Leaf blocks - Setext headings",
                "   Foo\r\n---\r\n\r\n  Foo\r\n-----\r\n\r\n  Foo\r\n  ===",
                "<h2>Foo</h2>\r\n<h2>Foo</h2>\r\n<h1>Foo</h1>");
        }
        // 
        // 
        // Four spaces indent is too much:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example053()
        {
            // Source:
            //         Foo
            //         ---
            //     
            //         Foo
            //     ---
            // 
            // Expected result:
            //     <pre><code>Foo
            //     ---
            //     
            //     Foo
            //     </code></pre>
            //     <hr />
            
            ExecuteExampleTest(53, "Leaf blocks - Setext headings",
                "    Foo\r\n    ---\r\n\r\n    Foo\r\n---",
                "<pre><code>Foo\r\n---\r\n\r\nFoo\r\n</code></pre>\r\n<hr />");
        }
        // 
        // 
        // The setext heading underline can be indented up to three spaces, and
        // may have trailing spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example054()
        {
            // Source:
            //     Foo
            //        ----      
            // 
            // Expected result:
            //     <h2>Foo</h2>
            
            ExecuteExampleTest(54, "Leaf blocks - Setext headings",
                "Foo\r\n   ----      ",
                "<h2>Foo</h2>");
        }
        // 
        // 
        // Four spaces is too much:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example055()
        {
            // Source:
            //     Foo
            //         ---
            // 
            // Expected result:
            //     <p>Foo
            //     ---</p>
            
            ExecuteExampleTest(55, "Leaf blocks - Setext headings",
                "Foo\r\n    ---",
                "<p>Foo\r\n---</p>");
        }
        // 
        // 
        // The setext heading underline cannot contain internal spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example056()
        {
            // Source:
            //     Foo
            //     = =
            //     
            //     Foo
            //     --- -
            // 
            // Expected result:
            //     <p>Foo
            //     = =</p>
            //     <p>Foo</p>
            //     <hr />
            
            ExecuteExampleTest(56, "Leaf blocks - Setext headings",
                "Foo\r\n= =\r\n\r\nFoo\r\n--- -",
                "<p>Foo\r\n= =</p>\r\n<p>Foo</p>\r\n<hr />");
        }
        // 
        // 
        // Trailing spaces in the content line do not cause a line break:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example057()
        {
            // Source:
            //     Foo  
            //     -----
            // 
            // Expected result:
            //     <h2>Foo</h2>
            
            ExecuteExampleTest(57, "Leaf blocks - Setext headings",
                "Foo  \r\n-----",
                "<h2>Foo</h2>");
        }
        // 
        // 
        // Nor does a backslash at the end:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example058()
        {
            // Source:
            //     Foo\
            //     ----
            // 
            // Expected result:
            //     <h2>Foo\</h2>
            
            ExecuteExampleTest(58, "Leaf blocks - Setext headings",
                "Foo\\\r\n----",
                "<h2>Foo\\</h2>");
        }
        // 
        // 
        // Since indicators of block structure take precedence over
        // indicators of inline structure, the following are setext headings:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example059()
        {
            // Source:
            //     `Foo
            //     ----
            //     `
            //     
            //     <a title="a lot
            //     ---
            //     of dashes"/>
            // 
            // Expected result:
            //     <h2>`Foo</h2>
            //     <p>`</p>
            //     <h2>&lt;a title=&quot;a lot</h2>
            //     <p>of dashes&quot;/&gt;</p>
            
            ExecuteExampleTest(59, "Leaf blocks - Setext headings",
                "`Foo\r\n----\r\n`\r\n\r\n<a title=\"a lot\r\n---\r\nof dashes\"/>",
                "<h2>`Foo</h2>\r\n<p>`</p>\r\n<h2>&lt;a title=&quot;a lot</h2>\r\n<p>of dashes&quot;/&gt;</p>");
        }
        // 
        // 
        // The setext heading underline cannot be a [lazy continuation
        // line] in a list item or block quote:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example060()
        {
            // Source:
            //     > Foo
            //     ---
            // 
            // Expected result:
            //     <blockquote>
            //     <p>Foo</p>
            //     </blockquote>
            //     <hr />
            
            ExecuteExampleTest(60, "Leaf blocks - Setext headings",
                "> Foo\r\n---",
                "<blockquote>\r\n<p>Foo</p>\r\n</blockquote>\r\n<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example061()
        {
            // Source:
            //     > foo
            //     bar
            //     ===
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo
            //     bar
            //     ===</p>
            //     </blockquote>
            
            ExecuteExampleTest(61, "Leaf blocks - Setext headings",
                "> foo\r\nbar\r\n===",
                "<blockquote>\r\n<p>foo\r\nbar\r\n===</p>\r\n</blockquote>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example062()
        {
            // Source:
            //     - Foo
            //     ---
            // 
            // Expected result:
            //     <ul>
            //     <li>Foo</li>
            //     </ul>
            //     <hr />
            
            ExecuteExampleTest(62, "Leaf blocks - Setext headings",
                "- Foo\r\n---",
                "<ul>\r\n<li>Foo</li>\r\n</ul>\r\n<hr />");
        }
        // 
        // 
        // A blank line is needed between a paragraph and a following
        // setext heading, since otherwise the paragraph becomes part
        // of the heading's content:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example063()
        {
            // Source:
            //     Foo
            //     Bar
            //     ---
            // 
            // Expected result:
            //     <h2>Foo
            //     Bar</h2>
            
            ExecuteExampleTest(63, "Leaf blocks - Setext headings",
                "Foo\r\nBar\r\n---",
                "<h2>Foo\r\nBar</h2>");
        }
        // 
        // 
        // But in general a blank line is not required before or after
        // setext headings:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example064()
        {
            // Source:
            //     ---
            //     Foo
            //     ---
            //     Bar
            //     ---
            //     Baz
            // 
            // Expected result:
            //     <hr />
            //     <h2>Foo</h2>
            //     <h2>Bar</h2>
            //     <p>Baz</p>
            
            ExecuteExampleTest(64, "Leaf blocks - Setext headings",
                "---\r\nFoo\r\n---\r\nBar\r\n---\r\nBaz",
                "<hr />\r\n<h2>Foo</h2>\r\n<h2>Bar</h2>\r\n<p>Baz</p>");
        }
        // 
        // 
        // Setext headings cannot be empty:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example065()
        {
            // Source:
            //     
            //     ====
            // 
            // Expected result:
            //     <p>====</p>
            
            ExecuteExampleTest(65, "Leaf blocks - Setext headings",
                "\r\n====",
                "<p>====</p>");
        }
        // 
        // 
        // Setext heading text lines must not be interpretable as block
        // constructs other than paragraphs.  So, the line of dashes
        // in these examples gets interpreted as a thematic break:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example066()
        {
            // Source:
            //     ---
            //     ---
            // 
            // Expected result:
            //     <hr />
            //     <hr />
            
            ExecuteExampleTest(66, "Leaf blocks - Setext headings",
                "---\r\n---",
                "<hr />\r\n<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example067()
        {
            // Source:
            //     - foo
            //     -----
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <hr />
            
            ExecuteExampleTest(67, "Leaf blocks - Setext headings",
                "- foo\r\n-----",
                "<ul>\r\n<li>foo</li>\r\n</ul>\r\n<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example068()
        {
            // Source:
            //         foo
            //     ---
            // 
            // Expected result:
            //     <pre><code>foo
            //     </code></pre>
            //     <hr />
            
            ExecuteExampleTest(68, "Leaf blocks - Setext headings",
                "    foo\r\n---",
                "<pre><code>foo\r\n</code></pre>\r\n<hr />");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example069()
        {
            // Source:
            //     > foo
            //     -----
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <hr />
            
            ExecuteExampleTest(69, "Leaf blocks - Setext headings",
                "> foo\r\n-----",
                "<blockquote>\r\n<p>foo</p>\r\n</blockquote>\r\n<hr />");
        }
        // 
        // 
        // If you want a heading with `> foo` as its literal text, you can
        // use backslash escapes:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example070()
        {
            // Source:
            //     \> foo
            //     ------
            // 
            // Expected result:
            //     <h2>&gt; foo</h2>
            
            ExecuteExampleTest(70, "Leaf blocks - Setext headings",
                "\\> foo\r\n------",
                "<h2>&gt; foo</h2>");
        }
        // 
        // 
        // **Compatibility note:**  Most existing Markdown implementations
        // do not allow the text of setext headings to span multiple lines.
        // But there is no consensus about how to interpret
        // 
        // ``` markdown
        // Foo
        // bar
        // ---
        // baz
        // ```
        // 
        // One can find four different interpretations:
        // 
        // 1. paragraph "Foo", heading "bar", paragraph "baz"
        // 2. paragraph "Foo bar", thematic break, paragraph "baz"
        // 3. paragraph "Foo bar --- baz"
        // 4. heading "Foo bar", paragraph "baz"
        // 
        // We find interpretation 4 most natural, and interpretation 4
        // increases the expressive power of CommonMark, by allowing
        // multiline headings.  Authors who want interpretation 1 can
        // put a blank line after the first paragraph:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example071()
        {
            // Source:
            //     Foo
            //     
            //     bar
            //     ---
            //     baz
            // 
            // Expected result:
            //     <p>Foo</p>
            //     <h2>bar</h2>
            //     <p>baz</p>
            
            ExecuteExampleTest(71, "Leaf blocks - Setext headings",
                "Foo\r\n\r\nbar\r\n---\r\nbaz",
                "<p>Foo</p>\r\n<h2>bar</h2>\r\n<p>baz</p>");
        }
        // 
        // 
        // Authors who want interpretation 2 can put blank lines around
        // the thematic break,
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example072()
        {
            // Source:
            //     Foo
            //     bar
            //     
            //     ---
            //     
            //     baz
            // 
            // Expected result:
            //     <p>Foo
            //     bar</p>
            //     <hr />
            //     <p>baz</p>
            
            ExecuteExampleTest(72, "Leaf blocks - Setext headings",
                "Foo\r\nbar\r\n\r\n---\r\n\r\nbaz",
                "<p>Foo\r\nbar</p>\r\n<hr />\r\n<p>baz</p>");
        }
        // 
        // 
        // or use a thematic break that cannot count as a [setext heading
        // underline], such as
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example073()
        {
            // Source:
            //     Foo
            //     bar
            //     * * *
            //     baz
            // 
            // Expected result:
            //     <p>Foo
            //     bar</p>
            //     <hr />
            //     <p>baz</p>
            
            ExecuteExampleTest(73, "Leaf blocks - Setext headings",
                "Foo\r\nbar\r\n* * *\r\nbaz",
                "<p>Foo\r\nbar</p>\r\n<hr />\r\n<p>baz</p>");
        }
        // 
        // 
        // Authors who want interpretation 3 can use backslash escapes:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Setext headings")]
        public void Example074()
        {
            // Source:
            //     Foo
            //     bar
            //     \---
            //     baz
            // 
            // Expected result:
            //     <p>Foo
            //     bar
            //     ---
            //     baz</p>
            
            ExecuteExampleTest(74, "Leaf blocks - Setext headings",
                "Foo\r\nbar\r\n\\---\r\nbaz",
                "<p>Foo\r\nbar\r\n---\r\nbaz</p>");
        }
        // 
        // 
        // ## Indented code blocks
        // 
        // An [indented code block](@) is composed of one or more
        // [indented chunks] separated by blank lines.
        // An [indented chunk](@) is a sequence of non-blank lines,
        // each indented four or more spaces. The contents of the code block are
        // the literal contents of the lines, including trailing
        // [line endings], minus four spaces of indentation.
        // An indented code block has no [info string].
        // 
        // An indented code block cannot interrupt a paragraph, so there must be
        // a blank line between a paragraph and a following indented code block.
        // (A blank line is not needed, however, between a code block and a following
        // paragraph.)
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example075()
        {
            // Source:
            //         a simple
            //           indented code block
            // 
            // Expected result:
            //     <pre><code>a simple
            //       indented code block
            //     </code></pre>
            
            ExecuteExampleTest(75, "Leaf blocks - Indented code blocks",
                "    a simple\r\n      indented code block",
                "<pre><code>a simple\r\n  indented code block\r\n</code></pre>");
        }
        // 
        // 
        // If there is any ambiguity between an interpretation of indentation
        // as a code block and as indicating that material belongs to a [list
        // item][list items], the list item interpretation takes precedence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example076()
        {
            // Source:
            //       - foo
            //     
            //         bar
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(76, "Leaf blocks - Indented code blocks",
                "  - foo\r\n\r\n    bar",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<p>bar</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example077()
        {
            // Source:
            //     1.  foo
            //     
            //         - bar
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>foo</p>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(77, "Leaf blocks - Indented code blocks",
                "1.  foo\r\n\r\n    - bar",
                "<ol>\r\n<li>\r\n<p>foo</p>\r\n<ul>\r\n<li>bar</li>\r\n</ul>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // 
        // The contents of a code block are literal text, and do not get parsed
        // as Markdown:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example078()
        {
            // Source:
            //         <a/>
            //         *hi*
            //     
            //         - one
            // 
            // Expected result:
            //     <pre><code>&lt;a/&gt;
            //     *hi*
            //     
            //     - one
            //     </code></pre>
            
            ExecuteExampleTest(78, "Leaf blocks - Indented code blocks",
                "    <a/>\r\n    *hi*\r\n\r\n    - one",
                "<pre><code>&lt;a/&gt;\r\n*hi*\r\n\r\n- one\r\n</code></pre>");
        }
        // 
        // 
        // Here we have three chunks separated by blank lines:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example079()
        {
            // Source:
            //         chunk1
            //     
            //         chunk2
            //       
            //      
            //      
            //         chunk3
            // 
            // Expected result:
            //     <pre><code>chunk1
            //     
            //     chunk2
            //     
            //     
            //     
            //     chunk3
            //     </code></pre>
            
            ExecuteExampleTest(79, "Leaf blocks - Indented code blocks",
                "    chunk1\r\n\r\n    chunk2\r\n  \r\n \r\n \r\n    chunk3",
                "<pre><code>chunk1\r\n\r\nchunk2\r\n\r\n\r\n\r\nchunk3\r\n</code></pre>");
        }
        // 
        // 
        // Any initial spaces beyond four will be included in the content, even
        // in interior blank lines:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example080()
        {
            // Source:
            //         chunk1
            //           
            //           chunk2
            // 
            // Expected result:
            //     <pre><code>chunk1
            //       
            //       chunk2
            //     </code></pre>
            
            ExecuteExampleTest(80, "Leaf blocks - Indented code blocks",
                "    chunk1\r\n      \r\n      chunk2",
                "<pre><code>chunk1\r\n  \r\n  chunk2\r\n</code></pre>");
        }
        // 
        // 
        // An indented code block cannot interrupt a paragraph.  (This
        // allows hanging indents and the like.)
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example081()
        {
            // Source:
            //     Foo
            //         bar
            //     
            // 
            // Expected result:
            //     <p>Foo
            //     bar</p>
            
            ExecuteExampleTest(81, "Leaf blocks - Indented code blocks",
                "Foo\r\n    bar\r\n",
                "<p>Foo\r\nbar</p>");
        }
        // 
        // 
        // However, any non-blank line with fewer than four leading spaces ends
        // the code block immediately.  So a paragraph may occur immediately
        // after indented code:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example082()
        {
            // Source:
            //         foo
            //     bar
            // 
            // Expected result:
            //     <pre><code>foo
            //     </code></pre>
            //     <p>bar</p>
            
            ExecuteExampleTest(82, "Leaf blocks - Indented code blocks",
                "    foo\r\nbar",
                "<pre><code>foo\r\n</code></pre>\r\n<p>bar</p>");
        }
        // 
        // 
        // And indented code can occur immediately before and after other kinds of
        // blocks:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example083()
        {
            // Source:
            //     # Heading
            //         foo
            //     Heading
            //     ------
            //         foo
            //     ----
            // 
            // Expected result:
            //     <h1>Heading</h1>
            //     <pre><code>foo
            //     </code></pre>
            //     <h2>Heading</h2>
            //     <pre><code>foo
            //     </code></pre>
            //     <hr />
            
            ExecuteExampleTest(83, "Leaf blocks - Indented code blocks",
                "# Heading\r\n    foo\r\nHeading\r\n------\r\n    foo\r\n----",
                "<h1>Heading</h1>\r\n<pre><code>foo\r\n</code></pre>\r\n<h2>Heading</h2>\r\n<pre><code>foo\r\n</code></pre>\r\n<hr />");
        }
        // 
        // 
        // The first line can be indented more than four spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example084()
        {
            // Source:
            //             foo
            //         bar
            // 
            // Expected result:
            //     <pre><code>    foo
            //     bar
            //     </code></pre>
            
            ExecuteExampleTest(84, "Leaf blocks - Indented code blocks",
                "        foo\r\n    bar",
                "<pre><code>    foo\r\nbar\r\n</code></pre>");
        }
        // 
        // 
        // Blank lines preceding or following an indented code block
        // are not included in it:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example085()
        {
            // Source:
            //     
            //         
            //         foo
            //         
            //     
            // 
            // Expected result:
            //     <pre><code>foo
            //     </code></pre>
            
            ExecuteExampleTest(85, "Leaf blocks - Indented code blocks",
                "\r\n    \r\n    foo\r\n    \r\n",
                "<pre><code>foo\r\n</code></pre>");
        }
        // 
        // 
        // Trailing spaces are included in the code block's content:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Indented code blocks")]
        public void Example086()
        {
            // Source:
            //         foo  
            // 
            // Expected result:
            //     <pre><code>foo  
            //     </code></pre>
            
            ExecuteExampleTest(86, "Leaf blocks - Indented code blocks",
                "    foo  ",
                "<pre><code>foo  \r\n</code></pre>");
        }
        // 
        // 
        // 
        // ## Fenced code blocks
        // 
        // A [code fence](@) is a sequence
        // of at least three consecutive backtick characters (`` ` ``) or
        // tildes (`~`).  (Tildes and backticks cannot be mixed.)
        // A [fenced code block](@)
        // begins with a code fence, indented no more than three spaces.
        // 
        // The line with the opening code fence may optionally contain some text
        // following the code fence; this is trimmed of leading and trailing
        // spaces and called the [info string](@).
        // The [info string] may not contain any backtick
        // characters.  (The reason for this restriction is that otherwise
        // some inline code would be incorrectly interpreted as the
        // beginning of a fenced code block.)
        // 
        // The content of the code block consists of all subsequent lines, until
        // a closing [code fence] of the same type as the code block
        // began with (backticks or tildes), and with at least as many backticks
        // or tildes as the opening code fence.  If the leading code fence is
        // indented N spaces, then up to N spaces of indentation are removed from
        // each line of the content (if present).  (If a content line is not
        // indented, it is preserved unchanged.  If it is indented less than N
        // spaces, all of the indentation is removed.)
        // 
        // The closing code fence may be indented up to three spaces, and may be
        // followed only by spaces, which are ignored.  If the end of the
        // containing block (or document) is reached and no closing code fence
        // has been found, the code block contains all of the lines after the
        // opening code fence until the end of the containing block (or
        // document).  (An alternative spec would require backtracking in the
        // event that a closing code fence is not found.  But this makes parsing
        // much less efficient, and there seems to be no real down side to the
        // behavior described here.)
        // 
        // A fenced code block may interrupt a paragraph, and does not require
        // a blank line either before or after.
        // 
        // The content of a code fence is treated as literal text, not parsed
        // as inlines.  The first word of the [info string] is typically used to
        // specify the language of the code sample, and rendered in the `class`
        // attribute of the `code` tag.  However, this spec does not mandate any
        // particular treatment of the [info string].
        // 
        // Here is a simple example with backticks:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example087()
        {
            // Source:
            //     ```
            //     <
            //      >
            //     ```
            // 
            // Expected result:
            //     <pre><code>&lt;
            //      &gt;
            //     </code></pre>
            
            ExecuteExampleTest(87, "Leaf blocks - Fenced code blocks",
                "```\r\n<\r\n >\r\n```",
                "<pre><code>&lt;\r\n &gt;\r\n</code></pre>");
        }
        // 
        // 
        // With tildes:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example088()
        {
            // Source:
            //     ~~~
            //     <
            //      >
            //     ~~~
            // 
            // Expected result:
            //     <pre><code>&lt;
            //      &gt;
            //     </code></pre>
            
            ExecuteExampleTest(88, "Leaf blocks - Fenced code blocks",
                "~~~\r\n<\r\n >\r\n~~~",
                "<pre><code>&lt;\r\n &gt;\r\n</code></pre>");
        }
        // 
        // 
        // The closing code fence must use the same character as the opening
        // fence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example089()
        {
            // Source:
            //     ```
            //     aaa
            //     ~~~
            //     ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //     ~~~
            //     </code></pre>
            
            ExecuteExampleTest(89, "Leaf blocks - Fenced code blocks",
                "```\r\naaa\r\n~~~\r\n```",
                "<pre><code>aaa\r\n~~~\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example090()
        {
            // Source:
            //     ~~~
            //     aaa
            //     ```
            //     ~~~
            // 
            // Expected result:
            //     <pre><code>aaa
            //     ```
            //     </code></pre>
            
            ExecuteExampleTest(90, "Leaf blocks - Fenced code blocks",
                "~~~\r\naaa\r\n```\r\n~~~",
                "<pre><code>aaa\r\n```\r\n</code></pre>");
        }
        // 
        // 
        // The closing code fence must be at least as long as the opening fence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example091()
        {
            // Source:
            //     ````
            //     aaa
            //     ```
            //     ``````
            // 
            // Expected result:
            //     <pre><code>aaa
            //     ```
            //     </code></pre>
            
            ExecuteExampleTest(91, "Leaf blocks - Fenced code blocks",
                "````\r\naaa\r\n```\r\n``````",
                "<pre><code>aaa\r\n```\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example092()
        {
            // Source:
            //     ~~~~
            //     aaa
            //     ~~~
            //     ~~~~
            // 
            // Expected result:
            //     <pre><code>aaa
            //     ~~~
            //     </code></pre>
            
            ExecuteExampleTest(92, "Leaf blocks - Fenced code blocks",
                "~~~~\r\naaa\r\n~~~\r\n~~~~",
                "<pre><code>aaa\r\n~~~\r\n</code></pre>");
        }
        // 
        // 
        // Unclosed code blocks are closed by the end of the document
        // (or the enclosing [block quote][block quotes] or [list item][list items]):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example093()
        {
            // Source:
            //     ```
            // 
            // Expected result:
            //     <pre><code></code></pre>
            
            ExecuteExampleTest(93, "Leaf blocks - Fenced code blocks",
                "```",
                "<pre><code></code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example094()
        {
            // Source:
            //     `````
            //     
            //     ```
            //     aaa
            // 
            // Expected result:
            //     <pre><code>
            //     ```
            //     aaa
            //     </code></pre>
            
            ExecuteExampleTest(94, "Leaf blocks - Fenced code blocks",
                "`````\r\n\r\n```\r\naaa",
                "<pre><code>\r\n```\r\naaa\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example095()
        {
            // Source:
            //     > ```
            //     > aaa
            //     
            //     bbb
            // 
            // Expected result:
            //     <blockquote>
            //     <pre><code>aaa
            //     </code></pre>
            //     </blockquote>
            //     <p>bbb</p>
            
            ExecuteExampleTest(95, "Leaf blocks - Fenced code blocks",
                "> ```\r\n> aaa\r\n\r\nbbb",
                "<blockquote>\r\n<pre><code>aaa\r\n</code></pre>\r\n</blockquote>\r\n<p>bbb</p>");
        }
        // 
        // 
        // A code block can have all empty lines as its content:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example096()
        {
            // Source:
            //     ```
            //     
            //       
            //     ```
            // 
            // Expected result:
            //     <pre><code>
            //       
            //     </code></pre>
            
            ExecuteExampleTest(96, "Leaf blocks - Fenced code blocks",
                "```\r\n\r\n  \r\n```",
                "<pre><code>\r\n  \r\n</code></pre>");
        }
        // 
        // 
        // A code block can be empty:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example097()
        {
            // Source:
            //     ```
            //     ```
            // 
            // Expected result:
            //     <pre><code></code></pre>
            
            ExecuteExampleTest(97, "Leaf blocks - Fenced code blocks",
                "```\r\n```",
                "<pre><code></code></pre>");
        }
        // 
        // 
        // Fences can be indented.  If the opening fence is indented,
        // content lines will have equivalent opening indentation removed,
        // if present:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example098()
        {
            // Source:
            //      ```
            //      aaa
            //     aaa
            //     ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //     aaa
            //     </code></pre>
            
            ExecuteExampleTest(98, "Leaf blocks - Fenced code blocks",
                " ```\r\n aaa\r\naaa\r\n```",
                "<pre><code>aaa\r\naaa\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example099()
        {
            // Source:
            //       ```
            //     aaa
            //       aaa
            //     aaa
            //       ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //     aaa
            //     aaa
            //     </code></pre>
            
            ExecuteExampleTest(99, "Leaf blocks - Fenced code blocks",
                "  ```\r\naaa\r\n  aaa\r\naaa\r\n  ```",
                "<pre><code>aaa\r\naaa\r\naaa\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example100()
        {
            // Source:
            //        ```
            //        aaa
            //         aaa
            //       aaa
            //        ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //      aaa
            //     aaa
            //     </code></pre>
            
            ExecuteExampleTest(100, "Leaf blocks - Fenced code blocks",
                "   ```\r\n   aaa\r\n    aaa\r\n  aaa\r\n   ```",
                "<pre><code>aaa\r\n aaa\r\naaa\r\n</code></pre>");
        }
        // 
        // 
        // Four spaces indentation produces an indented code block:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example101()
        {
            // Source:
            //         ```
            //         aaa
            //         ```
            // 
            // Expected result:
            //     <pre><code>```
            //     aaa
            //     ```
            //     </code></pre>
            
            ExecuteExampleTest(101, "Leaf blocks - Fenced code blocks",
                "    ```\r\n    aaa\r\n    ```",
                "<pre><code>```\r\naaa\r\n```\r\n</code></pre>");
        }
        // 
        // 
        // Closing fences may be indented by 0-3 spaces, and their indentation
        // need not match that of the opening fence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example102()
        {
            // Source:
            //     ```
            //     aaa
            //       ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //     </code></pre>
            
            ExecuteExampleTest(102, "Leaf blocks - Fenced code blocks",
                "```\r\naaa\r\n  ```",
                "<pre><code>aaa\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example103()
        {
            // Source:
            //        ```
            //     aaa
            //       ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //     </code></pre>
            
            ExecuteExampleTest(103, "Leaf blocks - Fenced code blocks",
                "   ```\r\naaa\r\n  ```",
                "<pre><code>aaa\r\n</code></pre>");
        }
        // 
        // 
        // This is not a closing fence, because it is indented 4 spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example104()
        {
            // Source:
            //     ```
            //     aaa
            //         ```
            // 
            // Expected result:
            //     <pre><code>aaa
            //         ```
            //     </code></pre>
            
            ExecuteExampleTest(104, "Leaf blocks - Fenced code blocks",
                "```\r\naaa\r\n    ```",
                "<pre><code>aaa\r\n    ```\r\n</code></pre>");
        }
        // 
        // 
        // 
        // Code fences (opening and closing) cannot contain internal spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example105()
        {
            // Source:
            //     ``` ```
            //     aaa
            // 
            // Expected result:
            //     <p><code></code>
            //     aaa</p>
            
            ExecuteExampleTest(105, "Leaf blocks - Fenced code blocks",
                "``` ```\r\naaa",
                "<p><code></code>\r\naaa</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example106()
        {
            // Source:
            //     ~~~~~~
            //     aaa
            //     ~~~ ~~
            // 
            // Expected result:
            //     <pre><code>aaa
            //     ~~~ ~~
            //     </code></pre>
            
            ExecuteExampleTest(106, "Leaf blocks - Fenced code blocks",
                "~~~~~~\r\naaa\r\n~~~ ~~",
                "<pre><code>aaa\r\n~~~ ~~\r\n</code></pre>");
        }
        // 
        // 
        // Fenced code blocks can interrupt paragraphs, and can be followed
        // directly by paragraphs, without a blank line between:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example107()
        {
            // Source:
            //     foo
            //     ```
            //     bar
            //     ```
            //     baz
            // 
            // Expected result:
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     <p>baz</p>
            
            ExecuteExampleTest(107, "Leaf blocks - Fenced code blocks",
                "foo\r\n```\r\nbar\r\n```\r\nbaz",
                "<p>foo</p>\r\n<pre><code>bar\r\n</code></pre>\r\n<p>baz</p>");
        }
        // 
        // 
        // Other blocks can also occur before and after fenced code blocks
        // without an intervening blank line:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example108()
        {
            // Source:
            //     foo
            //     ---
            //     ~~~
            //     bar
            //     ~~~
            //     # baz
            // 
            // Expected result:
            //     <h2>foo</h2>
            //     <pre><code>bar
            //     </code></pre>
            //     <h1>baz</h1>
            
            ExecuteExampleTest(108, "Leaf blocks - Fenced code blocks",
                "foo\r\n---\r\n~~~\r\nbar\r\n~~~\r\n# baz",
                "<h2>foo</h2>\r\n<pre><code>bar\r\n</code></pre>\r\n<h1>baz</h1>");
        }
        // 
        // 
        // An [info string] can be provided after the opening code fence.
        // Opening and closing spaces will be stripped, and the first word, prefixed
        // with `language-`, is used as the value for the `class` attribute of the
        // `code` element within the enclosing `pre` element.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example109()
        {
            // Source:
            //     ```ruby
            //     def foo(x)
            //       return 3
            //     end
            //     ```
            // 
            // Expected result:
            //     <pre><code class="language-ruby">def foo(x)
            //       return 3
            //     end
            //     </code></pre>
            
            ExecuteExampleTest(109, "Leaf blocks - Fenced code blocks",
                "```ruby\r\ndef foo(x)\r\n  return 3\r\nend\r\n```",
                "<pre><code class=\"language-ruby\">def foo(x)\r\n  return 3\r\nend\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example110()
        {
            // Source:
            //     ~~~~    ruby startline=3 $%@#$
            //     def foo(x)
            //       return 3
            //     end
            //     ~~~~~~~
            // 
            // Expected result:
            //     <pre><code class="language-ruby">def foo(x)
            //       return 3
            //     end
            //     </code></pre>
            
            ExecuteExampleTest(110, "Leaf blocks - Fenced code blocks",
                "~~~~    ruby startline=3 $%@#$\r\ndef foo(x)\r\n  return 3\r\nend\r\n~~~~~~~",
                "<pre><code class=\"language-ruby\">def foo(x)\r\n  return 3\r\nend\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example111()
        {
            // Source:
            //     ````;
            //     ````
            // 
            // Expected result:
            //     <pre><code class="language-;"></code></pre>
            
            ExecuteExampleTest(111, "Leaf blocks - Fenced code blocks",
                "````;\r\n````",
                "<pre><code class=\"language-;\"></code></pre>");
        }
        // 
        // 
        // [Info strings] for backtick code blocks cannot contain backticks:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example112()
        {
            // Source:
            //     ``` aa ```
            //     foo
            // 
            // Expected result:
            //     <p><code>aa</code>
            //     foo</p>
            
            ExecuteExampleTest(112, "Leaf blocks - Fenced code blocks",
                "``` aa ```\r\nfoo",
                "<p><code>aa</code>\r\nfoo</p>");
        }
        // 
        // 
        // Closing code fences cannot have [info strings]:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Fenced code blocks")]
        public void Example113()
        {
            // Source:
            //     ```
            //     ``` aaa
            //     ```
            // 
            // Expected result:
            //     <pre><code>``` aaa
            //     </code></pre>
            
            ExecuteExampleTest(113, "Leaf blocks - Fenced code blocks",
                "```\r\n``` aaa\r\n```",
                "<pre><code>``` aaa\r\n</code></pre>");
        }
        // 
        // 
        // 
        // ## HTML blocks
        // 
        // An [HTML block](@) is a group of lines that is treated
        // as raw HTML (and will not be escaped in HTML output).
        // 
        // There are seven kinds of [HTML block], which can be defined
        // by their start and end conditions.  The block begins with a line that
        // meets a [start condition](@) (after up to three spaces
        // optional indentation).  It ends with the first subsequent line that
        // meets a matching [end condition](@), or the last line of
        // the document, if no line is encountered that meets the
        // [end condition].  If the first line meets both the [start condition]
        // and the [end condition], the block will contain just that line.
        // 
        // 1.  **Start condition:**  line begins with the string `<script`,
        // `<pre`, or `<style` (case-insensitive), followed by whitespace,
        // the string `>`, or the end of the line.\
        // **End condition:**  line contains an end tag
        // `</script>`, `</pre>`, or `</style>` (case-insensitive; it
        // need not match the start tag).
        // 
        // 2.  **Start condition:** line begins with the string `<!--`.\
        // **End condition:**  line contains the string `-->`.
        // 
        // 3.  **Start condition:** line begins with the string `<?`.\
        // **End condition:** line contains the string `?>`.
        // 
        // 4.  **Start condition:** line begins with the string `<!`
        // followed by an uppercase ASCII letter.\
        // **End condition:** line contains the character `>`.
        // 
        // 5.  **Start condition:**  line begins with the string
        // `<![CDATA[`.\
        // **End condition:** line contains the string `]]>`.
        // 
        // 6.  **Start condition:** line begins the string `<` or `</`
        // followed by one of the strings (case-insensitive) `address`,
        // `article`, `aside`, `base`, `basefont`, `blockquote`, `body`,
        // `caption`, `center`, `col`, `colgroup`, `dd`, `details`, `dialog`,
        // `dir`, `div`, `dl`, `dt`, `fieldset`, `figcaption`, `figure`,
        // `footer`, `form`, `frame`, `frameset`, `h1`, `head`, `header`, `hr`,
        // `html`, `iframe`, `legend`, `li`, `link`, `main`, `menu`, `menuitem`,
        // `meta`, `nav`, `noframes`, `ol`, `optgroup`, `option`, `p`, `param`,
        // `section`, `source`, `summary`, `table`, `tbody`, `td`,
        // `tfoot`, `th`, `thead`, `title`, `tr`, `track`, `ul`, followed
        // by [whitespace], the end of the line, the string `>`, or
        // the string `/>`.\
        // **End condition:** line is followed by a [blank line].
        // 
        // 7.  **Start condition:**  line begins with a complete [open tag]
        // or [closing tag] (with any [tag name] other than `script`,
        // `style`, or `pre`) followed only by [whitespace]
        // or the end of the line.\
        // **End condition:** line is followed by a [blank line].
        // 
        // All types of [HTML blocks] except type 7 may interrupt
        // a paragraph.  Blocks of type 7 may not interrupt a paragraph.
        // (This restriction is intended to prevent unwanted interpretation
        // of long tags inside a wrapped paragraph as starting HTML blocks.)
        // 
        // Some simple examples follow.  Here are some basic HTML blocks
        // of type 6:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example114()
        {
            // Source:
            //     <table>
            //       <tr>
            //         <td>
            //                hi
            //         </td>
            //       </tr>
            //     </table>
            //     
            //     okay.
            // 
            // Expected result:
            //     <table>
            //       <tr>
            //         <td>
            //                hi
            //         </td>
            //       </tr>
            //     </table>
            //     <p>okay.</p>
            
            ExecuteExampleTest(114, "Leaf blocks - HTML blocks",
                "<table>\r\n  <tr>\r\n    <td>\r\n           hi\r\n    </td>\r\n  </tr>\r\n</table>\r\n\r\nokay.",
                "<table>\r\n  <tr>\r\n    <td>\r\n           hi\r\n    </td>\r\n  </tr>\r\n</table>\r\n<p>okay.</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example115()
        {
            // Source:
            //      <div>
            //       *hello*
            //              <foo><a>
            // 
            // Expected result:
            //      <div>
            //       *hello*
            //              <foo><a>
            
            ExecuteExampleTest(115, "Leaf blocks - HTML blocks",
                " <div>\r\n  *hello*\r\n         <foo><a>",
                " <div>\r\n  *hello*\r\n         <foo><a>");
        }
        // 
        // 
        // A block can also start with a closing tag:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example116()
        {
            // Source:
            //     </div>
            //     *foo*
            // 
            // Expected result:
            //     </div>
            //     *foo*
            
            ExecuteExampleTest(116, "Leaf blocks - HTML blocks",
                "</div>\r\n*foo*",
                "</div>\r\n*foo*");
        }
        // 
        // 
        // Here we have two HTML blocks with a Markdown paragraph between them:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example117()
        {
            // Source:
            //     <DIV CLASS="foo">
            //     
            //     *Markdown*
            //     
            //     </DIV>
            // 
            // Expected result:
            //     <DIV CLASS="foo">
            //     <p><em>Markdown</em></p>
            //     </DIV>
            
            ExecuteExampleTest(117, "Leaf blocks - HTML blocks",
                "<DIV CLASS=\"foo\">\r\n\r\n*Markdown*\r\n\r\n</DIV>",
                "<DIV CLASS=\"foo\">\r\n<p><em>Markdown</em></p>\r\n</DIV>");
        }
        // 
        // 
        // The tag on the first line can be partial, as long
        // as it is split where there would be whitespace:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example118()
        {
            // Source:
            //     <div id="foo"
            //       class="bar">
            //     </div>
            // 
            // Expected result:
            //     <div id="foo"
            //       class="bar">
            //     </div>
            
            ExecuteExampleTest(118, "Leaf blocks - HTML blocks",
                "<div id=\"foo\"\r\n  class=\"bar\">\r\n</div>",
                "<div id=\"foo\"\r\n  class=\"bar\">\r\n</div>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example119()
        {
            // Source:
            //     <div id="foo" class="bar
            //       baz">
            //     </div>
            // 
            // Expected result:
            //     <div id="foo" class="bar
            //       baz">
            //     </div>
            
            ExecuteExampleTest(119, "Leaf blocks - HTML blocks",
                "<div id=\"foo\" class=\"bar\r\n  baz\">\r\n</div>",
                "<div id=\"foo\" class=\"bar\r\n  baz\">\r\n</div>");
        }
        // 
        // 
        // An open tag need not be closed:
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example120()
        {
            // Source:
            //     <div>
            //     *foo*
            //     
            //     *bar*
            // 
            // Expected result:
            //     <div>
            //     *foo*
            //     <p><em>bar</em></p>
            
            ExecuteExampleTest(120, "Leaf blocks - HTML blocks",
                "<div>\r\n*foo*\r\n\r\n*bar*",
                "<div>\r\n*foo*\r\n<p><em>bar</em></p>");
        }
        // 
        // 
        // 
        // A partial tag need not even be completed (garbage
        // in, garbage out):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example121()
        {
            // Source:
            //     <div id="foo"
            //     *hi*
            // 
            // Expected result:
            //     <div id="foo"
            //     *hi*
            
            ExecuteExampleTest(121, "Leaf blocks - HTML blocks",
                "<div id=\"foo\"\r\n*hi*",
                "<div id=\"foo\"\r\n*hi*");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example122()
        {
            // Source:
            //     <div class
            //     foo
            // 
            // Expected result:
            //     <div class
            //     foo
            
            ExecuteExampleTest(122, "Leaf blocks - HTML blocks",
                "<div class\r\nfoo",
                "<div class\r\nfoo");
        }
        // 
        // 
        // The initial tag doesn't even need to be a valid
        // tag, as long as it starts like one:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example123()
        {
            // Source:
            //     <div *???-&&&-<---
            //     *foo*
            // 
            // Expected result:
            //     <div *???-&&&-<---
            //     *foo*
            
            ExecuteExampleTest(123, "Leaf blocks - HTML blocks",
                "<div *???-&&&-<---\r\n*foo*",
                "<div *???-&&&-<---\r\n*foo*");
        }
        // 
        // 
        // In type 6 blocks, the initial tag need not be on a line by
        // itself:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example124()
        {
            // Source:
            //     <div><a href="bar">*foo*</a></div>
            // 
            // Expected result:
            //     <div><a href="bar">*foo*</a></div>
            
            ExecuteExampleTest(124, "Leaf blocks - HTML blocks",
                "<div><a href=\"bar\">*foo*</a></div>",
                "<div><a href=\"bar\">*foo*</a></div>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example125()
        {
            // Source:
            //     <table><tr><td>
            //     foo
            //     </td></tr></table>
            // 
            // Expected result:
            //     <table><tr><td>
            //     foo
            //     </td></tr></table>
            
            ExecuteExampleTest(125, "Leaf blocks - HTML blocks",
                "<table><tr><td>\r\nfoo\r\n</td></tr></table>",
                "<table><tr><td>\r\nfoo\r\n</td></tr></table>");
        }
        // 
        // 
        // Everything until the next blank line or end of document
        // gets included in the HTML block.  So, in the following
        // example, what looks like a Markdown code block
        // is actually part of the HTML block, which continues until a blank
        // line or the end of the document is reached:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example126()
        {
            // Source:
            //     <div></div>
            //     ``` c
            //     int x = 33;
            //     ```
            // 
            // Expected result:
            //     <div></div>
            //     ``` c
            //     int x = 33;
            //     ```
            
            ExecuteExampleTest(126, "Leaf blocks - HTML blocks",
                "<div></div>\r\n``` c\r\nint x = 33;\r\n```",
                "<div></div>\r\n``` c\r\nint x = 33;\r\n```");
        }
        // 
        // 
        // To start an [HTML block] with a tag that is *not* in the
        // list of block-level tags in (6), you must put the tag by
        // itself on the first line (and it must be complete):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example127()
        {
            // Source:
            //     <a href="foo">
            //     *bar*
            //     </a>
            // 
            // Expected result:
            //     <a href="foo">
            //     *bar*
            //     </a>
            
            ExecuteExampleTest(127, "Leaf blocks - HTML blocks",
                "<a href=\"foo\">\r\n*bar*\r\n</a>",
                "<a href=\"foo\">\r\n*bar*\r\n</a>");
        }
        // 
        // 
        // In type 7 blocks, the [tag name] can be anything:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example128()
        {
            // Source:
            //     <Warning>
            //     *bar*
            //     </Warning>
            // 
            // Expected result:
            //     <Warning>
            //     *bar*
            //     </Warning>
            
            ExecuteExampleTest(128, "Leaf blocks - HTML blocks",
                "<Warning>\r\n*bar*\r\n</Warning>",
                "<Warning>\r\n*bar*\r\n</Warning>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example129()
        {
            // Source:
            //     <i class="foo">
            //     *bar*
            //     </i>
            // 
            // Expected result:
            //     <i class="foo">
            //     *bar*
            //     </i>
            
            ExecuteExampleTest(129, "Leaf blocks - HTML blocks",
                "<i class=\"foo\">\r\n*bar*\r\n</i>",
                "<i class=\"foo\">\r\n*bar*\r\n</i>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example130()
        {
            // Source:
            //     </ins>
            //     *bar*
            // 
            // Expected result:
            //     </ins>
            //     *bar*
            
            ExecuteExampleTest(130, "Leaf blocks - HTML blocks",
                "</ins>\r\n*bar*",
                "</ins>\r\n*bar*");
        }
        // 
        // 
        // These rules are designed to allow us to work with tags that
        // can function as either block-level or inline-level tags.
        // The `<del>` tag is a nice example.  We can surround content with
        // `<del>` tags in three different ways.  In this case, we get a raw
        // HTML block, because the `<del>` tag is on a line by itself:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example131()
        {
            // Source:
            //     <del>
            //     *foo*
            //     </del>
            // 
            // Expected result:
            //     <del>
            //     *foo*
            //     </del>
            
            ExecuteExampleTest(131, "Leaf blocks - HTML blocks",
                "<del>\r\n*foo*\r\n</del>",
                "<del>\r\n*foo*\r\n</del>");
        }
        // 
        // 
        // In this case, we get a raw HTML block that just includes
        // the `<del>` tag (because it ends with the following blank
        // line).  So the contents get interpreted as CommonMark:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example132()
        {
            // Source:
            //     <del>
            //     
            //     *foo*
            //     
            //     </del>
            // 
            // Expected result:
            //     <del>
            //     <p><em>foo</em></p>
            //     </del>
            
            ExecuteExampleTest(132, "Leaf blocks - HTML blocks",
                "<del>\r\n\r\n*foo*\r\n\r\n</del>",
                "<del>\r\n<p><em>foo</em></p>\r\n</del>");
        }
        // 
        // 
        // Finally, in this case, the `<del>` tags are interpreted
        // as [raw HTML] *inside* the CommonMark paragraph.  (Because
        // the tag is not on a line by itself, we get inline HTML
        // rather than an [HTML block].)
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example133()
        {
            // Source:
            //     <del>*foo*</del>
            // 
            // Expected result:
            //     <p><del><em>foo</em></del></p>
            
            ExecuteExampleTest(133, "Leaf blocks - HTML blocks",
                "<del>*foo*</del>",
                "<p><del><em>foo</em></del></p>");
        }
        // 
        // 
        // HTML tags designed to contain literal content
        // (`script`, `style`, `pre`), comments, processing instructions,
        // and declarations are treated somewhat differently.
        // Instead of ending at the first blank line, these blocks
        // end at the first line containing a corresponding end tag.
        // As a result, these blocks can contain blank lines:
        // 
        // A pre tag (type 1):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example134()
        {
            // Source:
            //     <pre language="haskell"><code>
            //     import Text.HTML.TagSoup
            //     
            //     main :: IO ()
            //     main = print $ parseTags tags
            //     </code></pre>
            // 
            // Expected result:
            //     <pre language="haskell"><code>
            //     import Text.HTML.TagSoup
            //     
            //     main :: IO ()
            //     main = print $ parseTags tags
            //     </code></pre>
            
            ExecuteExampleTest(134, "Leaf blocks - HTML blocks",
                "<pre language=\"haskell\"><code>\r\nimport Text.HTML.TagSoup\r\n\r\nmain :: IO ()\r\nmain = print $ parseTags tags\r\n</code></pre>",
                "<pre language=\"haskell\"><code>\r\nimport Text.HTML.TagSoup\r\n\r\nmain :: IO ()\r\nmain = print $ parseTags tags\r\n</code></pre>");
        }
        // 
        // 
        // A script tag (type 1):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example135()
        {
            // Source:
            //     <script type="text/javascript">
            //     // JavaScript example
            //     
            //     document.getElementById("demo").innerHTML = "Hello JavaScript!";
            //     </script>
            // 
            // Expected result:
            //     <script type="text/javascript">
            //     // JavaScript example
            //     
            //     document.getElementById("demo").innerHTML = "Hello JavaScript!";
            //     </script>
            
            ExecuteExampleTest(135, "Leaf blocks - HTML blocks",
                "<script type=\"text/javascript\">\r\n// JavaScript example\r\n\r\ndocument.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\r\n</script>",
                "<script type=\"text/javascript\">\r\n// JavaScript example\r\n\r\ndocument.getElementById(\"demo\").innerHTML = \"Hello JavaScript!\";\r\n</script>");
        }
        // 
        // 
        // A style tag (type 1):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example136()
        {
            // Source:
            //     <style
            //       type="text/css">
            //     h1 {color:red;}
            //     
            //     p {color:blue;}
            //     </style>
            // 
            // Expected result:
            //     <style
            //       type="text/css">
            //     h1 {color:red;}
            //     
            //     p {color:blue;}
            //     </style>
            
            ExecuteExampleTest(136, "Leaf blocks - HTML blocks",
                "<style\r\n  type=\"text/css\">\r\nh1 {color:red;}\r\n\r\np {color:blue;}\r\n</style>",
                "<style\r\n  type=\"text/css\">\r\nh1 {color:red;}\r\n\r\np {color:blue;}\r\n</style>");
        }
        // 
        // 
        // If there is no matching end tag, the block will end at the
        // end of the document (or the enclosing [block quote][block quotes]
        // or [list item][list items]):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example137()
        {
            // Source:
            //     <style
            //       type="text/css">
            //     
            //     foo
            // 
            // Expected result:
            //     <style
            //       type="text/css">
            //     
            //     foo
            
            ExecuteExampleTest(137, "Leaf blocks - HTML blocks",
                "<style\r\n  type=\"text/css\">\r\n\r\nfoo",
                "<style\r\n  type=\"text/css\">\r\n\r\nfoo");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example138()
        {
            // Source:
            //     > <div>
            //     > foo
            //     
            //     bar
            // 
            // Expected result:
            //     <blockquote>
            //     <div>
            //     foo
            //     </blockquote>
            //     <p>bar</p>
            
            ExecuteExampleTest(138, "Leaf blocks - HTML blocks",
                "> <div>\r\n> foo\r\n\r\nbar",
                "<blockquote>\r\n<div>\r\nfoo\r\n</blockquote>\r\n<p>bar</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example139()
        {
            // Source:
            //     - <div>
            //     - foo
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <div>
            //     </li>
            //     <li>foo</li>
            //     </ul>
            
            ExecuteExampleTest(139, "Leaf blocks - HTML blocks",
                "- <div>\r\n- foo",
                "<ul>\r\n<li>\r\n<div>\r\n</li>\r\n<li>foo</li>\r\n</ul>");
        }
        // 
        // 
        // The end tag can occur on the same line as the start tag:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example140()
        {
            // Source:
            //     <style>p{color:red;}</style>
            //     *foo*
            // 
            // Expected result:
            //     <style>p{color:red;}</style>
            //     <p><em>foo</em></p>
            
            ExecuteExampleTest(140, "Leaf blocks - HTML blocks",
                "<style>p{color:red;}</style>\r\n*foo*",
                "<style>p{color:red;}</style>\r\n<p><em>foo</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example141()
        {
            // Source:
            //     <!-- foo -->*bar*
            //     *baz*
            // 
            // Expected result:
            //     <!-- foo -->*bar*
            //     <p><em>baz</em></p>
            
            ExecuteExampleTest(141, "Leaf blocks - HTML blocks",
                "<!-- foo -->*bar*\r\n*baz*",
                "<!-- foo -->*bar*\r\n<p><em>baz</em></p>");
        }
        // 
        // 
        // Note that anything on the last line after the
        // end tag will be included in the [HTML block]:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example142()
        {
            // Source:
            //     <script>
            //     foo
            //     </script>1. *bar*
            // 
            // Expected result:
            //     <script>
            //     foo
            //     </script>1. *bar*
            
            ExecuteExampleTest(142, "Leaf blocks - HTML blocks",
                "<script>\r\nfoo\r\n</script>1. *bar*",
                "<script>\r\nfoo\r\n</script>1. *bar*");
        }
        // 
        // 
        // A comment (type 2):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example143()
        {
            // Source:
            //     <!-- Foo
            //     
            //     bar
            //        baz -->
            // 
            // Expected result:
            //     <!-- Foo
            //     
            //     bar
            //        baz -->
            
            ExecuteExampleTest(143, "Leaf blocks - HTML blocks",
                "<!-- Foo\r\n\r\nbar\r\n   baz -->",
                "<!-- Foo\r\n\r\nbar\r\n   baz -->");
        }
        // 
        // 
        // 
        // A processing instruction (type 3):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example144()
        {
            // Source:
            //     <?php
            //     
            //       echo '>';
            //     
            //     ?>
            // 
            // Expected result:
            //     <?php
            //     
            //       echo '>';
            //     
            //     ?>
            
            ExecuteExampleTest(144, "Leaf blocks - HTML blocks",
                "<?php\r\n\r\n  echo '>';\r\n\r\n?>",
                "<?php\r\n\r\n  echo '>';\r\n\r\n?>");
        }
        // 
        // 
        // A declaration (type 4):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example145()
        {
            // Source:
            //     <!DOCTYPE html>
            // 
            // Expected result:
            //     <!DOCTYPE html>
            
            ExecuteExampleTest(145, "Leaf blocks - HTML blocks",
                "<!DOCTYPE html>",
                "<!DOCTYPE html>");
        }
        // 
        // 
        // CDATA (type 5):
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example146()
        {
            // Source:
            //     <![CDATA[
            //     function matchwo(a,b)
            //     {
            //       if (a < b && a < 0) then {
            //         return 1;
            //     
            //       } else {
            //     
            //         return 0;
            //       }
            //     }
            //     ]]>
            // 
            // Expected result:
            //     <![CDATA[
            //     function matchwo(a,b)
            //     {
            //       if (a < b && a < 0) then {
            //         return 1;
            //     
            //       } else {
            //     
            //         return 0;
            //       }
            //     }
            //     ]]>
            
            ExecuteExampleTest(146, "Leaf blocks - HTML blocks",
                "<![CDATA[\r\nfunction matchwo(a,b)\r\n{\r\n  if (a < b && a < 0) then {\r\n    return 1;\r\n\r\n  } else {\r\n\r\n    return 0;\r\n  }\r\n}\r\n]]>",
                "<![CDATA[\r\nfunction matchwo(a,b)\r\n{\r\n  if (a < b && a < 0) then {\r\n    return 1;\r\n\r\n  } else {\r\n\r\n    return 0;\r\n  }\r\n}\r\n]]>");
        }
        // 
        // 
        // The opening tag can be indented 1-3 spaces, but not 4:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example147()
        {
            // Source:
            //       <!-- foo -->
            //     
            //         <!-- foo -->
            // 
            // Expected result:
            //       <!-- foo -->
            //     <pre><code>&lt;!-- foo --&gt;
            //     </code></pre>
            
            ExecuteExampleTest(147, "Leaf blocks - HTML blocks",
                "  <!-- foo -->\r\n\r\n    <!-- foo -->",
                "  <!-- foo -->\r\n<pre><code>&lt;!-- foo --&gt;\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example148()
        {
            // Source:
            //       <div>
            //     
            //         <div>
            // 
            // Expected result:
            //       <div>
            //     <pre><code>&lt;div&gt;
            //     </code></pre>
            
            ExecuteExampleTest(148, "Leaf blocks - HTML blocks",
                "  <div>\r\n\r\n    <div>",
                "  <div>\r\n<pre><code>&lt;div&gt;\r\n</code></pre>");
        }
        // 
        // 
        // An HTML block of types 1--6 can interrupt a paragraph, and need not be
        // preceded by a blank line.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example149()
        {
            // Source:
            //     Foo
            //     <div>
            //     bar
            //     </div>
            // 
            // Expected result:
            //     <p>Foo</p>
            //     <div>
            //     bar
            //     </div>
            
            ExecuteExampleTest(149, "Leaf blocks - HTML blocks",
                "Foo\r\n<div>\r\nbar\r\n</div>",
                "<p>Foo</p>\r\n<div>\r\nbar\r\n</div>");
        }
        // 
        // 
        // However, a following blank line is needed, except at the end of
        // a document, and except for blocks of types 1--5, above:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example150()
        {
            // Source:
            //     <div>
            //     bar
            //     </div>
            //     *foo*
            // 
            // Expected result:
            //     <div>
            //     bar
            //     </div>
            //     *foo*
            
            ExecuteExampleTest(150, "Leaf blocks - HTML blocks",
                "<div>\r\nbar\r\n</div>\r\n*foo*",
                "<div>\r\nbar\r\n</div>\r\n*foo*");
        }
        // 
        // 
        // HTML blocks of type 7 cannot interrupt a paragraph:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example151()
        {
            // Source:
            //     Foo
            //     <a href="bar">
            //     baz
            // 
            // Expected result:
            //     <p>Foo
            //     <a href="bar">
            //     baz</p>
            
            ExecuteExampleTest(151, "Leaf blocks - HTML blocks",
                "Foo\r\n<a href=\"bar\">\r\nbaz",
                "<p>Foo\r\n<a href=\"bar\">\r\nbaz</p>");
        }
        // 
        // 
        // This rule differs from John Gruber's original Markdown syntax
        // specification, which says:
        // 
        // > The only restrictions are that block-level HTML elements —
        // > e.g. `<div>`, `<table>`, `<pre>`, `<p>`, etc. — must be separated from
        // > surrounding content by blank lines, and the start and end tags of the
        // > block should not be indented with tabs or spaces.
        // 
        // In some ways Gruber's rule is more restrictive than the one given
        // here:
        // 
        // - It requires that an HTML block be preceded by a blank line.
        // - It does not allow the start tag to be indented.
        // - It requires a matching end tag, which it also does not allow to
        //   be indented.
        // 
        // Most Markdown implementations (including some of Gruber's own) do not
        // respect all of these restrictions.
        // 
        // There is one respect, however, in which Gruber's rule is more liberal
        // than the one given here, since it allows blank lines to occur inside
        // an HTML block.  There are two reasons for disallowing them here.
        // First, it removes the need to parse balanced tags, which is
        // expensive and can require backtracking from the end of the document
        // if no matching end tag is found. Second, it provides a very simple
        // and flexible way of including Markdown content inside HTML tags:
        // simply separate the Markdown from the HTML using blank lines:
        // 
        // Compare:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example152()
        {
            // Source:
            //     <div>
            //     
            //     *Emphasized* text.
            //     
            //     </div>
            // 
            // Expected result:
            //     <div>
            //     <p><em>Emphasized</em> text.</p>
            //     </div>
            
            ExecuteExampleTest(152, "Leaf blocks - HTML blocks",
                "<div>\r\n\r\n*Emphasized* text.\r\n\r\n</div>",
                "<div>\r\n<p><em>Emphasized</em> text.</p>\r\n</div>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example153()
        {
            // Source:
            //     <div>
            //     *Emphasized* text.
            //     </div>
            // 
            // Expected result:
            //     <div>
            //     *Emphasized* text.
            //     </div>
            
            ExecuteExampleTest(153, "Leaf blocks - HTML blocks",
                "<div>\r\n*Emphasized* text.\r\n</div>",
                "<div>\r\n*Emphasized* text.\r\n</div>");
        }
        // 
        // 
        // Some Markdown implementations have adopted a convention of
        // interpreting content inside tags as text if the open tag has
        // the attribute `markdown=1`.  The rule given above seems a simpler and
        // more elegant way of achieving the same expressive power, which is also
        // much simpler to parse.
        // 
        // The main potential drawback is that one can no longer paste HTML
        // blocks into Markdown documents with 100% reliability.  However,
        // *in most cases* this will work fine, because the blank lines in
        // HTML are usually followed by HTML block tags.  For example:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example154()
        {
            // Source:
            //     <table>
            //     
            //     <tr>
            //     
            //     <td>
            //     Hi
            //     </td>
            //     
            //     </tr>
            //     
            //     </table>
            // 
            // Expected result:
            //     <table>
            //     <tr>
            //     <td>
            //     Hi
            //     </td>
            //     </tr>
            //     </table>
            
            ExecuteExampleTest(154, "Leaf blocks - HTML blocks",
                "<table>\r\n\r\n<tr>\r\n\r\n<td>\r\nHi\r\n</td>\r\n\r\n</tr>\r\n\r\n</table>",
                "<table>\r\n<tr>\r\n<td>\r\nHi\r\n</td>\r\n</tr>\r\n</table>");
        }
        // 
        // 
        // There are problems, however, if the inner tags are indented
        // *and* separated by spaces, as then they will be interpreted as
        // an indented code block:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - HTML blocks")]
        public void Example155()
        {
            // Source:
            //     <table>
            //     
            //       <tr>
            //     
            //         <td>
            //           Hi
            //         </td>
            //     
            //       </tr>
            //     
            //     </table>
            // 
            // Expected result:
            //     <table>
            //       <tr>
            //     <pre><code>&lt;td&gt;
            //       Hi
            //     &lt;/td&gt;
            //     </code></pre>
            //       </tr>
            //     </table>
            
            ExecuteExampleTest(155, "Leaf blocks - HTML blocks",
                "<table>\r\n\r\n  <tr>\r\n\r\n    <td>\r\n      Hi\r\n    </td>\r\n\r\n  </tr>\r\n\r\n</table>",
                "<table>\r\n  <tr>\r\n<pre><code>&lt;td&gt;\r\n  Hi\r\n&lt;/td&gt;\r\n</code></pre>\r\n  </tr>\r\n</table>");
        }
        // 
        // 
        // Fortunately, blank lines are usually not necessary and can be
        // deleted.  The exception is inside `<pre>` tags, but as described
        // above, raw HTML blocks starting with `<pre>` *can* contain blank
        // lines.
        // 
        // ## Link reference definitions
        // 
        // A [link reference definition](@)
        // consists of a [link label], indented up to three spaces, followed
        // by a colon (`:`), optional [whitespace] (including up to one
        // [line ending]), a [link destination],
        // optional [whitespace] (including up to one
        // [line ending]), and an optional [link
        // title], which if it is present must be separated
        // from the [link destination] by [whitespace].
        // No further [non-whitespace characters] may occur on the line.
        // 
        // A [link reference definition]
        // does not correspond to a structural element of a document.  Instead, it
        // defines a label which can be used in [reference links]
        // and reference-style [images] elsewhere in the document.  [Link
        // reference definitions] can come either before or after the links that use
        // them.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example156()
        {
            // Source:
            //     [foo]: /url "title"
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(156, "Leaf blocks - Link reference definitions",
                "[foo]: /url \"title\"\r\n\r\n[foo]",
                "<p><a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example157()
        {
            // Source:
            //        [foo]: 
            //           /url  
            //                'the title'  
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p><a href="/url" title="the title">foo</a></p>
            
            ExecuteExampleTest(157, "Leaf blocks - Link reference definitions",
                "   [foo]: \r\n      /url  \r\n           'the title'  \r\n\r\n[foo]",
                "<p><a href=\"/url\" title=\"the title\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example158()
        {
            // Source:
            //     [Foo*bar\]]:my_(url) 'title (with parens)'
            //     
            //     [Foo*bar\]]
            // 
            // Expected result:
            //     <p><a href="my_(url)" title="title (with parens)">Foo*bar]</a></p>
            
            ExecuteExampleTest(158, "Leaf blocks - Link reference definitions",
                "[Foo*bar\\]]:my_(url) 'title (with parens)'\r\n\r\n[Foo*bar\\]]",
                "<p><a href=\"my_(url)\" title=\"title (with parens)\">Foo*bar]</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example159()
        {
            // Source:
            //     [Foo bar]:
            //     <my%20url>
            //     'title'
            //     
            //     [Foo bar]
            // 
            // Expected result:
            //     <p><a href="my%20url" title="title">Foo bar</a></p>
            
            ExecuteExampleTest(159, "Leaf blocks - Link reference definitions",
                "[Foo bar]:\r\n<my%20url>\r\n'title'\r\n\r\n[Foo bar]",
                "<p><a href=\"my%20url\" title=\"title\">Foo bar</a></p>");
        }
        // 
        // 
        // The title may extend over multiple lines:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example160()
        {
            // Source:
            //     [foo]: /url '
            //     title
            //     line1
            //     line2
            //     '
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p><a href="/url" title="
            //     title
            //     line1
            //     line2
            //     ">foo</a></p>
            
            ExecuteExampleTest(160, "Leaf blocks - Link reference definitions",
                "[foo]: /url '\r\ntitle\r\nline1\r\nline2\r\n'\r\n\r\n[foo]",
                "<p><a href=\"/url\" title=\"\r\ntitle\r\nline1\r\nline2\r\n\">foo</a></p>");
        }
        // 
        // 
        // However, it may not contain a [blank line]:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example161()
        {
            // Source:
            //     [foo]: /url 'title
            //     
            //     with blank line'
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p>[foo]: /url 'title</p>
            //     <p>with blank line'</p>
            //     <p>[foo]</p>
            
            ExecuteExampleTest(161, "Leaf blocks - Link reference definitions",
                "[foo]: /url 'title\r\n\r\nwith blank line'\r\n\r\n[foo]",
                "<p>[foo]: /url 'title</p>\r\n<p>with blank line'</p>\r\n<p>[foo]</p>");
        }
        // 
        // 
        // The title may be omitted:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example162()
        {
            // Source:
            //     [foo]:
            //     /url
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p><a href="/url">foo</a></p>
            
            ExecuteExampleTest(162, "Leaf blocks - Link reference definitions",
                "[foo]:\r\n/url\r\n\r\n[foo]",
                "<p><a href=\"/url\">foo</a></p>");
        }
        // 
        // 
        // The link destination may not be omitted:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example163()
        {
            // Source:
            //     [foo]:
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p>[foo]:</p>
            //     <p>[foo]</p>
            
            ExecuteExampleTest(163, "Leaf blocks - Link reference definitions",
                "[foo]:\r\n\r\n[foo]",
                "<p>[foo]:</p>\r\n<p>[foo]</p>");
        }
        // 
        // 
        // Both title and destination can contain backslash escapes
        // and literal backslashes:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example164()
        {
            // Source:
            //     [foo]: /url\bar\*baz "foo\"bar\baz"
            //     
            //     [foo]
            // 
            // Expected result:
            //     <p><a href="/url%5Cbar*baz" title="foo&quot;bar\baz">foo</a></p>
            
            ExecuteExampleTest(164, "Leaf blocks - Link reference definitions",
                "[foo]: /url\\bar\\*baz \"foo\\\"bar\\baz\"\r\n\r\n[foo]",
                "<p><a href=\"/url%5Cbar*baz\" title=\"foo&quot;bar\\baz\">foo</a></p>");
        }
        // 
        // 
        // A link can come before its corresponding definition:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example165()
        {
            // Source:
            //     [foo]
            //     
            //     [foo]: url
            // 
            // Expected result:
            //     <p><a href="url">foo</a></p>
            
            ExecuteExampleTest(165, "Leaf blocks - Link reference definitions",
                "[foo]\r\n\r\n[foo]: url",
                "<p><a href=\"url\">foo</a></p>");
        }
        // 
        // 
        // If there are several matching definitions, the first one takes
        // precedence:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example166()
        {
            // Source:
            //     [foo]
            //     
            //     [foo]: first
            //     [foo]: second
            // 
            // Expected result:
            //     <p><a href="first">foo</a></p>
            
            ExecuteExampleTest(166, "Leaf blocks - Link reference definitions",
                "[foo]\r\n\r\n[foo]: first\r\n[foo]: second",
                "<p><a href=\"first\">foo</a></p>");
        }
        // 
        // 
        // As noted in the section on [Links], matching of labels is
        // case-insensitive (see [matches]).
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example167()
        {
            // Source:
            //     [FOO]: /url
            //     
            //     [Foo]
            // 
            // Expected result:
            //     <p><a href="/url">Foo</a></p>
            
            ExecuteExampleTest(167, "Leaf blocks - Link reference definitions",
                "[FOO]: /url\r\n\r\n[Foo]",
                "<p><a href=\"/url\">Foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example168()
        {
            // Source:
            //     [ΑΓΩ]: /φου
            //     
            //     [αγω]
            // 
            // Expected result:
            //     <p><a href="/%CF%86%CE%BF%CF%85">αγω</a></p>
            
            ExecuteExampleTest(168, "Leaf blocks - Link reference definitions",
                "[ΑΓΩ]: /φου\r\n\r\n[αγω]",
                "<p><a href=\"/%CF%86%CE%BF%CF%85\">αγω</a></p>");
        }
        // 
        // 
        // Here is a link reference definition with no corresponding link.
        // It contributes nothing to the document.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example169()
        {
            // Source:
            //     [foo]: /url
            // 
            // Expected result:
            
            ExecuteExampleTest(169, "Leaf blocks - Link reference definitions",
                "[foo]: /url",
                "");
        }
        // 
        // 
        // Here is another one:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example170()
        {
            // Source:
            //     [
            //     foo
            //     ]: /url
            //     bar
            // 
            // Expected result:
            //     <p>bar</p>
            
            ExecuteExampleTest(170, "Leaf blocks - Link reference definitions",
                "[\r\nfoo\r\n]: /url\r\nbar",
                "<p>bar</p>");
        }
        // 
        // 
        // This is not a link reference definition, because there are
        // [non-whitespace characters] after the title:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example171()
        {
            // Source:
            //     [foo]: /url "title" ok
            // 
            // Expected result:
            //     <p>[foo]: /url &quot;title&quot; ok</p>
            
            ExecuteExampleTest(171, "Leaf blocks - Link reference definitions",
                "[foo]: /url \"title\" ok",
                "<p>[foo]: /url &quot;title&quot; ok</p>");
        }
        // 
        // 
        // This is a link reference definition, but it has no title:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example172()
        {
            // Source:
            //     [foo]: /url
            //     "title" ok
            // 
            // Expected result:
            //     <p>&quot;title&quot; ok</p>
            
            ExecuteExampleTest(172, "Leaf blocks - Link reference definitions",
                "[foo]: /url\r\n\"title\" ok",
                "<p>&quot;title&quot; ok</p>");
        }
        // 
        // 
        // This is not a link reference definition, because it is indented
        // four spaces:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example173()
        {
            // Source:
            //         [foo]: /url "title"
            //     
            //     [foo]
            // 
            // Expected result:
            //     <pre><code>[foo]: /url &quot;title&quot;
            //     </code></pre>
            //     <p>[foo]</p>
            
            ExecuteExampleTest(173, "Leaf blocks - Link reference definitions",
                "    [foo]: /url \"title\"\r\n\r\n[foo]",
                "<pre><code>[foo]: /url &quot;title&quot;\r\n</code></pre>\r\n<p>[foo]</p>");
        }
        // 
        // 
        // This is not a link reference definition, because it occurs inside
        // a code block:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example174()
        {
            // Source:
            //     ```
            //     [foo]: /url
            //     ```
            //     
            //     [foo]
            // 
            // Expected result:
            //     <pre><code>[foo]: /url
            //     </code></pre>
            //     <p>[foo]</p>
            
            ExecuteExampleTest(174, "Leaf blocks - Link reference definitions",
                "```\r\n[foo]: /url\r\n```\r\n\r\n[foo]",
                "<pre><code>[foo]: /url\r\n</code></pre>\r\n<p>[foo]</p>");
        }
        // 
        // 
        // A [link reference definition] cannot interrupt a paragraph.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example175()
        {
            // Source:
            //     Foo
            //     [bar]: /baz
            //     
            //     [bar]
            // 
            // Expected result:
            //     <p>Foo
            //     [bar]: /baz</p>
            //     <p>[bar]</p>
            
            ExecuteExampleTest(175, "Leaf blocks - Link reference definitions",
                "Foo\r\n[bar]: /baz\r\n\r\n[bar]",
                "<p>Foo\r\n[bar]: /baz</p>\r\n<p>[bar]</p>");
        }
        // 
        // 
        // However, it can directly follow other block elements, such as headings
        // and thematic breaks, and it need not be followed by a blank line.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example176()
        {
            // Source:
            //     # [Foo]
            //     [foo]: /url
            //     > bar
            // 
            // Expected result:
            //     <h1><a href="/url">Foo</a></h1>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(176, "Leaf blocks - Link reference definitions",
                "# [Foo]\r\n[foo]: /url\r\n> bar",
                "<h1><a href=\"/url\">Foo</a></h1>\r\n<blockquote>\r\n<p>bar</p>\r\n</blockquote>");
        }
        // 
        // 
        // Several [link reference definitions]
        // can occur one after another, without intervening blank lines.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example177()
        {
            // Source:
            //     [foo]: /foo-url "foo"
            //     [bar]: /bar-url
            //       "bar"
            //     [baz]: /baz-url
            //     
            //     [foo],
            //     [bar],
            //     [baz]
            // 
            // Expected result:
            //     <p><a href="/foo-url" title="foo">foo</a>,
            //     <a href="/bar-url" title="bar">bar</a>,
            //     <a href="/baz-url">baz</a></p>
            
            ExecuteExampleTest(177, "Leaf blocks - Link reference definitions",
                "[foo]: /foo-url \"foo\"\r\n[bar]: /bar-url\r\n  \"bar\"\r\n[baz]: /baz-url\r\n\r\n[foo],\r\n[bar],\r\n[baz]",
                "<p><a href=\"/foo-url\" title=\"foo\">foo</a>,\r\n<a href=\"/bar-url\" title=\"bar\">bar</a>,\r\n<a href=\"/baz-url\">baz</a></p>");
        }
        // 
        // 
        // [Link reference definitions] can occur
        // inside block containers, like lists and block quotations.  They
        // affect the entire document, not just the container in which they
        // are defined:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Link reference definitions")]
        public void Example178()
        {
            // Source:
            //     [foo]
            //     
            //     > [foo]: /url
            // 
            // Expected result:
            //     <p><a href="/url">foo</a></p>
            //     <blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(178, "Leaf blocks - Link reference definitions",
                "[foo]\r\n\r\n> [foo]: /url",
                "<p><a href=\"/url\">foo</a></p>\r\n<blockquote>\r\n</blockquote>");
        }
        // 
        // 
        // 
        // ## Paragraphs
        // 
        // A sequence of non-blank lines that cannot be interpreted as other
        // kinds of blocks forms a [paragraph](@).
        // The contents of the paragraph are the result of parsing the
        // paragraph's raw content as inlines.  The paragraph's raw content
        // is formed by concatenating the lines and removing initial and final
        // [whitespace].
        // 
        // A simple example with two paragraphs:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example179()
        {
            // Source:
            //     aaa
            //     
            //     bbb
            // 
            // Expected result:
            //     <p>aaa</p>
            //     <p>bbb</p>
            
            ExecuteExampleTest(179, "Leaf blocks - Paragraphs",
                "aaa\r\n\r\nbbb",
                "<p>aaa</p>\r\n<p>bbb</p>");
        }
        // 
        // 
        // Paragraphs can contain multiple lines, but no blank lines:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example180()
        {
            // Source:
            //     aaa
            //     bbb
            //     
            //     ccc
            //     ddd
            // 
            // Expected result:
            //     <p>aaa
            //     bbb</p>
            //     <p>ccc
            //     ddd</p>
            
            ExecuteExampleTest(180, "Leaf blocks - Paragraphs",
                "aaa\r\nbbb\r\n\r\nccc\r\nddd",
                "<p>aaa\r\nbbb</p>\r\n<p>ccc\r\nddd</p>");
        }
        // 
        // 
        // Multiple blank lines between paragraph have no effect:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example181()
        {
            // Source:
            //     aaa
            //     
            //     
            //     bbb
            // 
            // Expected result:
            //     <p>aaa</p>
            //     <p>bbb</p>
            
            ExecuteExampleTest(181, "Leaf blocks - Paragraphs",
                "aaa\r\n\r\n\r\nbbb",
                "<p>aaa</p>\r\n<p>bbb</p>");
        }
        // 
        // 
        // Leading spaces are skipped:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example182()
        {
            // Source:
            //       aaa
            //      bbb
            // 
            // Expected result:
            //     <p>aaa
            //     bbb</p>
            
            ExecuteExampleTest(182, "Leaf blocks - Paragraphs",
                "  aaa\r\n bbb",
                "<p>aaa\r\nbbb</p>");
        }
        // 
        // 
        // Lines after the first may be indented any amount, since indented
        // code blocks cannot interrupt paragraphs.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example183()
        {
            // Source:
            //     aaa
            //                  bbb
            //                                            ccc
            // 
            // Expected result:
            //     <p>aaa
            //     bbb
            //     ccc</p>
            
            ExecuteExampleTest(183, "Leaf blocks - Paragraphs",
                "aaa\r\n             bbb\r\n                                       ccc",
                "<p>aaa\r\nbbb\r\nccc</p>");
        }
        // 
        // 
        // However, the first line may be indented at most three spaces,
        // or an indented code block will be triggered:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example184()
        {
            // Source:
            //        aaa
            //     bbb
            // 
            // Expected result:
            //     <p>aaa
            //     bbb</p>
            
            ExecuteExampleTest(184, "Leaf blocks - Paragraphs",
                "   aaa\r\nbbb",
                "<p>aaa\r\nbbb</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example185()
        {
            // Source:
            //         aaa
            //     bbb
            // 
            // Expected result:
            //     <pre><code>aaa
            //     </code></pre>
            //     <p>bbb</p>
            
            ExecuteExampleTest(185, "Leaf blocks - Paragraphs",
                "    aaa\r\nbbb",
                "<pre><code>aaa\r\n</code></pre>\r\n<p>bbb</p>");
        }
        // 
        // 
        // Final spaces are stripped before inline parsing, so a paragraph
        // that ends with two or more spaces will not end with a [hard line
        // break]:
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Paragraphs")]
        public void Example186()
        {
            // Source:
            //     aaa     
            //     bbb     
            // 
            // Expected result:
            //     <p>aaa<br />
            //     bbb</p>
            
            ExecuteExampleTest(186, "Leaf blocks - Paragraphs",
                "aaa     \r\nbbb     ",
                "<p>aaa<br />\r\nbbb</p>");
        }
        // 
        // 
        // ## Blank lines
        // 
        // [Blank lines] between block-level elements are ignored,
        // except for the role they play in determining whether a [list]
        // is [tight] or [loose].
        // 
        // Blank lines at the beginning and end of the document are also ignored.
        // 
        [TestMethod]
        [TestCategory("Leaf blocks - Blank lines")]
        public void Example187()
        {
            // Source:
            //       
            //     
            //     aaa
            //       
            //     
            //     # aaa
            //     
            //       
            // 
            // Expected result:
            //     <p>aaa</p>
            //     <h1>aaa</h1>
            
            ExecuteExampleTest(187, "Leaf blocks - Blank lines",
                "  \r\n\r\naaa\r\n  \r\n\r\n# aaa\r\n\r\n  ",
                "<p>aaa</p>\r\n<h1>aaa</h1>");
        }
        // 
        // 
        // 
        // # Container blocks
        // 
        // A [container block] is a block that has other
        // blocks as its contents.  There are two basic kinds of container blocks:
        // [block quotes] and [list items].
        // [Lists] are meta-containers for [list items].
        // 
        // We define the syntax for container blocks recursively.  The general
        // form of the definition is:
        // 
        // > If X is a sequence of blocks, then the result of
        // > transforming X in such-and-such a way is a container of type Y
        // > with these blocks as its content.
        // 
        // So, we explain what counts as a block quote or list item by explaining
        // how these can be *generated* from their contents. This should suffice
        // to define the syntax, although it does not give a recipe for *parsing*
        // these constructions.  (A recipe is provided below in the section entitled
        // [A parsing strategy](#appendix-a-parsing-strategy).)
        // 
        // ## Block quotes
        // 
        // A [block quote marker](@)
        // consists of 0-3 spaces of initial indent, plus (a) the character `>` together
        // with a following space, or (b) a single character `>` not followed by a space.
        // 
        // The following rules define [block quotes]:
        // 
        // 1.  **Basic case.**  If a string of lines *Ls* constitute a sequence
        //     of blocks *Bs*, then the result of prepending a [block quote
        //     marker] to the beginning of each line in *Ls*
        //     is a [block quote](#block-quotes) containing *Bs*.
        // 
        // 2.  **Laziness.**  If a string of lines *Ls* constitute a [block
        //     quote](#block-quotes) with contents *Bs*, then the result of deleting
        //     the initial [block quote marker] from one or
        //     more lines in which the next [non-whitespace character] after the [block
        //     quote marker] is [paragraph continuation
        //     text] is a block quote with *Bs* as its content.
        //     [Paragraph continuation text](@) is text
        //     that will be parsed as part of the content of a paragraph, but does
        //     not occur at the beginning of the paragraph.
        // 
        // 3.  **Consecutiveness.**  A document cannot contain two [block
        //     quotes] in a row unless there is a [blank line] between them.
        // 
        // Nothing else counts as a [block quote](#block-quotes).
        // 
        // Here is a simple example:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example188()
        {
            // Source:
            //     > # Foo
            //     > bar
            //     > baz
            // 
            // Expected result:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>
            
            ExecuteExampleTest(188, "Container blocks - Block quotes",
                "> # Foo\r\n> bar\r\n> baz",
                "<blockquote>\r\n<h1>Foo</h1>\r\n<p>bar\r\nbaz</p>\r\n</blockquote>");
        }
        // 
        // 
        // The spaces after the `>` characters can be omitted:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example189()
        {
            // Source:
            //     ># Foo
            //     >bar
            //     > baz
            // 
            // Expected result:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>
            
            ExecuteExampleTest(189, "Container blocks - Block quotes",
                "># Foo\r\n>bar\r\n> baz",
                "<blockquote>\r\n<h1>Foo</h1>\r\n<p>bar\r\nbaz</p>\r\n</blockquote>");
        }
        // 
        // 
        // The `>` characters can be indented 1-3 spaces:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example190()
        {
            // Source:
            //        > # Foo
            //        > bar
            //      > baz
            // 
            // Expected result:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>
            
            ExecuteExampleTest(190, "Container blocks - Block quotes",
                "   > # Foo\r\n   > bar\r\n > baz",
                "<blockquote>\r\n<h1>Foo</h1>\r\n<p>bar\r\nbaz</p>\r\n</blockquote>");
        }
        // 
        // 
        // Four spaces gives us a code block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example191()
        {
            // Source:
            //         > # Foo
            //         > bar
            //         > baz
            // 
            // Expected result:
            //     <pre><code>&gt; # Foo
            //     &gt; bar
            //     &gt; baz
            //     </code></pre>
            
            ExecuteExampleTest(191, "Container blocks - Block quotes",
                "    > # Foo\r\n    > bar\r\n    > baz",
                "<pre><code>&gt; # Foo\r\n&gt; bar\r\n&gt; baz\r\n</code></pre>");
        }
        // 
        // 
        // The Laziness clause allows us to omit the `>` before a
        // paragraph continuation line:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example192()
        {
            // Source:
            //     > # Foo
            //     > bar
            //     baz
            // 
            // Expected result:
            //     <blockquote>
            //     <h1>Foo</h1>
            //     <p>bar
            //     baz</p>
            //     </blockquote>
            
            ExecuteExampleTest(192, "Container blocks - Block quotes",
                "> # Foo\r\n> bar\r\nbaz",
                "<blockquote>\r\n<h1>Foo</h1>\r\n<p>bar\r\nbaz</p>\r\n</blockquote>");
        }
        // 
        // 
        // A block quote can contain some lazy and some non-lazy
        // continuation lines:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example193()
        {
            // Source:
            //     > bar
            //     baz
            //     > foo
            // 
            // Expected result:
            //     <blockquote>
            //     <p>bar
            //     baz
            //     foo</p>
            //     </blockquote>
            
            ExecuteExampleTest(193, "Container blocks - Block quotes",
                "> bar\r\nbaz\r\n> foo",
                "<blockquote>\r\n<p>bar\r\nbaz\r\nfoo</p>\r\n</blockquote>");
        }
        // 
        // 
        // Laziness only applies to lines that would have been continuations of
        // paragraphs had they been prepended with [block quote markers].
        // For example, the `> ` cannot be omitted in the second line of
        // 
        // ``` markdown
        // > foo
        // > ---
        // ```
        // 
        // without changing the meaning:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example194()
        {
            // Source:
            //     > foo
            //     ---
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <hr />
            
            ExecuteExampleTest(194, "Container blocks - Block quotes",
                "> foo\r\n---",
                "<blockquote>\r\n<p>foo</p>\r\n</blockquote>\r\n<hr />");
        }
        // 
        // 
        // Similarly, if we omit the `> ` in the second line of
        // 
        // ``` markdown
        // > - foo
        // > - bar
        // ```
        // 
        // then the block quote ends after the first line:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example195()
        {
            // Source:
            //     > - foo
            //     - bar
            // 
            // Expected result:
            //     <blockquote>
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     </blockquote>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            
            ExecuteExampleTest(195, "Container blocks - Block quotes",
                "> - foo\r\n- bar",
                "<blockquote>\r\n<ul>\r\n<li>foo</li>\r\n</ul>\r\n</blockquote>\r\n<ul>\r\n<li>bar</li>\r\n</ul>");
        }
        // 
        // 
        // For the same reason, we can't omit the `> ` in front of
        // subsequent lines of an indented or fenced code block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example196()
        {
            // Source:
            //     >     foo
            //         bar
            // 
            // Expected result:
            //     <blockquote>
            //     <pre><code>foo
            //     </code></pre>
            //     </blockquote>
            //     <pre><code>bar
            //     </code></pre>
            
            ExecuteExampleTest(196, "Container blocks - Block quotes",
                ">     foo\r\n    bar",
                "<blockquote>\r\n<pre><code>foo\r\n</code></pre>\r\n</blockquote>\r\n<pre><code>bar\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example197()
        {
            // Source:
            //     > ```
            //     foo
            //     ```
            // 
            // Expected result:
            //     <blockquote>
            //     <pre><code></code></pre>
            //     </blockquote>
            //     <p>foo</p>
            //     <pre><code></code></pre>
            
            ExecuteExampleTest(197, "Container blocks - Block quotes",
                "> ```\r\nfoo\r\n```",
                "<blockquote>\r\n<pre><code></code></pre>\r\n</blockquote>\r\n<p>foo</p>\r\n<pre><code></code></pre>");
        }
        // 
        // 
        // Note that in the following case, we have a paragraph
        // continuation line:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example198()
        {
            // Source:
            //     > foo
            //         - bar
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo
            //     - bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(198, "Container blocks - Block quotes",
                "> foo\r\n    - bar",
                "<blockquote>\r\n<p>foo\r\n- bar</p>\r\n</blockquote>");
        }
        // 
        // 
        // To see why, note that in
        // 
        // ```markdown
        // > foo
        // >     - bar
        // ```
        // 
        // the `- bar` is indented too far to start a list, and can't
        // be an indented code block because indented code blocks cannot
        // interrupt paragraphs, so it is a [paragraph continuation line].
        // 
        // A block quote can be empty:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example199()
        {
            // Source:
            //     >
            // 
            // Expected result:
            //     <blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(199, "Container blocks - Block quotes",
                ">",
                "<blockquote>\r\n</blockquote>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example200()
        {
            // Source:
            //     >
            //     >  
            //     > 
            // 
            // Expected result:
            //     <blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(200, "Container blocks - Block quotes",
                ">\r\n>  \r\n> ",
                "<blockquote>\r\n</blockquote>");
        }
        // 
        // 
        // A block quote can have initial or final blank lines:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example201()
        {
            // Source:
            //     >
            //     > foo
            //     >  
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            
            ExecuteExampleTest(201, "Container blocks - Block quotes",
                ">\r\n> foo\r\n>  ",
                "<blockquote>\r\n<p>foo</p>\r\n</blockquote>");
        }
        // 
        // 
        // A blank line always separates block quotes:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example202()
        {
            // Source:
            //     > foo
            //     
            //     > bar
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo</p>
            //     </blockquote>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(202, "Container blocks - Block quotes",
                "> foo\r\n\r\n> bar",
                "<blockquote>\r\n<p>foo</p>\r\n</blockquote>\r\n<blockquote>\r\n<p>bar</p>\r\n</blockquote>");
        }
        // 
        // 
        // (Most current Markdown implementations, including John Gruber's
        // original `Markdown.pl`, will parse this example as a single block quote
        // with two paragraphs.  But it seems better to allow the author to decide
        // whether two block quotes or one are wanted.)
        // 
        // Consecutiveness means that if we put these block quotes together,
        // we get a single block quote:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example203()
        {
            // Source:
            //     > foo
            //     > bar
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo
            //     bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(203, "Container blocks - Block quotes",
                "> foo\r\n> bar",
                "<blockquote>\r\n<p>foo\r\nbar</p>\r\n</blockquote>");
        }
        // 
        // 
        // To get a block quote with two paragraphs, use:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example204()
        {
            // Source:
            //     > foo
            //     >
            //     > bar
            // 
            // Expected result:
            //     <blockquote>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(204, "Container blocks - Block quotes",
                "> foo\r\n>\r\n> bar",
                "<blockquote>\r\n<p>foo</p>\r\n<p>bar</p>\r\n</blockquote>");
        }
        // 
        // 
        // Block quotes can interrupt paragraphs:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example205()
        {
            // Source:
            //     foo
            //     > bar
            // 
            // Expected result:
            //     <p>foo</p>
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            
            ExecuteExampleTest(205, "Container blocks - Block quotes",
                "foo\r\n> bar",
                "<p>foo</p>\r\n<blockquote>\r\n<p>bar</p>\r\n</blockquote>");
        }
        // 
        // 
        // In general, blank lines are not needed before or after block
        // quotes:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example206()
        {
            // Source:
            //     > aaa
            //     ***
            //     > bbb
            // 
            // Expected result:
            //     <blockquote>
            //     <p>aaa</p>
            //     </blockquote>
            //     <hr />
            //     <blockquote>
            //     <p>bbb</p>
            //     </blockquote>
            
            ExecuteExampleTest(206, "Container blocks - Block quotes",
                "> aaa\r\n***\r\n> bbb",
                "<blockquote>\r\n<p>aaa</p>\r\n</blockquote>\r\n<hr />\r\n<blockquote>\r\n<p>bbb</p>\r\n</blockquote>");
        }
        // 
        // 
        // However, because of laziness, a blank line is needed between
        // a block quote and a following paragraph:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example207()
        {
            // Source:
            //     > bar
            //     baz
            // 
            // Expected result:
            //     <blockquote>
            //     <p>bar
            //     baz</p>
            //     </blockquote>
            
            ExecuteExampleTest(207, "Container blocks - Block quotes",
                "> bar\r\nbaz",
                "<blockquote>\r\n<p>bar\r\nbaz</p>\r\n</blockquote>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example208()
        {
            // Source:
            //     > bar
            //     
            //     baz
            // 
            // Expected result:
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            //     <p>baz</p>
            
            ExecuteExampleTest(208, "Container blocks - Block quotes",
                "> bar\r\n\r\nbaz",
                "<blockquote>\r\n<p>bar</p>\r\n</blockquote>\r\n<p>baz</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example209()
        {
            // Source:
            //     > bar
            //     >
            //     baz
            // 
            // Expected result:
            //     <blockquote>
            //     <p>bar</p>
            //     </blockquote>
            //     <p>baz</p>
            
            ExecuteExampleTest(209, "Container blocks - Block quotes",
                "> bar\r\n>\r\nbaz",
                "<blockquote>\r\n<p>bar</p>\r\n</blockquote>\r\n<p>baz</p>");
        }
        // 
        // 
        // It is a consequence of the Laziness rule that any number
        // of initial `>`s may be omitted on a continuation line of a
        // nested block quote:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example210()
        {
            // Source:
            //     > > > foo
            //     bar
            // 
            // Expected result:
            //     <blockquote>
            //     <blockquote>
            //     <blockquote>
            //     <p>foo
            //     bar</p>
            //     </blockquote>
            //     </blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(210, "Container blocks - Block quotes",
                "> > > foo\r\nbar",
                "<blockquote>\r\n<blockquote>\r\n<blockquote>\r\n<p>foo\r\nbar</p>\r\n</blockquote>\r\n</blockquote>\r\n</blockquote>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example211()
        {
            // Source:
            //     >>> foo
            //     > bar
            //     >>baz
            // 
            // Expected result:
            //     <blockquote>
            //     <blockquote>
            //     <blockquote>
            //     <p>foo
            //     bar
            //     baz</p>
            //     </blockquote>
            //     </blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(211, "Container blocks - Block quotes",
                ">>> foo\r\n> bar\r\n>>baz",
                "<blockquote>\r\n<blockquote>\r\n<blockquote>\r\n<p>foo\r\nbar\r\nbaz</p>\r\n</blockquote>\r\n</blockquote>\r\n</blockquote>");
        }
        // 
        // 
        // When including an indented code block in a block quote,
        // remember that the [block quote marker] includes
        // both the `>` and a following space.  So *five spaces* are needed after
        // the `>`:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Block quotes")]
        public void Example212()
        {
            // Source:
            //     >     code
            //     
            //     >    not code
            // 
            // Expected result:
            //     <blockquote>
            //     <pre><code>code
            //     </code></pre>
            //     </blockquote>
            //     <blockquote>
            //     <p>not code</p>
            //     </blockquote>
            
            ExecuteExampleTest(212, "Container blocks - Block quotes",
                ">     code\r\n\r\n>    not code",
                "<blockquote>\r\n<pre><code>code\r\n</code></pre>\r\n</blockquote>\r\n<blockquote>\r\n<p>not code</p>\r\n</blockquote>");
        }
        // 
        // 
        // 
        // ## List items
        // 
        // A [list marker](@) is a
        // [bullet list marker] or an [ordered list marker].
        // 
        // A [bullet list marker](@)
        // is a `-`, `+`, or `*` character.
        // 
        // An [ordered list marker](@)
        // is a sequence of 1--9 arabic digits (`0-9`), followed by either a
        // `.` character or a `)` character.  (The reason for the length
        // limit is that with 10 digits we start seeing integer overflows
        // in some browsers.)
        // 
        // The following rules define [list items]:
        // 
        // 1.  **Basic case.**  If a sequence of lines *Ls* constitute a sequence of
        //     blocks *Bs* starting with a [non-whitespace character] and not separated
        //     from each other by more than one blank line, and *M* is a list
        //     marker of width *W* followed by 0 < *N* < 5 spaces, then the result
        //     of prepending *M* and the following spaces to the first line of
        //     *Ls*, and indenting subsequent lines of *Ls* by *W + N* spaces, is a
        //     list item with *Bs* as its contents.  The type of the list item
        //     (bullet or ordered) is determined by the type of its list marker.
        //     If the list item is ordered, then it is also assigned a start
        //     number, based on the ordered list marker.
        // 
        // For example, let *Ls* be the lines
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example213()
        {
            // Source:
            //     A paragraph
            //     with two lines.
            //     
            //         indented code
            //     
            //     > A block quote.
            // 
            // Expected result:
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            
            ExecuteExampleTest(213, "Container blocks - List items",
                "A paragraph\r\nwith two lines.\r\n\r\n    indented code\r\n\r\n> A block quote.",
                "<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>");
        }
        // 
        // 
        // And let *M* be the marker `1.`, and *N* = 2.  Then rule #1 says
        // that the following is an ordered list item with start number 1,
        // and the same contents as *Ls*:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example214()
        {
            // Source:
            //     1.  A paragraph
            //         with two lines.
            //     
            //             indented code
            //     
            //         > A block quote.
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(214, "Container blocks - List items",
                "1.  A paragraph\r\n    with two lines.\r\n\r\n        indented code\r\n\r\n    > A block quote.",
                "<ol>\r\n<li>\r\n<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // The most important thing to notice is that the position of
        // the text after the list marker determines how much indentation
        // is needed in subsequent blocks in the list item.  If the list
        // marker takes up two spaces, and there are three spaces between
        // the list marker and the next [non-whitespace character], then blocks
        // must be indented five spaces in order to fall under the list
        // item.
        // 
        // Here are some examples showing how far content must be indented to be
        // put under the list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example215()
        {
            // Source:
            //     - one
            //     
            //      two
            // 
            // Expected result:
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <p>two</p>
            
            ExecuteExampleTest(215, "Container blocks - List items",
                "- one\r\n\r\n two",
                "<ul>\r\n<li>one</li>\r\n</ul>\r\n<p>two</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example216()
        {
            // Source:
            //     - one
            //     
            //       two
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(216, "Container blocks - List items",
                "- one\r\n\r\n  two",
                "<ul>\r\n<li>\r\n<p>one</p>\r\n<p>two</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example217()
        {
            // Source:
            //      -    one
            //     
            //          two
            // 
            // Expected result:
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <pre><code> two
            //     </code></pre>
            
            ExecuteExampleTest(217, "Container blocks - List items",
                " -    one\r\n\r\n     two",
                "<ul>\r\n<li>one</li>\r\n</ul>\r\n<pre><code> two\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example218()
        {
            // Source:
            //      -    one
            //     
            //           two
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(218, "Container blocks - List items",
                " -    one\r\n\r\n      two",
                "<ul>\r\n<li>\r\n<p>one</p>\r\n<p>two</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // It is tempting to think of this in terms of columns:  the continuation
        // blocks must be indented at least to the column of the first
        // [non-whitespace character] after the list marker. However, that is not quite right.
        // The spaces after the list marker determine how much relative indentation
        // is needed.  Which column this indentation reaches will depend on
        // how the list item is embedded in other constructions, as shown by
        // this example:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example219()
        {
            // Source:
            //        > > 1.  one
            //     >>
            //     >>     two
            // 
            // Expected result:
            //     <blockquote>
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <p>one</p>
            //     <p>two</p>
            //     </li>
            //     </ol>
            //     </blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(219, "Container blocks - List items",
                "   > > 1.  one\r\n>>\r\n>>     two",
                "<blockquote>\r\n<blockquote>\r\n<ol>\r\n<li>\r\n<p>one</p>\r\n<p>two</p>\r\n</li>\r\n</ol>\r\n</blockquote>\r\n</blockquote>");
        }
        // 
        // 
        // Here `two` occurs in the same column as the list marker `1.`,
        // but is actually contained in the list item, because there is
        // sufficient indentation after the last containing blockquote marker.
        // 
        // The converse is also possible.  In the following example, the word `two`
        // occurs far to the right of the initial text of the list item, `one`, but
        // it is not considered part of the list item, because it is not indented
        // far enough past the blockquote marker:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example220()
        {
            // Source:
            //     >>- one
            //     >>
            //       >  > two
            // 
            // Expected result:
            //     <blockquote>
            //     <blockquote>
            //     <ul>
            //     <li>one</li>
            //     </ul>
            //     <p>two</p>
            //     </blockquote>
            //     </blockquote>
            
            ExecuteExampleTest(220, "Container blocks - List items",
                ">>- one\r\n>>\r\n  >  > two",
                "<blockquote>\r\n<blockquote>\r\n<ul>\r\n<li>one</li>\r\n</ul>\r\n<p>two</p>\r\n</blockquote>\r\n</blockquote>");
        }
        // 
        // 
        // Note that at least one space is needed between the list marker and
        // any following content, so these are not list items:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example221()
        {
            // Source:
            //     -one
            //     
            //     2.two
            // 
            // Expected result:
            //     <p>-one</p>
            //     <p>2.two</p>
            
            ExecuteExampleTest(221, "Container blocks - List items",
                "-one\r\n\r\n2.two",
                "<p>-one</p>\r\n<p>2.two</p>");
        }
        // 
        // 
        // A list item may not contain blocks that are separated by more than
        // one blank line.  Thus, two blank lines will end a list, unless the
        // two blanks are contained in a [fenced code block].
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example222()
        {
            // Source:
            //     - foo
            //     
            //       bar
            //     
            //     - foo
            //     
            //     
            //       bar
            //     
            //     - ```
            //       foo
            //     
            //     
            //       bar
            //       ```
            //     
            //     - baz
            //     
            //       + ```
            //         foo
            //     
            //     
            //         bar
            //         ```
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     <li>
            //     <p>foo</p>
            //     </li>
            //     </ul>
            //     <p>bar</p>
            //     <ul>
            //     <li>
            //     <pre><code>foo
            //     
            //     
            //     bar
            //     </code></pre>
            //     </li>
            //     <li>
            //     <p>baz</p>
            //     <ul>
            //     <li>
            //     <pre><code>foo
            //     
            //     
            //     bar
            //     </code></pre>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(222, "Container blocks - List items",
                "- foo\r\n\r\n  bar\r\n\r\n- foo\r\n\r\n\r\n  bar\r\n\r\n- ```\r\n  foo\r\n\r\n\r\n  bar\r\n  ```\r\n\r\n- baz\r\n\r\n  + ```\r\n    foo\r\n\r\n\r\n    bar\r\n    ```",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<p>bar</p>\r\n</li>\r\n<li>\r\n<p>foo</p>\r\n</li>\r\n</ul>\r\n<p>bar</p>\r\n<ul>\r\n<li>\r\n<pre><code>foo\r\n\r\n\r\nbar\r\n</code></pre>\r\n</li>\r\n<li>\r\n<p>baz</p>\r\n<ul>\r\n<li>\r\n<pre><code>foo\r\n\r\n\r\nbar\r\n</code></pre>\r\n</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // A list item may contain any kind of block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example223()
        {
            // Source:
            //     1.  foo
            //     
            //         ```
            //         bar
            //         ```
            //     
            //         baz
            //     
            //         > bam
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     <p>baz</p>
            //     <blockquote>
            //     <p>bam</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(223, "Container blocks - List items",
                "1.  foo\r\n\r\n    ```\r\n    bar\r\n    ```\r\n\r\n    baz\r\n\r\n    > bam",
                "<ol>\r\n<li>\r\n<p>foo</p>\r\n<pre><code>bar\r\n</code></pre>\r\n<p>baz</p>\r\n<blockquote>\r\n<p>bam</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // A list item that contains an indented code block will preserve
        // empty lines within the code block verbatim, unless there are two
        // or more empty lines in a row (since as described above, two
        // blank lines end the list):
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example224()
        {
            // Source:
            //     - Foo
            //     
            //           bar
            //     
            //           baz
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>Foo</p>
            //     <pre><code>bar
            //     
            //     baz
            //     </code></pre>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(224, "Container blocks - List items",
                "- Foo\r\n\r\n      bar\r\n\r\n      baz",
                "<ul>\r\n<li>\r\n<p>Foo</p>\r\n<pre><code>bar\r\n\r\nbaz\r\n</code></pre>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example225()
        {
            // Source:
            //     - Foo
            //     
            //           bar
            //     
            //     
            //           baz
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>Foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     </ul>
            //     <pre><code>  baz
            //     </code></pre>
            
            ExecuteExampleTest(225, "Container blocks - List items",
                "- Foo\r\n\r\n      bar\r\n\r\n\r\n      baz",
                "<ul>\r\n<li>\r\n<p>Foo</p>\r\n<pre><code>bar\r\n</code></pre>\r\n</li>\r\n</ul>\r\n<pre><code>  baz\r\n</code></pre>");
        }
        // 
        // 
        // Note that ordered list start numbers must be nine digits or less:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example226()
        {
            // Source:
            //     123456789. ok
            // 
            // Expected result:
            //     <ol start="123456789">
            //     <li>ok</li>
            //     </ol>
            
            ExecuteExampleTest(226, "Container blocks - List items",
                "123456789. ok",
                "<ol start=\"123456789\">\r\n<li>ok</li>\r\n</ol>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example227()
        {
            // Source:
            //     1234567890. not ok
            // 
            // Expected result:
            //     <p>1234567890. not ok</p>
            
            ExecuteExampleTest(227, "Container blocks - List items",
                "1234567890. not ok",
                "<p>1234567890. not ok</p>");
        }
        // 
        // 
        // A start number may begin with 0s:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example228()
        {
            // Source:
            //     0. ok
            // 
            // Expected result:
            //     <ol start="0">
            //     <li>ok</li>
            //     </ol>
            
            ExecuteExampleTest(228, "Container blocks - List items",
                "0. ok",
                "<ol start=\"0\">\r\n<li>ok</li>\r\n</ol>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example229()
        {
            // Source:
            //     003. ok
            // 
            // Expected result:
            //     <ol start="3">
            //     <li>ok</li>
            //     </ol>
            
            ExecuteExampleTest(229, "Container blocks - List items",
                "003. ok",
                "<ol start=\"3\">\r\n<li>ok</li>\r\n</ol>");
        }
        // 
        // 
        // A start number may not be negative:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example230()
        {
            // Source:
            //     -1. not ok
            // 
            // Expected result:
            //     <p>-1. not ok</p>
            
            ExecuteExampleTest(230, "Container blocks - List items",
                "-1. not ok",
                "<p>-1. not ok</p>");
        }
        // 
        // 
        // 
        // 2.  **Item starting with indented code.**  If a sequence of lines *Ls*
        //     constitute a sequence of blocks *Bs* starting with an indented code
        //     block and not separated from each other by more than one blank line,
        //     and *M* is a list marker of width *W* followed by
        //     one space, then the result of prepending *M* and the following
        //     space to the first line of *Ls*, and indenting subsequent lines of
        //     *Ls* by *W + 1* spaces, is a list item with *Bs* as its contents.
        //     If a line is empty, then it need not be indented.  The type of the
        //     list item (bullet or ordered) is determined by the type of its list
        //     marker.  If the list item is ordered, then it is also assigned a
        //     start number, based on the ordered list marker.
        // 
        // An indented code block will have to be indented four spaces beyond
        // the edge of the region where text will be included in the list item.
        // In the following case that is 6 spaces:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example231()
        {
            // Source:
            //     - foo
            //     
            //           bar
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(231, "Container blocks - List items",
                "- foo\r\n\r\n      bar",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<pre><code>bar\r\n</code></pre>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // And in this case it is 11 spaces:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example232()
        {
            // Source:
            //       10.  foo
            //     
            //                bar
            // 
            // Expected result:
            //     <ol start="10">
            //     <li>
            //     <p>foo</p>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(232, "Container blocks - List items",
                "  10.  foo\r\n\r\n           bar",
                "<ol start=\"10\">\r\n<li>\r\n<p>foo</p>\r\n<pre><code>bar\r\n</code></pre>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // If the *first* block in the list item is an indented code block,
        // then by rule #2, the contents must be indented *one* space after the
        // list marker:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example233()
        {
            // Source:
            //         indented code
            //     
            //     paragraph
            //     
            //         more code
            // 
            // Expected result:
            //     <pre><code>indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>
            
            ExecuteExampleTest(233, "Container blocks - List items",
                "    indented code\r\n\r\nparagraph\r\n\r\n    more code",
                "<pre><code>indented code\r\n</code></pre>\r\n<p>paragraph</p>\r\n<pre><code>more code\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example234()
        {
            // Source:
            //     1.     indented code
            //     
            //        paragraph
            //     
            //            more code
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <pre><code>indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(234, "Container blocks - List items",
                "1.     indented code\r\n\r\n   paragraph\r\n\r\n       more code",
                "<ol>\r\n<li>\r\n<pre><code>indented code\r\n</code></pre>\r\n<p>paragraph</p>\r\n<pre><code>more code\r\n</code></pre>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Note that an additional space indent is interpreted as space
        // inside the code block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example235()
        {
            // Source:
            //     1.      indented code
            //     
            //        paragraph
            //     
            //            more code
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <pre><code> indented code
            //     </code></pre>
            //     <p>paragraph</p>
            //     <pre><code>more code
            //     </code></pre>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(235, "Container blocks - List items",
                "1.      indented code\r\n\r\n   paragraph\r\n\r\n       more code",
                "<ol>\r\n<li>\r\n<pre><code> indented code\r\n</code></pre>\r\n<p>paragraph</p>\r\n<pre><code>more code\r\n</code></pre>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Note that rules #1 and #2 only apply to two cases:  (a) cases
        // in which the lines to be included in a list item begin with a
        // [non-whitespace character], and (b) cases in which
        // they begin with an indented code
        // block.  In a case like the following, where the first block begins with
        // a three-space indent, the rules do not allow us to form a list item by
        // indenting the whole thing and prepending a list marker:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example236()
        {
            // Source:
            //        foo
            //     
            //     bar
            // 
            // Expected result:
            //     <p>foo</p>
            //     <p>bar</p>
            
            ExecuteExampleTest(236, "Container blocks - List items",
                "   foo\r\n\r\nbar",
                "<p>foo</p>\r\n<p>bar</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example237()
        {
            // Source:
            //     -    foo
            //     
            //       bar
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <p>bar</p>
            
            ExecuteExampleTest(237, "Container blocks - List items",
                "-    foo\r\n\r\n  bar",
                "<ul>\r\n<li>foo</li>\r\n</ul>\r\n<p>bar</p>");
        }
        // 
        // 
        // This is not a significant restriction, because when a block begins
        // with 1-3 spaces indent, the indentation can always be removed without
        // a change in interpretation, allowing rule #1 to be applied.  So, in
        // the above case:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example238()
        {
            // Source:
            //     -  foo
            //     
            //        bar
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>bar</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(238, "Container blocks - List items",
                "-  foo\r\n\r\n   bar",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<p>bar</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // 3.  **Item starting with a blank line.**  If a sequence of lines *Ls*
        //     starting with a single [blank line] constitute a (possibly empty)
        //     sequence of blocks *Bs*, not separated from each other by more than
        //     one blank line, and *M* is a list marker of width *W*,
        //     then the result of prepending *M* to the first line of *Ls*, and
        //     indenting subsequent lines of *Ls* by *W + 1* spaces, is a list
        //     item with *Bs* as its contents.
        //     If a line is empty, then it need not be indented.  The type of the
        //     list item (bullet or ordered) is determined by the type of its list
        //     marker.  If the list item is ordered, then it is also assigned a
        //     start number, based on the ordered list marker.
        // 
        // Here are some list items that start with a blank line but are not empty:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example239()
        {
            // Source:
            //     -
            //       foo
            //     -
            //       ```
            //       bar
            //       ```
            //     -
            //           baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li>
            //     <pre><code>bar
            //     </code></pre>
            //     </li>
            //     <li>
            //     <pre><code>baz
            //     </code></pre>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(239, "Container blocks - List items",
                "-\r\n  foo\r\n-\r\n  ```\r\n  bar\r\n  ```\r\n-\r\n      baz",
                "<ul>\r\n<li>foo</li>\r\n<li>\r\n<pre><code>bar\r\n</code></pre>\r\n</li>\r\n<li>\r\n<pre><code>baz\r\n</code></pre>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // A list item can begin with at most one blank line.
        // In the following example, `foo` is not part of the list
        // item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example240()
        {
            // Source:
            //     -
            //     
            //       foo
            // 
            // Expected result:
            //     <ul>
            //     <li></li>
            //     </ul>
            //     <p>foo</p>
            
            ExecuteExampleTest(240, "Container blocks - List items",
                "-\r\n\r\n  foo",
                "<ul>\r\n<li></li>\r\n</ul>\r\n<p>foo</p>");
        }
        // 
        // 
        // Here is an empty bullet list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example241()
        {
            // Source:
            //     - foo
            //     -
            //     - bar
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ul>
            
            ExecuteExampleTest(241, "Container blocks - List items",
                "- foo\r\n-\r\n- bar",
                "<ul>\r\n<li>foo</li>\r\n<li></li>\r\n<li>bar</li>\r\n</ul>");
        }
        // 
        // 
        // It does not matter whether there are spaces following the [list marker]:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example242()
        {
            // Source:
            //     - foo
            //     -   
            //     - bar
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ul>
            
            ExecuteExampleTest(242, "Container blocks - List items",
                "- foo\r\n-   \r\n- bar",
                "<ul>\r\n<li>foo</li>\r\n<li></li>\r\n<li>bar</li>\r\n</ul>");
        }
        // 
        // 
        // Here is an empty ordered list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example243()
        {
            // Source:
            //     1. foo
            //     2.
            //     3. bar
            // 
            // Expected result:
            //     <ol>
            //     <li>foo</li>
            //     <li></li>
            //     <li>bar</li>
            //     </ol>
            
            ExecuteExampleTest(243, "Container blocks - List items",
                "1. foo\r\n2.\r\n3. bar",
                "<ol>\r\n<li>foo</li>\r\n<li></li>\r\n<li>bar</li>\r\n</ol>");
        }
        // 
        // 
        // A list may start or end with an empty list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example244()
        {
            // Source:
            //     *
            // 
            // Expected result:
            //     <ul>
            //     <li></li>
            //     </ul>
            
            ExecuteExampleTest(244, "Container blocks - List items",
                "*",
                "<ul>\r\n<li></li>\r\n</ul>");
        }
        // 
        // 
        // 
        // 4.  **Indentation.**  If a sequence of lines *Ls* constitutes a list item
        //     according to rule #1, #2, or #3, then the result of indenting each line
        //     of *Ls* by 1-3 spaces (the same for each line) also constitutes a
        //     list item with the same contents and attributes.  If a line is
        //     empty, then it need not be indented.
        // 
        // Indented one space:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example245()
        {
            // Source:
            //      1.  A paragraph
            //          with two lines.
            //     
            //              indented code
            //     
            //          > A block quote.
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(245, "Container blocks - List items",
                " 1.  A paragraph\r\n     with two lines.\r\n\r\n         indented code\r\n\r\n     > A block quote.",
                "<ol>\r\n<li>\r\n<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Indented two spaces:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example246()
        {
            // Source:
            //       1.  A paragraph
            //           with two lines.
            //     
            //               indented code
            //     
            //           > A block quote.
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(246, "Container blocks - List items",
                "  1.  A paragraph\r\n      with two lines.\r\n\r\n          indented code\r\n\r\n      > A block quote.",
                "<ol>\r\n<li>\r\n<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Indented three spaces:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example247()
        {
            // Source:
            //        1.  A paragraph
            //            with two lines.
            //     
            //                indented code
            //     
            //            > A block quote.
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(247, "Container blocks - List items",
                "   1.  A paragraph\r\n       with two lines.\r\n\r\n           indented code\r\n\r\n       > A block quote.",
                "<ol>\r\n<li>\r\n<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Four spaces indent gives a code block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example248()
        {
            // Source:
            //         1.  A paragraph
            //             with two lines.
            //     
            //                 indented code
            //     
            //             > A block quote.
            // 
            // Expected result:
            //     <pre><code>1.  A paragraph
            //         with two lines.
            //     
            //             indented code
            //     
            //         &gt; A block quote.
            //     </code></pre>
            
            ExecuteExampleTest(248, "Container blocks - List items",
                "    1.  A paragraph\r\n        with two lines.\r\n\r\n            indented code\r\n\r\n        > A block quote.",
                "<pre><code>1.  A paragraph\r\n    with two lines.\r\n\r\n        indented code\r\n\r\n    &gt; A block quote.\r\n</code></pre>");
        }
        // 
        // 
        // 
        // 5.  **Laziness.**  If a string of lines *Ls* constitute a [list
        //     item](#list-items) with contents *Bs*, then the result of deleting
        //     some or all of the indentation from one or more lines in which the
        //     next [non-whitespace character] after the indentation is
        //     [paragraph continuation text] is a
        //     list item with the same contents and attributes.  The unindented
        //     lines are called
        //     [lazy continuation line](@)s.
        // 
        // Here is an example with [lazy continuation lines]:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example249()
        {
            // Source:
            //       1.  A paragraph
            //     with two lines.
            //     
            //               indented code
            //     
            //           > A block quote.
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>A paragraph
            //     with two lines.</p>
            //     <pre><code>indented code
            //     </code></pre>
            //     <blockquote>
            //     <p>A block quote.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(249, "Container blocks - List items",
                "  1.  A paragraph\r\nwith two lines.\r\n\r\n          indented code\r\n\r\n      > A block quote.",
                "<ol>\r\n<li>\r\n<p>A paragraph\r\nwith two lines.</p>\r\n<pre><code>indented code\r\n</code></pre>\r\n<blockquote>\r\n<p>A block quote.</p>\r\n</blockquote>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Indentation can be partially deleted:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example250()
        {
            // Source:
            //       1.  A paragraph
            //         with two lines.
            // 
            // Expected result:
            //     <ol>
            //     <li>A paragraph
            //     with two lines.</li>
            //     </ol>
            
            ExecuteExampleTest(250, "Container blocks - List items",
                "  1.  A paragraph\r\n    with two lines.",
                "<ol>\r\n<li>A paragraph\r\nwith two lines.</li>\r\n</ol>");
        }
        // 
        // 
        // These examples show how laziness can work in nested structures:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example251()
        {
            // Source:
            //     > 1. > Blockquote
            //     continued here.
            // 
            // Expected result:
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <blockquote>
            //     <p>Blockquote
            //     continued here.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            //     </blockquote>
            
            ExecuteExampleTest(251, "Container blocks - List items",
                "> 1. > Blockquote\r\ncontinued here.",
                "<blockquote>\r\n<ol>\r\n<li>\r\n<blockquote>\r\n<p>Blockquote\r\ncontinued here.</p>\r\n</blockquote>\r\n</li>\r\n</ol>\r\n</blockquote>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example252()
        {
            // Source:
            //     > 1. > Blockquote
            //     > continued here.
            // 
            // Expected result:
            //     <blockquote>
            //     <ol>
            //     <li>
            //     <blockquote>
            //     <p>Blockquote
            //     continued here.</p>
            //     </blockquote>
            //     </li>
            //     </ol>
            //     </blockquote>
            
            ExecuteExampleTest(252, "Container blocks - List items",
                "> 1. > Blockquote\r\n> continued here.",
                "<blockquote>\r\n<ol>\r\n<li>\r\n<blockquote>\r\n<p>Blockquote\r\ncontinued here.</p>\r\n</blockquote>\r\n</li>\r\n</ol>\r\n</blockquote>");
        }
        // 
        // 
        // 
        // 6.  **That's all.** Nothing that is not counted as a list item by rules
        //     #1--5 counts as a [list item](#list-items).
        // 
        // The rules for sublists follow from the general rules above.  A sublist
        // must be indented the same number of spaces a paragraph would need to be
        // in order to be included in the list item.
        // 
        // So, in this case we need two spaces indent:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example253()
        {
            // Source:
            //     - foo
            //       - bar
            //         - baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(253, "Container blocks - List items",
                "- foo\r\n  - bar\r\n    - baz",
                "<ul>\r\n<li>foo\r\n<ul>\r\n<li>bar\r\n<ul>\r\n<li>baz</li>\r\n</ul>\r\n</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // One is not enough:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example254()
        {
            // Source:
            //     - foo
            //      - bar
            //       - baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     <li>baz</li>
            //     </ul>
            
            ExecuteExampleTest(254, "Container blocks - List items",
                "- foo\r\n - bar\r\n  - baz",
                "<ul>\r\n<li>foo</li>\r\n<li>bar</li>\r\n<li>baz</li>\r\n</ul>");
        }
        // 
        // 
        // Here we need four, because the list marker is wider:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example255()
        {
            // Source:
            //     10) foo
            //         - bar
            // 
            // Expected result:
            //     <ol start="10">
            //     <li>foo
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(255, "Container blocks - List items",
                "10) foo\r\n    - bar",
                "<ol start=\"10\">\r\n<li>foo\r\n<ul>\r\n<li>bar</li>\r\n</ul>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Three is not enough:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example256()
        {
            // Source:
            //     10) foo
            //        - bar
            // 
            // Expected result:
            //     <ol start="10">
            //     <li>foo</li>
            //     </ol>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            
            ExecuteExampleTest(256, "Container blocks - List items",
                "10) foo\r\n   - bar",
                "<ol start=\"10\">\r\n<li>foo</li>\r\n</ol>\r\n<ul>\r\n<li>bar</li>\r\n</ul>");
        }
        // 
        // 
        // A list may be the first block in a list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example257()
        {
            // Source:
            //     - - foo
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(257, "Container blocks - List items",
                "- - foo",
                "<ul>\r\n<li>\r\n<ul>\r\n<li>foo</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example258()
        {
            // Source:
            //     1. - 2. foo
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <ul>
            //     <li>
            //     <ol start="2">
            //     <li>foo</li>
            //     </ol>
            //     </li>
            //     </ul>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(258, "Container blocks - List items",
                "1. - 2. foo",
                "<ol>\r\n<li>\r\n<ul>\r\n<li>\r\n<ol start=\"2\">\r\n<li>foo</li>\r\n</ol>\r\n</li>\r\n</ul>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // A list item can contain a heading:
        // 
        [TestMethod]
        [TestCategory("Container blocks - List items")]
        public void Example259()
        {
            // Source:
            //     - # Foo
            //     - Bar
            //       ---
            //       baz
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <h1>Foo</h1>
            //     </li>
            //     <li>
            //     <h2>Bar</h2>
            //     baz</li>
            //     </ul>
            
            ExecuteExampleTest(259, "Container blocks - List items",
                "- # Foo\r\n- Bar\r\n  ---\r\n  baz",
                "<ul>\r\n<li>\r\n<h1>Foo</h1>\r\n</li>\r\n<li>\r\n<h2>Bar</h2>\r\nbaz</li>\r\n</ul>");
        }
        // 
        // 
        // ### Motivation
        // 
        // John Gruber's Markdown spec says the following about list items:
        // 
        // 1. "List markers typically start at the left margin, but may be indented
        //    by up to three spaces. List markers must be followed by one or more
        //    spaces or a tab."
        // 
        // 2. "To make lists look nice, you can wrap items with hanging indents....
        //    But if you don't want to, you don't have to."
        // 
        // 3. "List items may consist of multiple paragraphs. Each subsequent
        //    paragraph in a list item must be indented by either 4 spaces or one
        //    tab."
        // 
        // 4. "It looks nice if you indent every line of the subsequent paragraphs,
        //    but here again, Markdown will allow you to be lazy."
        // 
        // 5. "To put a blockquote within a list item, the blockquote's `>`
        //    delimiters need to be indented."
        // 
        // 6. "To put a code block within a list item, the code block needs to be
        //    indented twice — 8 spaces or two tabs."
        // 
        // These rules specify that a paragraph under a list item must be indented
        // four spaces (presumably, from the left margin, rather than the start of
        // the list marker, but this is not said), and that code under a list item
        // must be indented eight spaces instead of the usual four.  They also say
        // that a block quote must be indented, but not by how much; however, the
        // example given has four spaces indentation.  Although nothing is said
        // about other kinds of block-level content, it is certainly reasonable to
        // infer that *all* block elements under a list item, including other
        // lists, must be indented four spaces.  This principle has been called the
        // *four-space rule*.
        // 
        // The four-space rule is clear and principled, and if the reference
        // implementation `Markdown.pl` had followed it, it probably would have
        // become the standard.  However, `Markdown.pl` allowed paragraphs and
        // sublists to start with only two spaces indentation, at least on the
        // outer level.  Worse, its behavior was inconsistent: a sublist of an
        // outer-level list needed two spaces indentation, but a sublist of this
        // sublist needed three spaces.  It is not surprising, then, that different
        // implementations of Markdown have developed very different rules for
        // determining what comes under a list item.  (Pandoc and python-Markdown,
        // for example, stuck with Gruber's syntax description and the four-space
        // rule, while discount, redcarpet, marked, PHP Markdown, and others
        // followed `Markdown.pl`'s behavior more closely.)
        // 
        // Unfortunately, given the divergences between implementations, there
        // is no way to give a spec for list items that will be guaranteed not
        // to break any existing documents.  However, the spec given here should
        // correctly handle lists formatted with either the four-space rule or
        // the more forgiving `Markdown.pl` behavior, provided they are laid out
        // in a way that is natural for a human to read.
        // 
        // The strategy here is to let the width and indentation of the list marker
        // determine the indentation necessary for blocks to fall under the list
        // item, rather than having a fixed and arbitrary number.  The writer can
        // think of the body of the list item as a unit which gets indented to the
        // right enough to fit the list marker (and any indentation on the list
        // marker).  (The laziness rule, #5, then allows continuation lines to be
        // unindented if needed.)
        // 
        // This rule is superior, we claim, to any rule requiring a fixed level of
        // indentation from the margin.  The four-space rule is clear but
        // unnatural. It is quite unintuitive that
        // 
        // ``` markdown
        // - foo
        // 
        //   bar
        // 
        //   - baz
        // ```
        // 
        // should be parsed as two lists with an intervening paragraph,
        // 
        // ``` html
        // <ul>
        // <li>foo</li>
        // </ul>
        // <p>bar</p>
        // <ul>
        // <li>baz</li>
        // </ul>
        // ```
        // 
        // as the four-space rule demands, rather than a single list,
        // 
        // ``` html
        // <ul>
        // <li>
        // <p>foo</p>
        // <p>bar</p>
        // <ul>
        // <li>baz</li>
        // </ul>
        // </li>
        // </ul>
        // ```
        // 
        // The choice of four spaces is arbitrary.  It can be learned, but it is
        // not likely to be guessed, and it trips up beginners regularly.
        // 
        // Would it help to adopt a two-space rule?  The problem is that such
        // a rule, together with the rule allowing 1--3 spaces indentation of the
        // initial list marker, allows text that is indented *less than* the
        // original list marker to be included in the list item. For example,
        // `Markdown.pl` parses
        // 
        // ``` markdown
        //    - one
        // 
        //   two
        // ```
        // 
        // as a single list item, with `two` a continuation paragraph:
        // 
        // ``` html
        // <ul>
        // <li>
        // <p>one</p>
        // <p>two</p>
        // </li>
        // </ul>
        // ```
        // 
        // and similarly
        // 
        // ``` markdown
        // >   - one
        // >
        // >  two
        // ```
        // 
        // as
        // 
        // ``` html
        // <blockquote>
        // <ul>
        // <li>
        // <p>one</p>
        // <p>two</p>
        // </li>
        // </ul>
        // </blockquote>
        // ```
        // 
        // This is extremely unintuitive.
        // 
        // Rather than requiring a fixed indent from the margin, we could require
        // a fixed indent (say, two spaces, or even one space) from the list marker (which
        // may itself be indented).  This proposal would remove the last anomaly
        // discussed.  Unlike the spec presented above, it would count the following
        // as a list item with a subparagraph, even though the paragraph `bar`
        // is not indented as far as the first paragraph `foo`:
        // 
        // ``` markdown
        //  10. foo
        // 
        //    bar  
        // ```
        // 
        // Arguably this text does read like a list item with `bar` as a subparagraph,
        // which may count in favor of the proposal.  However, on this proposal indented
        // code would have to be indented six spaces after the list marker.  And this
        // would break a lot of existing Markdown, which has the pattern:
        // 
        // ``` markdown
        // 1.  foo
        // 
        //         indented code
        // ```
        // 
        // where the code is indented eight spaces.  The spec above, by contrast, will
        // parse this text as expected, since the code block's indentation is measured
        // from the beginning of `foo`.
        // 
        // The one case that needs special treatment is a list item that *starts*
        // with indented code.  How much indentation is required in that case, since
        // we don't have a "first paragraph" to measure from?  Rule #2 simply stipulates
        // that in such cases, we require one space indentation from the list marker
        // (and then the normal four spaces for the indented code).  This will match the
        // four-space rule in cases where the list marker plus its initial indentation
        // takes four spaces (a common case), but diverge in other cases.
        // 
        // ## Lists
        // 
        // A [list](@) is a sequence of one or more
        // list items [of the same type].  The list items
        // may be separated by single [blank lines], but two
        // blank lines end all containing lists.
        // 
        // Two list items are [of the same type](@)
        // if they begin with a [list marker] of the same type.
        // Two list markers are of the
        // same type if (a) they are bullet list markers using the same character
        // (`-`, `+`, or `*`) or (b) they are ordered list numbers with the same
        // delimiter (either `.` or `)`).
        // 
        // A list is an [ordered list](@)
        // if its constituent list items begin with
        // [ordered list markers], and a
        // [bullet list](@) if its constituent list
        // items begin with [bullet list markers].
        // 
        // The [start number](@)
        // of an [ordered list] is determined by the list number of
        // its initial list item.  The numbers of subsequent list items are
        // disregarded.
        // 
        // A list is [loose](@) if any of its constituent
        // list items are separated by blank lines, or if any of its constituent
        // list items directly contain two block-level elements with a blank line
        // between them.  Otherwise a list is [tight](@).
        // (The difference in HTML output is that paragraphs in a loose list are
        // wrapped in `<p>` tags, while paragraphs in a tight list are not.)
        // 
        // Changing the bullet or ordered list delimiter starts a new list:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example260()
        {
            // Source:
            //     - foo
            //     - bar
            //     + baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ul>
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            
            ExecuteExampleTest(260, "Container blocks - Lists",
                "- foo\r\n- bar\r\n+ baz",
                "<ul>\r\n<li>foo</li>\r\n<li>bar</li>\r\n</ul>\r\n<ul>\r\n<li>baz</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example261()
        {
            // Source:
            //     1. foo
            //     2. bar
            //     3) baz
            // 
            // Expected result:
            //     <ol>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ol>
            //     <ol start="3">
            //     <li>baz</li>
            //     </ol>
            
            ExecuteExampleTest(261, "Container blocks - Lists",
                "1. foo\r\n2. bar\r\n3) baz",
                "<ol>\r\n<li>foo</li>\r\n<li>bar</li>\r\n</ol>\r\n<ol start=\"3\">\r\n<li>baz</li>\r\n</ol>");
        }
        // 
        // 
        // In CommonMark, a list can interrupt a paragraph. That is,
        // no blank line is needed to separate a paragraph from a following
        // list:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example262()
        {
            // Source:
            //     Foo
            //     - bar
            //     - baz
            // 
            // Expected result:
            //     <p>Foo</p>
            //     <ul>
            //     <li>bar</li>
            //     <li>baz</li>
            //     </ul>
            
            ExecuteExampleTest(262, "Container blocks - Lists",
                "Foo\r\n- bar\r\n- baz",
                "<p>Foo</p>\r\n<ul>\r\n<li>bar</li>\r\n<li>baz</li>\r\n</ul>");
        }
        // 
        // 
        // `Markdown.pl` does not allow this, through fear of triggering a list
        // via a numeral in a hard-wrapped line:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example263()
        {
            // Source:
            //     The number of windows in my house is
            //     14.  The number of doors is 6.
            // 
            // Expected result:
            //     <p>The number of windows in my house is</p>
            //     <ol start="14">
            //     <li>The number of doors is 6.</li>
            //     </ol>
            
            ExecuteExampleTest(263, "Container blocks - Lists",
                "The number of windows in my house is\r\n14.  The number of doors is 6.",
                "<p>The number of windows in my house is</p>\r\n<ol start=\"14\">\r\n<li>The number of doors is 6.</li>\r\n</ol>");
        }
        // 
        // 
        // Oddly, `Markdown.pl` *does* allow a blockquote to interrupt a paragraph,
        // even though the same considerations might apply.  We think that the two
        // cases should be treated the same.  Here are two reasons for allowing
        // lists to interrupt paragraphs:
        // 
        // First, it is natural and not uncommon for people to start lists without
        // blank lines:
        // 
        //     I need to buy
        //     - new shoes
        //     - a coat
        //     - a plane ticket
        // 
        // Second, we are attracted to a
        // 
        // > [principle of uniformity](@):
        // > if a chunk of text has a certain
        // > meaning, it will continue to have the same meaning when put into a
        // > container block (such as a list item or blockquote).
        // 
        // (Indeed, the spec for [list items] and [block quotes] presupposes
        // this principle.) This principle implies that if
        // 
        //       * I need to buy
        //         - new shoes
        //         - a coat
        //         - a plane ticket
        // 
        // is a list item containing a paragraph followed by a nested sublist,
        // as all Markdown implementations agree it is (though the paragraph
        // may be rendered without `<p>` tags, since the list is "tight"),
        // then
        // 
        //     I need to buy
        //     - new shoes
        //     - a coat
        //     - a plane ticket
        // 
        // by itself should be a paragraph followed by a nested sublist.
        // 
        // Our adherence to the [principle of uniformity]
        // thus inclines us to think that there are two coherent packages:
        // 
        // 1.  Require blank lines before *all* lists and blockquotes,
        //     including lists that occur as sublists inside other list items.
        // 
        // 2.  Require blank lines in none of these places.
        // 
        // [reStructuredText](http://docutils.sourceforge.net/rst.html) takes
        // the first approach, for which there is much to be said.  But the second
        // seems more consistent with established practice with Markdown.
        // 
        // There can be blank lines between items, but two blank lines end
        // a list:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example264()
        {
            // Source:
            //     - foo
            //     
            //     - bar
            //     
            //     
            //     - baz
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     </li>
            //     <li>
            //     <p>bar</p>
            //     </li>
            //     </ul>
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            
            ExecuteExampleTest(264, "Container blocks - Lists",
                "- foo\r\n\r\n- bar\r\n\r\n\r\n- baz",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n</li>\r\n<li>\r\n<p>bar</p>\r\n</li>\r\n</ul>\r\n<ul>\r\n<li>baz</li>\r\n</ul>");
        }
        // 
        // 
        // As illustrated above in the section on [list items],
        // two blank lines between blocks *within* a list item will also end a
        // list:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example265()
        {
            // Source:
            //     - foo
            //     
            //     
            //       bar
            //     - baz
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     </ul>
            //     <p>bar</p>
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            
            ExecuteExampleTest(265, "Container blocks - Lists",
                "- foo\r\n\r\n\r\n  bar\r\n- baz",
                "<ul>\r\n<li>foo</li>\r\n</ul>\r\n<p>bar</p>\r\n<ul>\r\n<li>baz</li>\r\n</ul>");
        }
        // 
        // 
        // Indeed, two blank lines will end *all* containing lists:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example266()
        {
            // Source:
            //     - foo
            //       - bar
            //         - baz
            //     
            //     
            //           bim
            // 
            // Expected result:
            //     <ul>
            //     <li>foo
            //     <ul>
            //     <li>bar
            //     <ul>
            //     <li>baz</li>
            //     </ul>
            //     </li>
            //     </ul>
            //     </li>
            //     </ul>
            //     <pre><code>  bim
            //     </code></pre>
            
            ExecuteExampleTest(266, "Container blocks - Lists",
                "- foo\r\n  - bar\r\n    - baz\r\n\r\n\r\n      bim",
                "<ul>\r\n<li>foo\r\n<ul>\r\n<li>bar\r\n<ul>\r\n<li>baz</li>\r\n</ul>\r\n</li>\r\n</ul>\r\n</li>\r\n</ul>\r\n<pre><code>  bim\r\n</code></pre>");
        }
        // 
        // 
        // Thus, two blank lines can be used to separate consecutive lists of
        // the same type, or to separate a list from an indented code block
        // that would otherwise be parsed as a subparagraph of the final list
        // item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example267()
        {
            // Source:
            //     - foo
            //     - bar
            //     
            //     
            //     - baz
            //     - bim
            // 
            // Expected result:
            //     <ul>
            //     <li>foo</li>
            //     <li>bar</li>
            //     </ul>
            //     <ul>
            //     <li>baz</li>
            //     <li>bim</li>
            //     </ul>
            
            ExecuteExampleTest(267, "Container blocks - Lists",
                "- foo\r\n- bar\r\n\r\n\r\n- baz\r\n- bim",
                "<ul>\r\n<li>foo</li>\r\n<li>bar</li>\r\n</ul>\r\n<ul>\r\n<li>baz</li>\r\n<li>bim</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example268()
        {
            // Source:
            //     -   foo
            //     
            //         notcode
            //     
            //     -   foo
            //     
            //     
            //         code
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <p>notcode</p>
            //     </li>
            //     <li>
            //     <p>foo</p>
            //     </li>
            //     </ul>
            //     <pre><code>code
            //     </code></pre>
            
            ExecuteExampleTest(268, "Container blocks - Lists",
                "-   foo\r\n\r\n    notcode\r\n\r\n-   foo\r\n\r\n\r\n    code",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<p>notcode</p>\r\n</li>\r\n<li>\r\n<p>foo</p>\r\n</li>\r\n</ul>\r\n<pre><code>code\r\n</code></pre>");
        }
        // 
        // 
        // List items need not be indented to the same level.  The following
        // list items will be treated as items at the same list level,
        // since none is indented enough to belong to the previous list
        // item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example269()
        {
            // Source:
            //     - a
            //      - b
            //       - c
            //        - d
            //         - e
            //        - f
            //       - g
            //      - h
            //     - i
            // 
            // Expected result:
            //     <ul>
            //     <li>a</li>
            //     <li>b</li>
            //     <li>c</li>
            //     <li>d</li>
            //     <li>e</li>
            //     <li>f</li>
            //     <li>g</li>
            //     <li>h</li>
            //     <li>i</li>
            //     </ul>
            
            ExecuteExampleTest(269, "Container blocks - Lists",
                "- a\r\n - b\r\n  - c\r\n   - d\r\n    - e\r\n   - f\r\n  - g\r\n - h\r\n- i",
                "<ul>\r\n<li>a</li>\r\n<li>b</li>\r\n<li>c</li>\r\n<li>d</li>\r\n<li>e</li>\r\n<li>f</li>\r\n<li>g</li>\r\n<li>h</li>\r\n<li>i</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example270()
        {
            // Source:
            //     1. a
            //     
            //       2. b
            //     
            //         3. c
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(270, "Container blocks - Lists",
                "1. a\r\n\r\n  2. b\r\n\r\n    3. c",
                "<ol>\r\n<li>\r\n<p>a</p>\r\n</li>\r\n<li>\r\n<p>b</p>\r\n</li>\r\n<li>\r\n<p>c</p>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // This is a loose list, because there is a blank line between
        // two of the list items:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example271()
        {
            // Source:
            //     - a
            //     - b
            //     
            //     - c
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(271, "Container blocks - Lists",
                "- a\r\n- b\r\n\r\n- c",
                "<ul>\r\n<li>\r\n<p>a</p>\r\n</li>\r\n<li>\r\n<p>b</p>\r\n</li>\r\n<li>\r\n<p>c</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // So is this, with a empty second item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example272()
        {
            // Source:
            //     * a
            //     *
            //     
            //     * c
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li></li>
            //     <li>
            //     <p>c</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(272, "Container blocks - Lists",
                "* a\r\n*\r\n\r\n* c",
                "<ul>\r\n<li>\r\n<p>a</p>\r\n</li>\r\n<li></li>\r\n<li>\r\n<p>c</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // These are loose lists, even though there is no space between the items,
        // because one of the items directly contains two block-level elements
        // with a blank line between them:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example273()
        {
            // Source:
            //     - a
            //     - b
            //     
            //       c
            //     - d
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     <p>c</p>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(273, "Container blocks - Lists",
                "- a\r\n- b\r\n\r\n  c\r\n- d",
                "<ul>\r\n<li>\r\n<p>a</p>\r\n</li>\r\n<li>\r\n<p>b</p>\r\n<p>c</p>\r\n</li>\r\n<li>\r\n<p>d</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example274()
        {
            // Source:
            //     - a
            //     - b
            //     
            //       [ref]: /url
            //     - d
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     </li>
            //     <li>
            //     <p>b</p>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(274, "Container blocks - Lists",
                "- a\r\n- b\r\n\r\n  [ref]: /url\r\n- d",
                "<ul>\r\n<li>\r\n<p>a</p>\r\n</li>\r\n<li>\r\n<p>b</p>\r\n</li>\r\n<li>\r\n<p>d</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // This is a tight list, because the blank lines are in a code block:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example275()
        {
            // Source:
            //     - a
            //     - ```
            //       b
            //     
            //     
            //       ```
            //     - c
            // 
            // Expected result:
            //     <ul>
            //     <li>a</li>
            //     <li>
            //     <pre><code>b
            //     
            //     
            //     </code></pre>
            //     </li>
            //     <li>c</li>
            //     </ul>
            
            ExecuteExampleTest(275, "Container blocks - Lists",
                "- a\r\n- ```\r\n  b\r\n\r\n\r\n  ```\r\n- c",
                "<ul>\r\n<li>a</li>\r\n<li>\r\n<pre><code>b\r\n\r\n\r\n</code></pre>\r\n</li>\r\n<li>c</li>\r\n</ul>");
        }
        // 
        // 
        // This is a tight list, because the blank line is between two
        // paragraphs of a sublist.  So the sublist is loose while
        // the outer list is tight:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example276()
        {
            // Source:
            //     - a
            //       - b
            //     
            //         c
            //     - d
            // 
            // Expected result:
            //     <ul>
            //     <li>a
            //     <ul>
            //     <li>
            //     <p>b</p>
            //     <p>c</p>
            //     </li>
            //     </ul>
            //     </li>
            //     <li>d</li>
            //     </ul>
            
            ExecuteExampleTest(276, "Container blocks - Lists",
                "- a\r\n  - b\r\n\r\n    c\r\n- d",
                "<ul>\r\n<li>a\r\n<ul>\r\n<li>\r\n<p>b</p>\r\n<p>c</p>\r\n</li>\r\n</ul>\r\n</li>\r\n<li>d</li>\r\n</ul>");
        }
        // 
        // 
        // This is a tight list, because the blank line is inside the
        // block quote:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example277()
        {
            // Source:
            //     * a
            //       > b
            //       >
            //     * c
            // 
            // Expected result:
            //     <ul>
            //     <li>a
            //     <blockquote>
            //     <p>b</p>
            //     </blockquote>
            //     </li>
            //     <li>c</li>
            //     </ul>
            
            ExecuteExampleTest(277, "Container blocks - Lists",
                "* a\r\n  > b\r\n  >\r\n* c",
                "<ul>\r\n<li>a\r\n<blockquote>\r\n<p>b</p>\r\n</blockquote>\r\n</li>\r\n<li>c</li>\r\n</ul>");
        }
        // 
        // 
        // This list is tight, because the consecutive block elements
        // are not separated by blank lines:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example278()
        {
            // Source:
            //     - a
            //       > b
            //       ```
            //       c
            //       ```
            //     - d
            // 
            // Expected result:
            //     <ul>
            //     <li>a
            //     <blockquote>
            //     <p>b</p>
            //     </blockquote>
            //     <pre><code>c
            //     </code></pre>
            //     </li>
            //     <li>d</li>
            //     </ul>
            
            ExecuteExampleTest(278, "Container blocks - Lists",
                "- a\r\n  > b\r\n  ```\r\n  c\r\n  ```\r\n- d",
                "<ul>\r\n<li>a\r\n<blockquote>\r\n<p>b</p>\r\n</blockquote>\r\n<pre><code>c\r\n</code></pre>\r\n</li>\r\n<li>d</li>\r\n</ul>");
        }
        // 
        // 
        // A single-paragraph list is tight:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example279()
        {
            // Source:
            //     - a
            // 
            // Expected result:
            //     <ul>
            //     <li>a</li>
            //     </ul>
            
            ExecuteExampleTest(279, "Container blocks - Lists",
                "- a",
                "<ul>\r\n<li>a</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example280()
        {
            // Source:
            //     - a
            //       - b
            // 
            // Expected result:
            //     <ul>
            //     <li>a
            //     <ul>
            //     <li>b</li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(280, "Container blocks - Lists",
                "- a\r\n  - b",
                "<ul>\r\n<li>a\r\n<ul>\r\n<li>b</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // This list is loose, because of the blank line between the
        // two block elements in the list item:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example281()
        {
            // Source:
            //     1. ```
            //        foo
            //        ```
            //     
            //        bar
            // 
            // Expected result:
            //     <ol>
            //     <li>
            //     <pre><code>foo
            //     </code></pre>
            //     <p>bar</p>
            //     </li>
            //     </ol>
            
            ExecuteExampleTest(281, "Container blocks - Lists",
                "1. ```\r\n   foo\r\n   ```\r\n\r\n   bar",
                "<ol>\r\n<li>\r\n<pre><code>foo\r\n</code></pre>\r\n<p>bar</p>\r\n</li>\r\n</ol>");
        }
        // 
        // 
        // Here the outer list is loose, the inner list tight:
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example282()
        {
            // Source:
            //     * foo
            //       * bar
            //     
            //       baz
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>foo</p>
            //     <ul>
            //     <li>bar</li>
            //     </ul>
            //     <p>baz</p>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(282, "Container blocks - Lists",
                "* foo\r\n  * bar\r\n\r\n  baz",
                "<ul>\r\n<li>\r\n<p>foo</p>\r\n<ul>\r\n<li>bar</li>\r\n</ul>\r\n<p>baz</p>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Container blocks - Lists")]
        public void Example283()
        {
            // Source:
            //     - a
            //       - b
            //       - c
            //     
            //     - d
            //       - e
            //       - f
            // 
            // Expected result:
            //     <ul>
            //     <li>
            //     <p>a</p>
            //     <ul>
            //     <li>b</li>
            //     <li>c</li>
            //     </ul>
            //     </li>
            //     <li>
            //     <p>d</p>
            //     <ul>
            //     <li>e</li>
            //     <li>f</li>
            //     </ul>
            //     </li>
            //     </ul>
            
            ExecuteExampleTest(283, "Container blocks - Lists",
                "- a\r\n  - b\r\n  - c\r\n\r\n- d\r\n  - e\r\n  - f",
                "<ul>\r\n<li>\r\n<p>a</p>\r\n<ul>\r\n<li>b</li>\r\n<li>c</li>\r\n</ul>\r\n</li>\r\n<li>\r\n<p>d</p>\r\n<ul>\r\n<li>e</li>\r\n<li>f</li>\r\n</ul>\r\n</li>\r\n</ul>");
        }
        // 
        // 
        // # Inlines
        // 
        // Inlines are parsed sequentially from the beginning of the character
        // stream to the end (left to right, in left-to-right languages).
        // Thus, for example, in
        // 
        [TestMethod]
        [TestCategory("Inlines")]
        public void Example284()
        {
            // Source:
            //     `hi`lo`
            // 
            // Expected result:
            //     <p><code>hi</code>lo`</p>
            
            ExecuteExampleTest(284, "Inlines",
                "`hi`lo`",
                "<p><code>hi</code>lo`</p>");
        }
        // 
        // 
        // `hi` is parsed as code, leaving the backtick at the end as a literal
        // backtick.
        // 
        // ## Backslash escapes
        // 
        // Any ASCII punctuation character may be backslash-escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example285()
        {
            // Source:
            //     \!\"\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~
            // 
            // Expected result:
            //     <p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~</p>
            
            ExecuteExampleTest(285, "Inlines - Backslash escapes",
                "\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~",
                "<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_`{|}~</p>");
        }
        // 
        // 
        // Backslashes before other characters are treated as literal
        // backslashes:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example286()
        {
            // Source:
            //     \→\A\a\ \3\φ\«
            // 
            // Expected result:
            //     <p>\→\A\a\ \3\φ\«</p>
            
            ExecuteExampleTest(286, "Inlines - Backslash escapes",
                "\\\t\\A\\a\\ \\3\\φ\\«",
                "<p>\\\t\\A\\a\\ \\3\\φ\\«</p>");
        }
        // 
        // 
        // Escaped characters are treated as regular characters and do
        // not have their usual Markdown meanings:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example287()
        {
            // Source:
            //     \*not emphasized*
            //     \<br/> not a tag
            //     \[not a link](/foo)
            //     \`not code`
            //     1\. not a list
            //     \* not a list
            //     \# not a heading
            //     \[foo]: /url "not a reference"
            // 
            // Expected result:
            //     <p>*not emphasized*
            //     &lt;br/&gt; not a tag
            //     [not a link](/foo)
            //     `not code`
            //     1. not a list
            //     * not a list
            //     # not a heading
            //     [foo]: /url &quot;not a reference&quot;</p>
            
            ExecuteExampleTest(287, "Inlines - Backslash escapes",
                "\\*not emphasized*\r\n\\<br/> not a tag\r\n\\[not a link](/foo)\r\n\\`not code`\r\n1\\. not a list\r\n\\* not a list\r\n\\# not a heading\r\n\\[foo]: /url \"not a reference\"",
                "<p>*not emphasized*\r\n&lt;br/&gt; not a tag\r\n[not a link](/foo)\r\n`not code`\r\n1. not a list\r\n* not a list\r\n# not a heading\r\n[foo]: /url &quot;not a reference&quot;</p>");
        }
        // 
        // 
        // If a backslash is itself escaped, the following character is not:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example288()
        {
            // Source:
            //     \\*emphasis*
            // 
            // Expected result:
            //     <p>\<em>emphasis</em></p>
            
            ExecuteExampleTest(288, "Inlines - Backslash escapes",
                "\\\\*emphasis*",
                "<p>\\<em>emphasis</em></p>");
        }
        // 
        // 
        // A backslash at the end of the line is a [hard line break]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example289()
        {
            // Source:
            //     foo\
            //     bar
            // 
            // Expected result:
            //     <p>foo<br />
            //     bar</p>
            
            ExecuteExampleTest(289, "Inlines - Backslash escapes",
                "foo\\\r\nbar",
                "<p>foo<br />\r\nbar</p>");
        }
        // 
        // 
        // Backslash escapes do not work in code blocks, code spans, autolinks, or
        // raw HTML:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example290()
        {
            // Source:
            //     `` \[\` ``
            // 
            // Expected result:
            //     <p><code>\[\`</code></p>
            
            ExecuteExampleTest(290, "Inlines - Backslash escapes",
                "`` \\[\\` ``",
                "<p><code>\\[\\`</code></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example291()
        {
            // Source:
            //         \[\]
            // 
            // Expected result:
            //     <pre><code>\[\]
            //     </code></pre>
            
            ExecuteExampleTest(291, "Inlines - Backslash escapes",
                "    \\[\\]",
                "<pre><code>\\[\\]\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example292()
        {
            // Source:
            //     ~~~
            //     \[\]
            //     ~~~
            // 
            // Expected result:
            //     <pre><code>\[\]
            //     </code></pre>
            
            ExecuteExampleTest(292, "Inlines - Backslash escapes",
                "~~~\r\n\\[\\]\r\n~~~",
                "<pre><code>\\[\\]\r\n</code></pre>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example293()
        {
            // Source:
            //     <http://example.com?find=\*>
            // 
            // Expected result:
            //     <p><a href="http://example.com?find=%5C*">http://example.com?find=\*</a></p>
            
            ExecuteExampleTest(293, "Inlines - Backslash escapes",
                "<http://example.com?find=\\*>",
                "<p><a href=\"http://example.com?find=%5C*\">http://example.com?find=\\*</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example294()
        {
            // Source:
            //     <a href="/bar\/)">
            // 
            // Expected result:
            //     <a href="/bar\/)">
            
            ExecuteExampleTest(294, "Inlines - Backslash escapes",
                "<a href=\"/bar\\/)\">",
                "<a href=\"/bar\\/)\">");
        }
        // 
        // 
        // But they work in all other contexts, including URLs and link titles,
        // link references, and [info strings] in [fenced code blocks]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example295()
        {
            // Source:
            //     [foo](/bar\* "ti\*tle")
            // 
            // Expected result:
            //     <p><a href="/bar*" title="ti*tle">foo</a></p>
            
            ExecuteExampleTest(295, "Inlines - Backslash escapes",
                "[foo](/bar\\* \"ti\\*tle\")",
                "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example296()
        {
            // Source:
            //     [foo]
            //     
            //     [foo]: /bar\* "ti\*tle"
            // 
            // Expected result:
            //     <p><a href="/bar*" title="ti*tle">foo</a></p>
            
            ExecuteExampleTest(296, "Inlines - Backslash escapes",
                "[foo]\r\n\r\n[foo]: /bar\\* \"ti\\*tle\"",
                "<p><a href=\"/bar*\" title=\"ti*tle\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Backslash escapes")]
        public void Example297()
        {
            // Source:
            //     ``` foo\+bar
            //     foo
            //     ```
            // 
            // Expected result:
            //     <pre><code class="language-foo+bar">foo
            //     </code></pre>
            
            ExecuteExampleTest(297, "Inlines - Backslash escapes",
                "``` foo\\+bar\r\nfoo\r\n```",
                "<pre><code class=\"language-foo+bar\">foo\r\n</code></pre>");
        }
        // 
        // 
        // 
        // ## Entity and numeric character references
        // 
        // All valid HTML entity references and numeric character
        // references, except those occuring in code blocks and code spans,
        // are recognized as such and treated as equivalent to the
        // corresponding Unicode characters.  Conforming CommonMark parsers
        // need not store information about whether a particular character
        // was represented in the source using a Unicode character or
        // an entity reference.
        // 
        // [Entity references](@) consist of `&` + any of the valid
        // HTML5 entity names + `;`. The
        // document <https://html.spec.whatwg.org/multipage/entities.json>
        // is used as an authoritative source for the valid entity
        // references and their corresponding code points.
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example298()
        {
            // Source:
            //     &nbsp; &amp; &copy; &AElig; &Dcaron;
            //     &frac34; &HilbertSpace; &DifferentialD;
            //     &ClockwiseContourIntegral; &ngE;
            // 
            // Expected result:
            //     <p>  &amp; © Æ Ď
            //     ¾ ℋ ⅆ
            //     ∲ ≧̸</p>
            
            ExecuteExampleTest(298, "Inlines - Entity and numeric character references",
                "&nbsp; &amp; &copy; &AElig; &Dcaron;\r\n&frac34; &HilbertSpace; &DifferentialD;\r\n&ClockwiseContourIntegral; &ngE;",
                "<p>  &amp; © Æ Ď\r\n¾ ℋ ⅆ\r\n∲ ≧̸</p>");
        }
        // 
        // 
        // [Decimal numeric character
        // references](@)
        // consist of `&#` + a string of 1--8 arabic digits + `;`. A
        // numeric character reference is parsed as the corresponding
        // Unicode character. Invalid Unicode code points will be replaced by
        // the REPLACEMENT CHARACTER (`U+FFFD`).  For security reasons,
        // the code point `U+0000` will also be replaced by `U+FFFD`.
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example299()
        {
            // Source:
            //     &#35; &#1234; &#992; &#98765432; &#0;
            // 
            // Expected result:
            //     <p># Ӓ Ϡ � �</p>
            
            ExecuteExampleTest(299, "Inlines - Entity and numeric character references",
                "&#35; &#1234; &#992; &#98765432; &#0;",
                "<p># Ӓ Ϡ � �</p>");
        }
        // 
        // 
        // [Hexadecimal numeric character
        // references](@) consist of `&#` +
        // either `X` or `x` + a string of 1-8 hexadecimal digits + `;`.
        // They too are parsed as the corresponding Unicode character (this
        // time specified with a hexadecimal numeral instead of decimal).
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example300()
        {
            // Source:
            //     &#X22; &#XD06; &#xcab;
            // 
            // Expected result:
            //     <p>&quot; ആ ಫ</p>
            
            ExecuteExampleTest(300, "Inlines - Entity and numeric character references",
                "&#X22; &#XD06; &#xcab;",
                "<p>&quot; ആ ಫ</p>");
        }
        // 
        // 
        // Here are some nonentities:
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example301()
        {
            // Source:
            //     &nbsp &x; &#; &#x;
            //     &ThisIsNotDefined; &hi?;
            // 
            // Expected result:
            //     <p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
            //     &amp;ThisIsNotDefined; &amp;hi?;</p>
            
            ExecuteExampleTest(301, "Inlines - Entity and numeric character references",
                "&nbsp &x; &#; &#x;\r\n&ThisIsNotDefined; &hi?;",
                "<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;\r\n&amp;ThisIsNotDefined; &amp;hi?;</p>");
        }
        // 
        // 
        // Although HTML5 does accept some entity references
        // without a trailing semicolon (such as `&copy`), these are not
        // recognized here, because it makes the grammar too ambiguous:
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example302()
        {
            // Source:
            //     &copy
            // 
            // Expected result:
            //     <p>&amp;copy</p>
            
            ExecuteExampleTest(302, "Inlines - Entity and numeric character references",
                "&copy",
                "<p>&amp;copy</p>");
        }
        // 
        // 
        // Strings that are not on the list of HTML5 named entities are not
        // recognized as entity references either:
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example303()
        {
            // Source:
            //     &MadeUpEntity;
            // 
            // Expected result:
            //     <p>&amp;MadeUpEntity;</p>
            
            ExecuteExampleTest(303, "Inlines - Entity and numeric character references",
                "&MadeUpEntity;",
                "<p>&amp;MadeUpEntity;</p>");
        }
        // 
        // 
        // Entity and numeric character references are recognized in any
        // context besides code spans or code blocks, including
        // URLs, [link titles], and [fenced code block][] [info strings]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example304()
        {
            // Source:
            //     <a href="&ouml;&ouml;.html">
            // 
            // Expected result:
            //     <a href="&ouml;&ouml;.html">
            
            ExecuteExampleTest(304, "Inlines - Entity and numeric character references",
                "<a href=\"&ouml;&ouml;.html\">",
                "<a href=\"&ouml;&ouml;.html\">");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example305()
        {
            // Source:
            //     [foo](/f&ouml;&ouml; "f&ouml;&ouml;")
            // 
            // Expected result:
            //     <p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
            
            ExecuteExampleTest(305, "Inlines - Entity and numeric character references",
                "[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")",
                "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example306()
        {
            // Source:
            //     [foo]
            //     
            //     [foo]: /f&ouml;&ouml; "f&ouml;&ouml;"
            // 
            // Expected result:
            //     <p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
            
            ExecuteExampleTest(306, "Inlines - Entity and numeric character references",
                "[foo]\r\n\r\n[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"",
                "<p><a href=\"/f%C3%B6%C3%B6\" title=\"föö\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example307()
        {
            // Source:
            //     ``` f&ouml;&ouml;
            //     foo
            //     ```
            // 
            // Expected result:
            //     <pre><code class="language-föö">foo
            //     </code></pre>
            
            ExecuteExampleTest(307, "Inlines - Entity and numeric character references",
                "``` f&ouml;&ouml;\r\nfoo\r\n```",
                "<pre><code class=\"language-föö\">foo\r\n</code></pre>");
        }
        // 
        // 
        // Entity and numeric character references are treated as literal
        // text in code spans and code blocks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example308()
        {
            // Source:
            //     `f&ouml;&ouml;`
            // 
            // Expected result:
            //     <p><code>f&amp;ouml;&amp;ouml;</code></p>
            
            ExecuteExampleTest(308, "Inlines - Entity and numeric character references",
                "`f&ouml;&ouml;`",
                "<p><code>f&amp;ouml;&amp;ouml;</code></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Entity and numeric character references")]
        public void Example309()
        {
            // Source:
            //         f&ouml;f&ouml;
            // 
            // Expected result:
            //     <pre><code>f&amp;ouml;f&amp;ouml;
            //     </code></pre>
            
            ExecuteExampleTest(309, "Inlines - Entity and numeric character references",
                "    f&ouml;f&ouml;",
                "<pre><code>f&amp;ouml;f&amp;ouml;\r\n</code></pre>");
        }
        // 
        // 
        // ## Code spans
        // 
        // A [backtick string](@)
        // is a string of one or more backtick characters (`` ` ``) that is neither
        // preceded nor followed by a backtick.
        // 
        // A [code span](@) begins with a backtick string and ends with
        // a backtick string of equal length.  The contents of the code span are
        // the characters between the two backtick strings, with leading and
        // trailing spaces and [line endings] removed, and
        // [whitespace] collapsed to single spaces.
        // 
        // This is a simple code span:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example310()
        {
            // Source:
            //     `foo`
            // 
            // Expected result:
            //     <p><code>foo</code></p>
            
            ExecuteExampleTest(310, "Inlines - Code spans",
                "`foo`",
                "<p><code>foo</code></p>");
        }
        // 
        // 
        // Here two backticks are used, because the code contains a backtick.
        // This example also illustrates stripping of leading and trailing spaces:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example311()
        {
            // Source:
            //     `` foo ` bar  ``
            // 
            // Expected result:
            //     <p><code>foo ` bar</code></p>
            
            ExecuteExampleTest(311, "Inlines - Code spans",
                "`` foo ` bar  ``",
                "<p><code>foo ` bar</code></p>");
        }
        // 
        // 
        // This example shows the motivation for stripping leading and trailing
        // spaces:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example312()
        {
            // Source:
            //     ` `` `
            // 
            // Expected result:
            //     <p><code>``</code></p>
            
            ExecuteExampleTest(312, "Inlines - Code spans",
                "` `` `",
                "<p><code>``</code></p>");
        }
        // 
        // 
        // [Line endings] are treated like spaces:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example313()
        {
            // Source:
            //     ``
            //     foo
            //     ``
            // 
            // Expected result:
            //     <p><code>foo</code></p>
            
            ExecuteExampleTest(313, "Inlines - Code spans",
                "``\r\nfoo\r\n``",
                "<p><code>foo</code></p>");
        }
        // 
        // 
        // Interior spaces and [line endings] are collapsed into
        // single spaces, just as they would be by a browser:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example314()
        {
            // Source:
            //     `foo   bar
            //       baz`
            // 
            // Expected result:
            //     <p><code>foo bar baz</code></p>
            
            ExecuteExampleTest(314, "Inlines - Code spans",
                "`foo   bar\r\n  baz`",
                "<p><code>foo bar baz</code></p>");
        }
        // 
        // 
        // Q: Why not just leave the spaces, since browsers will collapse them
        // anyway?  A:  Because we might be targeting a non-HTML format, and we
        // shouldn't rely on HTML-specific rendering assumptions.
        // 
        // (Existing implementations differ in their treatment of internal
        // spaces and [line endings].  Some, including `Markdown.pl` and
        // `showdown`, convert an internal [line ending] into a
        // `<br />` tag.  But this makes things difficult for those who like to
        // hard-wrap their paragraphs, since a line break in the midst of a code
        // span will cause an unintended line break in the output.  Others just
        // leave internal spaces as they are, which is fine if only HTML is being
        // targeted.)
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example315()
        {
            // Source:
            //     `foo `` bar`
            // 
            // Expected result:
            //     <p><code>foo `` bar</code></p>
            
            ExecuteExampleTest(315, "Inlines - Code spans",
                "`foo `` bar`",
                "<p><code>foo `` bar</code></p>");
        }
        // 
        // 
        // Note that backslash escapes do not work in code spans. All backslashes
        // are treated literally:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example316()
        {
            // Source:
            //     `foo\`bar`
            // 
            // Expected result:
            //     <p><code>foo\</code>bar`</p>
            
            ExecuteExampleTest(316, "Inlines - Code spans",
                "`foo\\`bar`",
                "<p><code>foo\\</code>bar`</p>");
        }
        // 
        // 
        // Backslash escapes are never needed, because one can always choose a
        // string of *n* backtick characters as delimiters, where the code does
        // not contain any strings of exactly *n* backtick characters.
        // 
        // Code span backticks have higher precedence than any other inline
        // constructs except HTML tags and autolinks.  Thus, for example, this is
        // not parsed as emphasized text, since the second `*` is part of a code
        // span:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example317()
        {
            // Source:
            //     *foo`*`
            // 
            // Expected result:
            //     <p>*foo<code>*</code></p>
            
            ExecuteExampleTest(317, "Inlines - Code spans",
                "*foo`*`",
                "<p>*foo<code>*</code></p>");
        }
        // 
        // 
        // And this is not parsed as a link:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example318()
        {
            // Source:
            //     [not a `link](/foo`)
            // 
            // Expected result:
            //     <p>[not a <code>link](/foo</code>)</p>
            
            ExecuteExampleTest(318, "Inlines - Code spans",
                "[not a `link](/foo`)",
                "<p>[not a <code>link](/foo</code>)</p>");
        }
        // 
        // 
        // Code spans, HTML tags, and autolinks have the same precedence.
        // Thus, this is code:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example319()
        {
            // Source:
            //     `<a href="`">`
            // 
            // Expected result:
            //     <p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>
            
            ExecuteExampleTest(319, "Inlines - Code spans",
                "`<a href=\"`\">`",
                "<p><code>&lt;a href=&quot;</code>&quot;&gt;`</p>");
        }
        // 
        // 
        // But this is an HTML tag:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example320()
        {
            // Source:
            //     <a href="`">`
            // 
            // Expected result:
            //     <p><a href="`">`</p>
            
            ExecuteExampleTest(320, "Inlines - Code spans",
                "<a href=\"`\">`",
                "<p><a href=\"`\">`</p>");
        }
        // 
        // 
        // And this is code:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example321()
        {
            // Source:
            //     `<http://foo.bar.`baz>`
            // 
            // Expected result:
            //     <p><code>&lt;http://foo.bar.</code>baz&gt;`</p>
            
            ExecuteExampleTest(321, "Inlines - Code spans",
                "`<http://foo.bar.`baz>`",
                "<p><code>&lt;http://foo.bar.</code>baz&gt;`</p>");
        }
        // 
        // 
        // But this is an autolink:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example322()
        {
            // Source:
            //     <http://foo.bar.`baz>`
            // 
            // Expected result:
            //     <p><a href="http://foo.bar.%60baz">http://foo.bar.`baz</a>`</p>
            
            ExecuteExampleTest(322, "Inlines - Code spans",
                "<http://foo.bar.`baz>`",
                "<p><a href=\"http://foo.bar.%60baz\">http://foo.bar.`baz</a>`</p>");
        }
        // 
        // 
        // When a backtick string is not closed by a matching backtick string,
        // we just have literal backticks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example323()
        {
            // Source:
            //     ```foo``
            // 
            // Expected result:
            //     <p>```foo``</p>
            
            ExecuteExampleTest(323, "Inlines - Code spans",
                "```foo``",
                "<p>```foo``</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Code spans")]
        public void Example324()
        {
            // Source:
            //     `foo
            // 
            // Expected result:
            //     <p>`foo</p>
            
            ExecuteExampleTest(324, "Inlines - Code spans",
                "`foo",
                "<p>`foo</p>");
        }
        // 
        // 
        // ## Emphasis and strong emphasis
        // 
        // John Gruber's original [Markdown syntax
        // description](http://daringfireball.net/projects/markdown/syntax#em) says:
        // 
        // > Markdown treats asterisks (`*`) and underscores (`_`) as indicators of
        // > emphasis. Text wrapped with one `*` or `_` will be wrapped with an HTML
        // > `<em>` tag; double `*`'s or `_`'s will be wrapped with an HTML `<strong>`
        // > tag.
        // 
        // This is enough for most users, but these rules leave much undecided,
        // especially when it comes to nested emphasis.  The original
        // `Markdown.pl` test suite makes it clear that triple `***` and
        // `___` delimiters can be used for strong emphasis, and most
        // implementations have also allowed the following patterns:
        // 
        // ``` markdown
        // ***strong emph***
        // ***strong** in emph*
        // ***emph* in strong**
        // **in strong *emph***
        // *in emph **strong***
        // ```
        // 
        // The following patterns are less widely supported, but the intent
        // is clear and they are useful (especially in contexts like bibliography
        // entries):
        // 
        // ``` markdown
        // *emph *with emph* in it*
        // **strong **with strong** in it**
        // ```
        // 
        // Many implementations have also restricted intraword emphasis to
        // the `*` forms, to avoid unwanted emphasis in words containing
        // internal underscores.  (It is best practice to put these in code
        // spans, but users often do not.)
        // 
        // ``` markdown
        // internal emphasis: foo*bar*baz
        // no emphasis: foo_bar_baz
        // ```
        // 
        // The rules given below capture all of these patterns, while allowing
        // for efficient parsing strategies that do not backtrack.
        // 
        // First, some definitions.  A [delimiter run](@) is either
        // a sequence of one or more `*` characters that is not preceded or
        // followed by a `*` character, or a sequence of one or more `_`
        // characters that is not preceded or followed by a `_` character.
        // 
        // A [left-flanking delimiter run](@) is
        // a [delimiter run] that is (a) not followed by [Unicode whitespace],
        // and (b) either not followed by a [punctuation character], or
        // preceded by [Unicode whitespace] or a [punctuation character].
        // For purposes of this definition, the beginning and the end of
        // the line count as Unicode whitespace.
        // 
        // A [right-flanking delimiter run](@) is
        // a [delimiter run] that is (a) not preceded by [Unicode whitespace],
        // and (b) either not preceded by a [punctuation character], or
        // followed by [Unicode whitespace] or a [punctuation character].
        // For purposes of this definition, the beginning and the end of
        // the line count as Unicode whitespace.
        // 
        // Here are some examples of delimiter runs.
        // 
        //   - left-flanking but not right-flanking:
        // 
        //     ```
        //     ***abc
        //       _abc
        //     **"abc"
        //      _"abc"
        //     ```
        // 
        //   - right-flanking but not left-flanking:
        // 
        //     ```
        //      abc***
        //      abc_
        //     "abc"**
        //     "abc"_
        //     ```
        // 
        //   - Both left and right-flanking:
        // 
        //     ```
        //      abc***def
        //     "abc"_"def"
        //     ```
        // 
        //   - Neither left nor right-flanking:
        // 
        //     ```
        //     abc *** def
        //     a _ b
        //     ```
        // 
        // (The idea of distinguishing left-flanking and right-flanking
        // delimiter runs based on the character before and the character
        // after comes from Roopesh Chander's
        // [vfmd](http://www.vfmd.org/vfmd-spec/specification/#procedure-for-identifying-emphasis-tags).
        // vfmd uses the terminology "emphasis indicator string" instead of "delimiter
        // run," and its rules for distinguishing left- and right-flanking runs
        // are a bit more complex than the ones given here.)
        // 
        // The following rules define emphasis and strong emphasis:
        // 
        // 1.  A single `*` character [can open emphasis](@)
        //     iff (if and only if) it is part of a [left-flanking delimiter run].
        // 
        // 2.  A single `_` character [can open emphasis] iff
        //     it is part of a [left-flanking delimiter run]
        //     and either (a) not part of a [right-flanking delimiter run]
        //     or (b) part of a [right-flanking delimiter run]
        //     preceded by punctuation.
        // 
        // 3.  A single `*` character [can close emphasis](@)
        //     iff it is part of a [right-flanking delimiter run].
        // 
        // 4.  A single `_` character [can close emphasis] iff
        //     it is part of a [right-flanking delimiter run]
        //     and either (a) not part of a [left-flanking delimiter run]
        //     or (b) part of a [left-flanking delimiter run]
        //     followed by punctuation.
        // 
        // 5.  A double `**` [can open strong emphasis](@)
        //     iff it is part of a [left-flanking delimiter run].
        // 
        // 6.  A double `__` [can open strong emphasis] iff
        //     it is part of a [left-flanking delimiter run]
        //     and either (a) not part of a [right-flanking delimiter run]
        //     or (b) part of a [right-flanking delimiter run]
        //     preceded by punctuation.
        // 
        // 7.  A double `**` [can close strong emphasis](@)
        //     iff it is part of a [right-flanking delimiter run].
        // 
        // 8.  A double `__` [can close strong emphasis]
        //     it is part of a [right-flanking delimiter run]
        //     and either (a) not part of a [left-flanking delimiter run]
        //     or (b) part of a [left-flanking delimiter run]
        //     followed by punctuation.
        // 
        // 9.  Emphasis begins with a delimiter that [can open emphasis] and ends
        //     with a delimiter that [can close emphasis], and that uses the same
        //     character (`_` or `*`) as the opening delimiter.  There must
        //     be a nonempty sequence of inlines between the open delimiter
        //     and the closing delimiter; these form the contents of the emphasis
        //     inline.
        // 
        // 10. Strong emphasis begins with a delimiter that
        //     [can open strong emphasis] and ends with a delimiter that
        //     [can close strong emphasis], and that uses the same character
        //     (`_` or `*`) as the opening delimiter.
        //     There must be a nonempty sequence of inlines between the open
        //     delimiter and the closing delimiter; these form the contents of
        //     the strong emphasis inline.
        // 
        // 11. A literal `*` character cannot occur at the beginning or end of
        //     `*`-delimited emphasis or `**`-delimited strong emphasis, unless it
        //     is backslash-escaped.
        // 
        // 12. A literal `_` character cannot occur at the beginning or end of
        //     `_`-delimited emphasis or `__`-delimited strong emphasis, unless it
        //     is backslash-escaped.
        // 
        // Where rules 1--12 above are compatible with multiple parsings,
        // the following principles resolve ambiguity:
        // 
        // 13. The number of nestings should be minimized. Thus, for example,
        //     an interpretation `<strong>...</strong>` is always preferred to
        //     `<em><em>...</em></em>`.
        // 
        // 14. An interpretation `<strong><em>...</em></strong>` is always
        //     preferred to `<em><strong>..</strong></em>`.
        // 
        // 15. When two potential emphasis or strong emphasis spans overlap,
        //     so that the second begins before the first ends and ends after
        //     the first ends, the first takes precedence. Thus, for example,
        //     `*foo _bar* baz_` is parsed as `<em>foo _bar</em> baz_` rather
        //     than `*foo <em>bar* baz</em>`.  For the same reason,
        //     `**foo*bar**` is parsed as `<em><em>foo</em>bar</em>*`
        //     rather than `<strong>foo*bar</strong>`.
        // 
        // 16. When there are two potential emphasis or strong emphasis spans
        //     with the same closing delimiter, the shorter one (the one that
        //     opens later) takes precedence. Thus, for example,
        //     `**foo **bar baz**` is parsed as `**foo <strong>bar baz</strong>`
        //     rather than `<strong>foo **bar baz</strong>`.
        // 
        // 17. Inline code spans, links, images, and HTML tags group more tightly
        //     than emphasis.  So, when there is a choice between an interpretation
        //     that contains one of these elements and one that does not, the
        //     former always wins.  Thus, for example, `*[foo*](bar)` is
        //     parsed as `*<a href="bar">foo*</a>` rather than as
        //     `<em>[foo</em>](bar)`.
        // 
        // These rules can be illustrated through a series of examples.
        // 
        // Rule 1:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example325()
        {
            // Source:
            //     *foo bar*
            // 
            // Expected result:
            //     <p><em>foo bar</em></p>
            
            ExecuteExampleTest(325, "Inlines - Emphasis and strong emphasis",
                "*foo bar*",
                "<p><em>foo bar</em></p>");
        }
        // 
        // 
        // This is not emphasis, because the opening `*` is followed by
        // whitespace, and hence not part of a [left-flanking delimiter run]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example326()
        {
            // Source:
            //     a * foo bar*
            // 
            // Expected result:
            //     <p>a * foo bar*</p>
            
            ExecuteExampleTest(326, "Inlines - Emphasis and strong emphasis",
                "a * foo bar*",
                "<p>a * foo bar*</p>");
        }
        // 
        // 
        // This is not emphasis, because the opening `*` is preceded
        // by an alphanumeric and followed by punctuation, and hence
        // not part of a [left-flanking delimiter run]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example327()
        {
            // Source:
            //     a*"foo"*
            // 
            // Expected result:
            //     <p>a*&quot;foo&quot;*</p>
            
            ExecuteExampleTest(327, "Inlines - Emphasis and strong emphasis",
                "a*\"foo\"*",
                "<p>a*&quot;foo&quot;*</p>");
        }
        // 
        // 
        // Unicode nonbreaking spaces count as whitespace, too:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example328()
        {
            // Source:
            //     * a *
            // 
            // Expected result:
            //     <p>* a *</p>
            
            ExecuteExampleTest(328, "Inlines - Emphasis and strong emphasis",
                "* a *",
                "<p>* a *</p>");
        }
        // 
        // 
        // Intraword emphasis with `*` is permitted:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example329()
        {
            // Source:
            //     foo*bar*
            // 
            // Expected result:
            //     <p>foo<em>bar</em></p>
            
            ExecuteExampleTest(329, "Inlines - Emphasis and strong emphasis",
                "foo*bar*",
                "<p>foo<em>bar</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example330()
        {
            // Source:
            //     5*6*78
            // 
            // Expected result:
            //     <p>5<em>6</em>78</p>
            
            ExecuteExampleTest(330, "Inlines - Emphasis and strong emphasis",
                "5*6*78",
                "<p>5<em>6</em>78</p>");
        }
        // 
        // 
        // Rule 2:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example331()
        {
            // Source:
            //     _foo bar_
            // 
            // Expected result:
            //     <p><em>foo bar</em></p>
            
            ExecuteExampleTest(331, "Inlines - Emphasis and strong emphasis",
                "_foo bar_",
                "<p><em>foo bar</em></p>");
        }
        // 
        // 
        // This is not emphasis, because the opening `_` is followed by
        // whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example332()
        {
            // Source:
            //     _ foo bar_
            // 
            // Expected result:
            //     <p>_ foo bar_</p>
            
            ExecuteExampleTest(332, "Inlines - Emphasis and strong emphasis",
                "_ foo bar_",
                "<p>_ foo bar_</p>");
        }
        // 
        // 
        // This is not emphasis, because the opening `_` is preceded
        // by an alphanumeric and followed by punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example333()
        {
            // Source:
            //     a_"foo"_
            // 
            // Expected result:
            //     <p>a_&quot;foo&quot;_</p>
            
            ExecuteExampleTest(333, "Inlines - Emphasis and strong emphasis",
                "a_\"foo\"_",
                "<p>a_&quot;foo&quot;_</p>");
        }
        // 
        // 
        // Emphasis with `_` is not allowed inside words:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example334()
        {
            // Source:
            //     foo_bar_
            // 
            // Expected result:
            //     <p>foo_bar_</p>
            
            ExecuteExampleTest(334, "Inlines - Emphasis and strong emphasis",
                "foo_bar_",
                "<p>foo_bar_</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example335()
        {
            // Source:
            //     5_6_78
            // 
            // Expected result:
            //     <p>5_6_78</p>
            
            ExecuteExampleTest(335, "Inlines - Emphasis and strong emphasis",
                "5_6_78",
                "<p>5_6_78</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example336()
        {
            // Source:
            //     пристаням_стремятся_
            // 
            // Expected result:
            //     <p>пристаням_стремятся_</p>
            
            ExecuteExampleTest(336, "Inlines - Emphasis and strong emphasis",
                "пристаням_стремятся_",
                "<p>пристаням_стремятся_</p>");
        }
        // 
        // 
        // Here `_` does not generate emphasis, because the first delimiter run
        // is right-flanking and the second left-flanking:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example337()
        {
            // Source:
            //     aa_"bb"_cc
            // 
            // Expected result:
            //     <p>aa_&quot;bb&quot;_cc</p>
            
            ExecuteExampleTest(337, "Inlines - Emphasis and strong emphasis",
                "aa_\"bb\"_cc",
                "<p>aa_&quot;bb&quot;_cc</p>");
        }
        // 
        // 
        // This is emphasis, even though the opening delimiter is
        // both left- and right-flanking, because it is preceded by
        // punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example338()
        {
            // Source:
            //     foo-_(bar)_
            // 
            // Expected result:
            //     <p>foo-<em>(bar)</em></p>
            
            ExecuteExampleTest(338, "Inlines - Emphasis and strong emphasis",
                "foo-_(bar)_",
                "<p>foo-<em>(bar)</em></p>");
        }
        // 
        // 
        // Rule 3:
        // 
        // This is not emphasis, because the closing delimiter does
        // not match the opening delimiter:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example339()
        {
            // Source:
            //     _foo*
            // 
            // Expected result:
            //     <p>_foo*</p>
            
            ExecuteExampleTest(339, "Inlines - Emphasis and strong emphasis",
                "_foo*",
                "<p>_foo*</p>");
        }
        // 
        // 
        // This is not emphasis, because the closing `*` is preceded by
        // whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example340()
        {
            // Source:
            //     *foo bar *
            // 
            // Expected result:
            //     <p>*foo bar *</p>
            
            ExecuteExampleTest(340, "Inlines - Emphasis and strong emphasis",
                "*foo bar *",
                "<p>*foo bar *</p>");
        }
        // 
        // 
        // A newline also counts as whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example341()
        {
            // Source:
            //     *foo bar
            //     *
            // 
            // Expected result:
            //     <p>*foo bar</p>
            //     <ul>
            //     <li></li>
            //     </ul>
            
            ExecuteExampleTest(341, "Inlines - Emphasis and strong emphasis",
                "*foo bar\r\n*",
                "<p>*foo bar</p>\r\n<ul>\r\n<li></li>\r\n</ul>");
        }
        // 
        // 
        // This is not emphasis, because the second `*` is
        // preceded by punctuation and followed by an alphanumeric
        // (hence it is not part of a [right-flanking delimiter run]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example342()
        {
            // Source:
            //     *(*foo)
            // 
            // Expected result:
            //     <p>*(*foo)</p>
            
            ExecuteExampleTest(342, "Inlines - Emphasis and strong emphasis",
                "*(*foo)",
                "<p>*(*foo)</p>");
        }
        // 
        // 
        // The point of this restriction is more easily appreciated
        // with this example:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example343()
        {
            // Source:
            //     *(*foo*)*
            // 
            // Expected result:
            //     <p><em>(<em>foo</em>)</em></p>
            
            ExecuteExampleTest(343, "Inlines - Emphasis and strong emphasis",
                "*(*foo*)*",
                "<p><em>(<em>foo</em>)</em></p>");
        }
        // 
        // 
        // Intraword emphasis with `*` is allowed:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example344()
        {
            // Source:
            //     *foo*bar
            // 
            // Expected result:
            //     <p><em>foo</em>bar</p>
            
            ExecuteExampleTest(344, "Inlines - Emphasis and strong emphasis",
                "*foo*bar",
                "<p><em>foo</em>bar</p>");
        }
        // 
        // 
        // 
        // Rule 4:
        // 
        // This is not emphasis, because the closing `_` is preceded by
        // whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example345()
        {
            // Source:
            //     _foo bar _
            // 
            // Expected result:
            //     <p>_foo bar _</p>
            
            ExecuteExampleTest(345, "Inlines - Emphasis and strong emphasis",
                "_foo bar _",
                "<p>_foo bar _</p>");
        }
        // 
        // 
        // This is not emphasis, because the second `_` is
        // preceded by punctuation and followed by an alphanumeric:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example346()
        {
            // Source:
            //     _(_foo)
            // 
            // Expected result:
            //     <p>_(_foo)</p>
            
            ExecuteExampleTest(346, "Inlines - Emphasis and strong emphasis",
                "_(_foo)",
                "<p>_(_foo)</p>");
        }
        // 
        // 
        // This is emphasis within emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example347()
        {
            // Source:
            //     _(_foo_)_
            // 
            // Expected result:
            //     <p><em>(<em>foo</em>)</em></p>
            
            ExecuteExampleTest(347, "Inlines - Emphasis and strong emphasis",
                "_(_foo_)_",
                "<p><em>(<em>foo</em>)</em></p>");
        }
        // 
        // 
        // Intraword emphasis is disallowed for `_`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example348()
        {
            // Source:
            //     _foo_bar
            // 
            // Expected result:
            //     <p>_foo_bar</p>
            
            ExecuteExampleTest(348, "Inlines - Emphasis and strong emphasis",
                "_foo_bar",
                "<p>_foo_bar</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example349()
        {
            // Source:
            //     _пристаням_стремятся
            // 
            // Expected result:
            //     <p>_пристаням_стремятся</p>
            
            ExecuteExampleTest(349, "Inlines - Emphasis and strong emphasis",
                "_пристаням_стремятся",
                "<p>_пристаням_стремятся</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example350()
        {
            // Source:
            //     _foo_bar_baz_
            // 
            // Expected result:
            //     <p><em>foo_bar_baz</em></p>
            
            ExecuteExampleTest(350, "Inlines - Emphasis and strong emphasis",
                "_foo_bar_baz_",
                "<p><em>foo_bar_baz</em></p>");
        }
        // 
        // 
        // This is emphasis, even though the closing delimiter is
        // both left- and right-flanking, because it is followed by
        // punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example351()
        {
            // Source:
            //     _(bar)_.
            // 
            // Expected result:
            //     <p><em>(bar)</em>.</p>
            
            ExecuteExampleTest(351, "Inlines - Emphasis and strong emphasis",
                "_(bar)_.",
                "<p><em>(bar)</em>.</p>");
        }
        // 
        // 
        // Rule 5:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example352()
        {
            // Source:
            //     **foo bar**
            // 
            // Expected result:
            //     <p><strong>foo bar</strong></p>
            
            ExecuteExampleTest(352, "Inlines - Emphasis and strong emphasis",
                "**foo bar**",
                "<p><strong>foo bar</strong></p>");
        }
        // 
        // 
        // This is not strong emphasis, because the opening delimiter is
        // followed by whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example353()
        {
            // Source:
            //     ** foo bar**
            // 
            // Expected result:
            //     <p>** foo bar**</p>
            
            ExecuteExampleTest(353, "Inlines - Emphasis and strong emphasis",
                "** foo bar**",
                "<p>** foo bar**</p>");
        }
        // 
        // 
        // This is not strong emphasis, because the opening `**` is preceded
        // by an alphanumeric and followed by punctuation, and hence
        // not part of a [left-flanking delimiter run]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example354()
        {
            // Source:
            //     a**"foo"**
            // 
            // Expected result:
            //     <p>a**&quot;foo&quot;**</p>
            
            ExecuteExampleTest(354, "Inlines - Emphasis and strong emphasis",
                "a**\"foo\"**",
                "<p>a**&quot;foo&quot;**</p>");
        }
        // 
        // 
        // Intraword strong emphasis with `**` is permitted:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example355()
        {
            // Source:
            //     foo**bar**
            // 
            // Expected result:
            //     <p>foo<strong>bar</strong></p>
            
            ExecuteExampleTest(355, "Inlines - Emphasis and strong emphasis",
                "foo**bar**",
                "<p>foo<strong>bar</strong></p>");
        }
        // 
        // 
        // Rule 6:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example356()
        {
            // Source:
            //     __foo bar__
            // 
            // Expected result:
            //     <p><strong>foo bar</strong></p>
            
            ExecuteExampleTest(356, "Inlines - Emphasis and strong emphasis",
                "__foo bar__",
                "<p><strong>foo bar</strong></p>");
        }
        // 
        // 
        // This is not strong emphasis, because the opening delimiter is
        // followed by whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example357()
        {
            // Source:
            //     __ foo bar__
            // 
            // Expected result:
            //     <p>__ foo bar__</p>
            
            ExecuteExampleTest(357, "Inlines - Emphasis and strong emphasis",
                "__ foo bar__",
                "<p>__ foo bar__</p>");
        }
        // 
        // 
        // A newline counts as whitespace:
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example358()
        {
            // Source:
            //     __
            //     foo bar__
            // 
            // Expected result:
            //     <p>__
            //     foo bar__</p>
            
            ExecuteExampleTest(358, "Inlines - Emphasis and strong emphasis",
                "__\r\nfoo bar__",
                "<p>__\r\nfoo bar__</p>");
        }
        // 
        // 
        // This is not strong emphasis, because the opening `__` is preceded
        // by an alphanumeric and followed by punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example359()
        {
            // Source:
            //     a__"foo"__
            // 
            // Expected result:
            //     <p>a__&quot;foo&quot;__</p>
            
            ExecuteExampleTest(359, "Inlines - Emphasis and strong emphasis",
                "a__\"foo\"__",
                "<p>a__&quot;foo&quot;__</p>");
        }
        // 
        // 
        // Intraword strong emphasis is forbidden with `__`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example360()
        {
            // Source:
            //     foo__bar__
            // 
            // Expected result:
            //     <p>foo__bar__</p>
            
            ExecuteExampleTest(360, "Inlines - Emphasis and strong emphasis",
                "foo__bar__",
                "<p>foo__bar__</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example361()
        {
            // Source:
            //     5__6__78
            // 
            // Expected result:
            //     <p>5__6__78</p>
            
            ExecuteExampleTest(361, "Inlines - Emphasis and strong emphasis",
                "5__6__78",
                "<p>5__6__78</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example362()
        {
            // Source:
            //     пристаням__стремятся__
            // 
            // Expected result:
            //     <p>пристаням__стремятся__</p>
            
            ExecuteExampleTest(362, "Inlines - Emphasis and strong emphasis",
                "пристаням__стремятся__",
                "<p>пристаням__стремятся__</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example363()
        {
            // Source:
            //     __foo, __bar__, baz__
            // 
            // Expected result:
            //     <p><strong>foo, <strong>bar</strong>, baz</strong></p>
            
            ExecuteExampleTest(363, "Inlines - Emphasis and strong emphasis",
                "__foo, __bar__, baz__",
                "<p><strong>foo, <strong>bar</strong>, baz</strong></p>");
        }
        // 
        // 
        // This is strong emphasis, even though the opening delimiter is
        // both left- and right-flanking, because it is preceded by
        // punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example364()
        {
            // Source:
            //     foo-__(bar)__
            // 
            // Expected result:
            //     <p>foo-<strong>(bar)</strong></p>
            
            ExecuteExampleTest(364, "Inlines - Emphasis and strong emphasis",
                "foo-__(bar)__",
                "<p>foo-<strong>(bar)</strong></p>");
        }
        // 
        // 
        // 
        // Rule 7:
        // 
        // This is not strong emphasis, because the closing delimiter is preceded
        // by whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example365()
        {
            // Source:
            //     **foo bar **
            // 
            // Expected result:
            //     <p>**foo bar **</p>
            
            ExecuteExampleTest(365, "Inlines - Emphasis and strong emphasis",
                "**foo bar **",
                "<p>**foo bar **</p>");
        }
        // 
        // 
        // (Nor can it be interpreted as an emphasized `*foo bar *`, because of
        // Rule 11.)
        // 
        // This is not strong emphasis, because the second `**` is
        // preceded by punctuation and followed by an alphanumeric:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example366()
        {
            // Source:
            //     **(**foo)
            // 
            // Expected result:
            //     <p>**(**foo)</p>
            
            ExecuteExampleTest(366, "Inlines - Emphasis and strong emphasis",
                "**(**foo)",
                "<p>**(**foo)</p>");
        }
        // 
        // 
        // The point of this restriction is more easily appreciated
        // with these examples:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example367()
        {
            // Source:
            //     *(**foo**)*
            // 
            // Expected result:
            //     <p><em>(<strong>foo</strong>)</em></p>
            
            ExecuteExampleTest(367, "Inlines - Emphasis and strong emphasis",
                "*(**foo**)*",
                "<p><em>(<strong>foo</strong>)</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example368()
        {
            // Source:
            //     **Gomphocarpus (*Gomphocarpus physocarpus*, syn.
            //     *Asclepias physocarpa*)**
            // 
            // Expected result:
            //     <p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
            //     <em>Asclepias physocarpa</em>)</strong></p>
            
            ExecuteExampleTest(368, "Inlines - Emphasis and strong emphasis",
                "**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\r\n*Asclepias physocarpa*)**",
                "<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.\r\n<em>Asclepias physocarpa</em>)</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example369()
        {
            // Source:
            //     **foo "*bar*" foo**
            // 
            // Expected result:
            //     <p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>
            
            ExecuteExampleTest(369, "Inlines - Emphasis and strong emphasis",
                "**foo \"*bar*\" foo**",
                "<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>");
        }
        // 
        // 
        // Intraword emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example370()
        {
            // Source:
            //     **foo**bar
            // 
            // Expected result:
            //     <p><strong>foo</strong>bar</p>
            
            ExecuteExampleTest(370, "Inlines - Emphasis and strong emphasis",
                "**foo**bar",
                "<p><strong>foo</strong>bar</p>");
        }
        // 
        // 
        // Rule 8:
        // 
        // This is not strong emphasis, because the closing delimiter is
        // preceded by whitespace:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example371()
        {
            // Source:
            //     __foo bar __
            // 
            // Expected result:
            //     <p>__foo bar __</p>
            
            ExecuteExampleTest(371, "Inlines - Emphasis and strong emphasis",
                "__foo bar __",
                "<p>__foo bar __</p>");
        }
        // 
        // 
        // This is not strong emphasis, because the second `__` is
        // preceded by punctuation and followed by an alphanumeric:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example372()
        {
            // Source:
            //     __(__foo)
            // 
            // Expected result:
            //     <p>__(__foo)</p>
            
            ExecuteExampleTest(372, "Inlines - Emphasis and strong emphasis",
                "__(__foo)",
                "<p>__(__foo)</p>");
        }
        // 
        // 
        // The point of this restriction is more easily appreciated
        // with this example:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example373()
        {
            // Source:
            //     _(__foo__)_
            // 
            // Expected result:
            //     <p><em>(<strong>foo</strong>)</em></p>
            
            ExecuteExampleTest(373, "Inlines - Emphasis and strong emphasis",
                "_(__foo__)_",
                "<p><em>(<strong>foo</strong>)</em></p>");
        }
        // 
        // 
        // Intraword strong emphasis is forbidden with `__`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example374()
        {
            // Source:
            //     __foo__bar
            // 
            // Expected result:
            //     <p>__foo__bar</p>
            
            ExecuteExampleTest(374, "Inlines - Emphasis and strong emphasis",
                "__foo__bar",
                "<p>__foo__bar</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example375()
        {
            // Source:
            //     __пристаням__стремятся
            // 
            // Expected result:
            //     <p>__пристаням__стремятся</p>
            
            ExecuteExampleTest(375, "Inlines - Emphasis and strong emphasis",
                "__пристаням__стремятся",
                "<p>__пристаням__стремятся</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example376()
        {
            // Source:
            //     __foo__bar__baz__
            // 
            // Expected result:
            //     <p><strong>foo__bar__baz</strong></p>
            
            ExecuteExampleTest(376, "Inlines - Emphasis and strong emphasis",
                "__foo__bar__baz__",
                "<p><strong>foo__bar__baz</strong></p>");
        }
        // 
        // 
        // This is strong emphasis, even though the closing delimiter is
        // both left- and right-flanking, because it is followed by
        // punctuation:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example377()
        {
            // Source:
            //     __(bar)__.
            // 
            // Expected result:
            //     <p><strong>(bar)</strong>.</p>
            
            ExecuteExampleTest(377, "Inlines - Emphasis and strong emphasis",
                "__(bar)__.",
                "<p><strong>(bar)</strong>.</p>");
        }
        // 
        // 
        // Rule 9:
        // 
        // Any nonempty sequence of inline elements can be the contents of an
        // emphasized span.
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example378()
        {
            // Source:
            //     *foo [bar](/url)*
            // 
            // Expected result:
            //     <p><em>foo <a href="/url">bar</a></em></p>
            
            ExecuteExampleTest(378, "Inlines - Emphasis and strong emphasis",
                "*foo [bar](/url)*",
                "<p><em>foo <a href=\"/url\">bar</a></em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example379()
        {
            // Source:
            //     *foo
            //     bar*
            // 
            // Expected result:
            //     <p><em>foo
            //     bar</em></p>
            
            ExecuteExampleTest(379, "Inlines - Emphasis and strong emphasis",
                "*foo\r\nbar*",
                "<p><em>foo\r\nbar</em></p>");
        }
        // 
        // 
        // In particular, emphasis and strong emphasis can be nested
        // inside emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example380()
        {
            // Source:
            //     _foo __bar__ baz_
            // 
            // Expected result:
            //     <p><em>foo <strong>bar</strong> baz</em></p>
            
            ExecuteExampleTest(380, "Inlines - Emphasis and strong emphasis",
                "_foo __bar__ baz_",
                "<p><em>foo <strong>bar</strong> baz</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example381()
        {
            // Source:
            //     _foo _bar_ baz_
            // 
            // Expected result:
            //     <p><em>foo <em>bar</em> baz</em></p>
            
            ExecuteExampleTest(381, "Inlines - Emphasis and strong emphasis",
                "_foo _bar_ baz_",
                "<p><em>foo <em>bar</em> baz</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example382()
        {
            // Source:
            //     __foo_ bar_
            // 
            // Expected result:
            //     <p><em><em>foo</em> bar</em></p>
            
            ExecuteExampleTest(382, "Inlines - Emphasis and strong emphasis",
                "__foo_ bar_",
                "<p><em><em>foo</em> bar</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example383()
        {
            // Source:
            //     *foo *bar**
            // 
            // Expected result:
            //     <p><em>foo <em>bar</em></em></p>
            
            ExecuteExampleTest(383, "Inlines - Emphasis and strong emphasis",
                "*foo *bar**",
                "<p><em>foo <em>bar</em></em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example384()
        {
            // Source:
            //     *foo **bar** baz*
            // 
            // Expected result:
            //     <p><em>foo <strong>bar</strong> baz</em></p>
            
            ExecuteExampleTest(384, "Inlines - Emphasis and strong emphasis",
                "*foo **bar** baz*",
                "<p><em>foo <strong>bar</strong> baz</em></p>");
        }
        // 
        // 
        // But note:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example385()
        {
            // Source:
            //     *foo**bar**baz*
            // 
            // Expected result:
            //     <p><em>foo</em><em>bar</em><em>baz</em></p>
            
            ExecuteExampleTest(385, "Inlines - Emphasis and strong emphasis",
                "*foo**bar**baz*",
                "<p><em>foo</em><em>bar</em><em>baz</em></p>");
        }
        // 
        // 
        // The difference is that in the preceding case, the internal delimiters
        // [can close emphasis], while in the cases with spaces, they cannot.
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example386()
        {
            // Source:
            //     ***foo** bar*
            // 
            // Expected result:
            //     <p><em><strong>foo</strong> bar</em></p>
            
            ExecuteExampleTest(386, "Inlines - Emphasis and strong emphasis",
                "***foo** bar*",
                "<p><em><strong>foo</strong> bar</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example387()
        {
            // Source:
            //     *foo **bar***
            // 
            // Expected result:
            //     <p><em>foo <strong>bar</strong></em></p>
            
            ExecuteExampleTest(387, "Inlines - Emphasis and strong emphasis",
                "*foo **bar***",
                "<p><em>foo <strong>bar</strong></em></p>");
        }
        // 
        // 
        // Note, however, that in the following case we get no strong
        // emphasis, because the opening delimiter is closed by the first
        // `*` before `bar`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example388()
        {
            // Source:
            //     *foo**bar***
            // 
            // Expected result:
            //     <p><em>foo</em><em>bar</em>**</p>
            
            ExecuteExampleTest(388, "Inlines - Emphasis and strong emphasis",
                "*foo**bar***",
                "<p><em>foo</em><em>bar</em>**</p>");
        }
        // 
        // 
        // 
        // Indefinite levels of nesting are possible:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example389()
        {
            // Source:
            //     *foo **bar *baz* bim** bop*
            // 
            // Expected result:
            //     <p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>
            
            ExecuteExampleTest(389, "Inlines - Emphasis and strong emphasis",
                "*foo **bar *baz* bim** bop*",
                "<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example390()
        {
            // Source:
            //     *foo [*bar*](/url)*
            // 
            // Expected result:
            //     <p><em>foo <a href="/url"><em>bar</em></a></em></p>
            
            ExecuteExampleTest(390, "Inlines - Emphasis and strong emphasis",
                "*foo [*bar*](/url)*",
                "<p><em>foo <a href=\"/url\"><em>bar</em></a></em></p>");
        }
        // 
        // 
        // There can be no empty emphasis or strong emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example391()
        {
            // Source:
            //     ** is not an empty emphasis
            // 
            // Expected result:
            //     <p>** is not an empty emphasis</p>
            
            ExecuteExampleTest(391, "Inlines - Emphasis and strong emphasis",
                "** is not an empty emphasis",
                "<p>** is not an empty emphasis</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example392()
        {
            // Source:
            //     **** is not an empty strong emphasis
            // 
            // Expected result:
            //     <p>**** is not an empty strong emphasis</p>
            
            ExecuteExampleTest(392, "Inlines - Emphasis and strong emphasis",
                "**** is not an empty strong emphasis",
                "<p>**** is not an empty strong emphasis</p>");
        }
        // 
        // 
        // 
        // Rule 10:
        // 
        // Any nonempty sequence of inline elements can be the contents of an
        // strongly emphasized span.
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example393()
        {
            // Source:
            //     **foo [bar](/url)**
            // 
            // Expected result:
            //     <p><strong>foo <a href="/url">bar</a></strong></p>
            
            ExecuteExampleTest(393, "Inlines - Emphasis and strong emphasis",
                "**foo [bar](/url)**",
                "<p><strong>foo <a href=\"/url\">bar</a></strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example394()
        {
            // Source:
            //     **foo
            //     bar**
            // 
            // Expected result:
            //     <p><strong>foo
            //     bar</strong></p>
            
            ExecuteExampleTest(394, "Inlines - Emphasis and strong emphasis",
                "**foo\r\nbar**",
                "<p><strong>foo\r\nbar</strong></p>");
        }
        // 
        // 
        // In particular, emphasis and strong emphasis can be nested
        // inside strong emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example395()
        {
            // Source:
            //     __foo _bar_ baz__
            // 
            // Expected result:
            //     <p><strong>foo <em>bar</em> baz</strong></p>
            
            ExecuteExampleTest(395, "Inlines - Emphasis and strong emphasis",
                "__foo _bar_ baz__",
                "<p><strong>foo <em>bar</em> baz</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example396()
        {
            // Source:
            //     __foo __bar__ baz__
            // 
            // Expected result:
            //     <p><strong>foo <strong>bar</strong> baz</strong></p>
            
            ExecuteExampleTest(396, "Inlines - Emphasis and strong emphasis",
                "__foo __bar__ baz__",
                "<p><strong>foo <strong>bar</strong> baz</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example397()
        {
            // Source:
            //     ____foo__ bar__
            // 
            // Expected result:
            //     <p><strong><strong>foo</strong> bar</strong></p>
            
            ExecuteExampleTest(397, "Inlines - Emphasis and strong emphasis",
                "____foo__ bar__",
                "<p><strong><strong>foo</strong> bar</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example398()
        {
            // Source:
            //     **foo **bar****
            // 
            // Expected result:
            //     <p><strong>foo <strong>bar</strong></strong></p>
            
            ExecuteExampleTest(398, "Inlines - Emphasis and strong emphasis",
                "**foo **bar****",
                "<p><strong>foo <strong>bar</strong></strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example399()
        {
            // Source:
            //     **foo *bar* baz**
            // 
            // Expected result:
            //     <p><strong>foo <em>bar</em> baz</strong></p>
            
            ExecuteExampleTest(399, "Inlines - Emphasis and strong emphasis",
                "**foo *bar* baz**",
                "<p><strong>foo <em>bar</em> baz</strong></p>");
        }
        // 
        // 
        // But note:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example400()
        {
            // Source:
            //     **foo*bar*baz**
            // 
            // Expected result:
            //     <p><em><em>foo</em>bar</em>baz**</p>
            
            ExecuteExampleTest(400, "Inlines - Emphasis and strong emphasis",
                "**foo*bar*baz**",
                "<p><em><em>foo</em>bar</em>baz**</p>");
        }
        // 
        // 
        // The difference is that in the preceding case, the internal delimiters
        // [can close emphasis], while in the cases with spaces, they cannot.
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example401()
        {
            // Source:
            //     ***foo* bar**
            // 
            // Expected result:
            //     <p><strong><em>foo</em> bar</strong></p>
            
            ExecuteExampleTest(401, "Inlines - Emphasis and strong emphasis",
                "***foo* bar**",
                "<p><strong><em>foo</em> bar</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example402()
        {
            // Source:
            //     **foo *bar***
            // 
            // Expected result:
            //     <p><strong>foo <em>bar</em></strong></p>
            
            ExecuteExampleTest(402, "Inlines - Emphasis and strong emphasis",
                "**foo *bar***",
                "<p><strong>foo <em>bar</em></strong></p>");
        }
        // 
        // 
        // Indefinite levels of nesting are possible:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example403()
        {
            // Source:
            //     **foo *bar **baz**
            //     bim* bop**
            // 
            // Expected result:
            //     <p><strong>foo <em>bar <strong>baz</strong>
            //     bim</em> bop</strong></p>
            
            ExecuteExampleTest(403, "Inlines - Emphasis and strong emphasis",
                "**foo *bar **baz**\r\nbim* bop**",
                "<p><strong>foo <em>bar <strong>baz</strong>\r\nbim</em> bop</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example404()
        {
            // Source:
            //     **foo [*bar*](/url)**
            // 
            // Expected result:
            //     <p><strong>foo <a href="/url"><em>bar</em></a></strong></p>
            
            ExecuteExampleTest(404, "Inlines - Emphasis and strong emphasis",
                "**foo [*bar*](/url)**",
                "<p><strong>foo <a href=\"/url\"><em>bar</em></a></strong></p>");
        }
        // 
        // 
        // There can be no empty emphasis or strong emphasis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example405()
        {
            // Source:
            //     __ is not an empty emphasis
            // 
            // Expected result:
            //     <p>__ is not an empty emphasis</p>
            
            ExecuteExampleTest(405, "Inlines - Emphasis and strong emphasis",
                "__ is not an empty emphasis",
                "<p>__ is not an empty emphasis</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example406()
        {
            // Source:
            //     ____ is not an empty strong emphasis
            // 
            // Expected result:
            //     <p>____ is not an empty strong emphasis</p>
            
            ExecuteExampleTest(406, "Inlines - Emphasis and strong emphasis",
                "____ is not an empty strong emphasis",
                "<p>____ is not an empty strong emphasis</p>");
        }
        // 
        // 
        // 
        // Rule 11:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example407()
        {
            // Source:
            //     foo ***
            // 
            // Expected result:
            //     <p>foo ***</p>
            
            ExecuteExampleTest(407, "Inlines - Emphasis and strong emphasis",
                "foo ***",
                "<p>foo ***</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example408()
        {
            // Source:
            //     foo *\**
            // 
            // Expected result:
            //     <p>foo <em>*</em></p>
            
            ExecuteExampleTest(408, "Inlines - Emphasis and strong emphasis",
                "foo *\\**",
                "<p>foo <em>*</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example409()
        {
            // Source:
            //     foo *_*
            // 
            // Expected result:
            //     <p>foo <em>_</em></p>
            
            ExecuteExampleTest(409, "Inlines - Emphasis and strong emphasis",
                "foo *_*",
                "<p>foo <em>_</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example410()
        {
            // Source:
            //     foo *****
            // 
            // Expected result:
            //     <p>foo *****</p>
            
            ExecuteExampleTest(410, "Inlines - Emphasis and strong emphasis",
                "foo *****",
                "<p>foo *****</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example411()
        {
            // Source:
            //     foo **\***
            // 
            // Expected result:
            //     <p>foo <strong>*</strong></p>
            
            ExecuteExampleTest(411, "Inlines - Emphasis and strong emphasis",
                "foo **\\***",
                "<p>foo <strong>*</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example412()
        {
            // Source:
            //     foo **_**
            // 
            // Expected result:
            //     <p>foo <strong>_</strong></p>
            
            ExecuteExampleTest(412, "Inlines - Emphasis and strong emphasis",
                "foo **_**",
                "<p>foo <strong>_</strong></p>");
        }
        // 
        // 
        // Note that when delimiters do not match evenly, Rule 11 determines
        // that the excess literal `*` characters will appear outside of the
        // emphasis, rather than inside it:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example413()
        {
            // Source:
            //     **foo*
            // 
            // Expected result:
            //     <p>*<em>foo</em></p>
            
            ExecuteExampleTest(413, "Inlines - Emphasis and strong emphasis",
                "**foo*",
                "<p>*<em>foo</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example414()
        {
            // Source:
            //     *foo**
            // 
            // Expected result:
            //     <p><em>foo</em>*</p>
            
            ExecuteExampleTest(414, "Inlines - Emphasis and strong emphasis",
                "*foo**",
                "<p><em>foo</em>*</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example415()
        {
            // Source:
            //     ***foo**
            // 
            // Expected result:
            //     <p>*<strong>foo</strong></p>
            
            ExecuteExampleTest(415, "Inlines - Emphasis and strong emphasis",
                "***foo**",
                "<p>*<strong>foo</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example416()
        {
            // Source:
            //     ****foo*
            // 
            // Expected result:
            //     <p>***<em>foo</em></p>
            
            ExecuteExampleTest(416, "Inlines - Emphasis and strong emphasis",
                "****foo*",
                "<p>***<em>foo</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example417()
        {
            // Source:
            //     **foo***
            // 
            // Expected result:
            //     <p><strong>foo</strong>*</p>
            
            ExecuteExampleTest(417, "Inlines - Emphasis and strong emphasis",
                "**foo***",
                "<p><strong>foo</strong>*</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example418()
        {
            // Source:
            //     *foo****
            // 
            // Expected result:
            //     <p><em>foo</em>***</p>
            
            ExecuteExampleTest(418, "Inlines - Emphasis and strong emphasis",
                "*foo****",
                "<p><em>foo</em>***</p>");
        }
        // 
        // 
        // 
        // Rule 12:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example419()
        {
            // Source:
            //     foo ___
            // 
            // Expected result:
            //     <p>foo ___</p>
            
            ExecuteExampleTest(419, "Inlines - Emphasis and strong emphasis",
                "foo ___",
                "<p>foo ___</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example420()
        {
            // Source:
            //     foo _\__
            // 
            // Expected result:
            //     <p>foo <em>_</em></p>
            
            ExecuteExampleTest(420, "Inlines - Emphasis and strong emphasis",
                "foo _\\__",
                "<p>foo <em>_</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example421()
        {
            // Source:
            //     foo _*_
            // 
            // Expected result:
            //     <p>foo <em>*</em></p>
            
            ExecuteExampleTest(421, "Inlines - Emphasis and strong emphasis",
                "foo _*_",
                "<p>foo <em>*</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example422()
        {
            // Source:
            //     foo _____
            // 
            // Expected result:
            //     <p>foo _____</p>
            
            ExecuteExampleTest(422, "Inlines - Emphasis and strong emphasis",
                "foo _____",
                "<p>foo _____</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example423()
        {
            // Source:
            //     foo __\___
            // 
            // Expected result:
            //     <p>foo <strong>_</strong></p>
            
            ExecuteExampleTest(423, "Inlines - Emphasis and strong emphasis",
                "foo __\\___",
                "<p>foo <strong>_</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example424()
        {
            // Source:
            //     foo __*__
            // 
            // Expected result:
            //     <p>foo <strong>*</strong></p>
            
            ExecuteExampleTest(424, "Inlines - Emphasis and strong emphasis",
                "foo __*__",
                "<p>foo <strong>*</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example425()
        {
            // Source:
            //     __foo_
            // 
            // Expected result:
            //     <p>_<em>foo</em></p>
            
            ExecuteExampleTest(425, "Inlines - Emphasis and strong emphasis",
                "__foo_",
                "<p>_<em>foo</em></p>");
        }
        // 
        // 
        // Note that when delimiters do not match evenly, Rule 12 determines
        // that the excess literal `_` characters will appear outside of the
        // emphasis, rather than inside it:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example426()
        {
            // Source:
            //     _foo__
            // 
            // Expected result:
            //     <p><em>foo</em>_</p>
            
            ExecuteExampleTest(426, "Inlines - Emphasis and strong emphasis",
                "_foo__",
                "<p><em>foo</em>_</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example427()
        {
            // Source:
            //     ___foo__
            // 
            // Expected result:
            //     <p>_<strong>foo</strong></p>
            
            ExecuteExampleTest(427, "Inlines - Emphasis and strong emphasis",
                "___foo__",
                "<p>_<strong>foo</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example428()
        {
            // Source:
            //     ____foo_
            // 
            // Expected result:
            //     <p>___<em>foo</em></p>
            
            ExecuteExampleTest(428, "Inlines - Emphasis and strong emphasis",
                "____foo_",
                "<p>___<em>foo</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example429()
        {
            // Source:
            //     __foo___
            // 
            // Expected result:
            //     <p><strong>foo</strong>_</p>
            
            ExecuteExampleTest(429, "Inlines - Emphasis and strong emphasis",
                "__foo___",
                "<p><strong>foo</strong>_</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example430()
        {
            // Source:
            //     _foo____
            // 
            // Expected result:
            //     <p><em>foo</em>___</p>
            
            ExecuteExampleTest(430, "Inlines - Emphasis and strong emphasis",
                "_foo____",
                "<p><em>foo</em>___</p>");
        }
        // 
        // 
        // Rule 13 implies that if you want emphasis nested directly inside
        // emphasis, you must use different delimiters:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example431()
        {
            // Source:
            //     **foo**
            // 
            // Expected result:
            //     <p><strong>foo</strong></p>
            
            ExecuteExampleTest(431, "Inlines - Emphasis and strong emphasis",
                "**foo**",
                "<p><strong>foo</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example432()
        {
            // Source:
            //     *_foo_*
            // 
            // Expected result:
            //     <p><em><em>foo</em></em></p>
            
            ExecuteExampleTest(432, "Inlines - Emphasis and strong emphasis",
                "*_foo_*",
                "<p><em><em>foo</em></em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example433()
        {
            // Source:
            //     __foo__
            // 
            // Expected result:
            //     <p><strong>foo</strong></p>
            
            ExecuteExampleTest(433, "Inlines - Emphasis and strong emphasis",
                "__foo__",
                "<p><strong>foo</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example434()
        {
            // Source:
            //     _*foo*_
            // 
            // Expected result:
            //     <p><em><em>foo</em></em></p>
            
            ExecuteExampleTest(434, "Inlines - Emphasis and strong emphasis",
                "_*foo*_",
                "<p><em><em>foo</em></em></p>");
        }
        // 
        // 
        // However, strong emphasis within strong emphasis is possible without
        // switching delimiters:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example435()
        {
            // Source:
            //     ****foo****
            // 
            // Expected result:
            //     <p><strong><strong>foo</strong></strong></p>
            
            ExecuteExampleTest(435, "Inlines - Emphasis and strong emphasis",
                "****foo****",
                "<p><strong><strong>foo</strong></strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example436()
        {
            // Source:
            //     ____foo____
            // 
            // Expected result:
            //     <p><strong><strong>foo</strong></strong></p>
            
            ExecuteExampleTest(436, "Inlines - Emphasis and strong emphasis",
                "____foo____",
                "<p><strong><strong>foo</strong></strong></p>");
        }
        // 
        // 
        // 
        // Rule 13 can be applied to arbitrarily long sequences of
        // delimiters:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example437()
        {
            // Source:
            //     ******foo******
            // 
            // Expected result:
            //     <p><strong><strong><strong>foo</strong></strong></strong></p>
            
            ExecuteExampleTest(437, "Inlines - Emphasis and strong emphasis",
                "******foo******",
                "<p><strong><strong><strong>foo</strong></strong></strong></p>");
        }
        // 
        // 
        // Rule 14:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example438()
        {
            // Source:
            //     ***foo***
            // 
            // Expected result:
            //     <p><strong><em>foo</em></strong></p>
            
            ExecuteExampleTest(438, "Inlines - Emphasis and strong emphasis",
                "***foo***",
                "<p><strong><em>foo</em></strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example439()
        {
            // Source:
            //     _____foo_____
            // 
            // Expected result:
            //     <p><strong><strong><em>foo</em></strong></strong></p>
            
            ExecuteExampleTest(439, "Inlines - Emphasis and strong emphasis",
                "_____foo_____",
                "<p><strong><strong><em>foo</em></strong></strong></p>");
        }
        // 
        // 
        // Rule 15:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example440()
        {
            // Source:
            //     *foo _bar* baz_
            // 
            // Expected result:
            //     <p><em>foo _bar</em> baz_</p>
            
            ExecuteExampleTest(440, "Inlines - Emphasis and strong emphasis",
                "*foo _bar* baz_",
                "<p><em>foo _bar</em> baz_</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example441()
        {
            // Source:
            //     **foo*bar**
            // 
            // Expected result:
            //     <p><em><em>foo</em>bar</em>*</p>
            
            ExecuteExampleTest(441, "Inlines - Emphasis and strong emphasis",
                "**foo*bar**",
                "<p><em><em>foo</em>bar</em>*</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example442()
        {
            // Source:
            //     *foo __bar *baz bim__ bam*
            // 
            // Expected result:
            //     <p><em>foo <strong>bar *baz bim</strong> bam</em></p>
            
            ExecuteExampleTest(442, "Inlines - Emphasis and strong emphasis",
                "*foo __bar *baz bim__ bam*",
                "<p><em>foo <strong>bar *baz bim</strong> bam</em></p>");
        }
        // 
        // 
        // Rule 16:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example443()
        {
            // Source:
            //     **foo **bar baz**
            // 
            // Expected result:
            //     <p>**foo <strong>bar baz</strong></p>
            
            ExecuteExampleTest(443, "Inlines - Emphasis and strong emphasis",
                "**foo **bar baz**",
                "<p>**foo <strong>bar baz</strong></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example444()
        {
            // Source:
            //     *foo *bar baz*
            // 
            // Expected result:
            //     <p>*foo <em>bar baz</em></p>
            
            ExecuteExampleTest(444, "Inlines - Emphasis and strong emphasis",
                "*foo *bar baz*",
                "<p>*foo <em>bar baz</em></p>");
        }
        // 
        // 
        // Rule 17:
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example445()
        {
            // Source:
            //     *[bar*](/url)
            // 
            // Expected result:
            //     <p>*<a href="/url">bar*</a></p>
            
            ExecuteExampleTest(445, "Inlines - Emphasis and strong emphasis",
                "*[bar*](/url)",
                "<p>*<a href=\"/url\">bar*</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example446()
        {
            // Source:
            //     _foo [bar_](/url)
            // 
            // Expected result:
            //     <p>_foo <a href="/url">bar_</a></p>
            
            ExecuteExampleTest(446, "Inlines - Emphasis and strong emphasis",
                "_foo [bar_](/url)",
                "<p>_foo <a href=\"/url\">bar_</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example447()
        {
            // Source:
            //     *<img src="foo" title="*"/>
            // 
            // Expected result:
            //     <p>*<img src="foo" title="*"/></p>
            
            ExecuteExampleTest(447, "Inlines - Emphasis and strong emphasis",
                "*<img src=\"foo\" title=\"*\"/>",
                "<p>*<img src=\"foo\" title=\"*\"/></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example448()
        {
            // Source:
            //     **<a href="**">
            // 
            // Expected result:
            //     <p>**<a href="**"></p>
            
            ExecuteExampleTest(448, "Inlines - Emphasis and strong emphasis",
                "**<a href=\"**\">",
                "<p>**<a href=\"**\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example449()
        {
            // Source:
            //     __<a href="__">
            // 
            // Expected result:
            //     <p>__<a href="__"></p>
            
            ExecuteExampleTest(449, "Inlines - Emphasis and strong emphasis",
                "__<a href=\"__\">",
                "<p>__<a href=\"__\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example450()
        {
            // Source:
            //     *a `*`*
            // 
            // Expected result:
            //     <p><em>a <code>*</code></em></p>
            
            ExecuteExampleTest(450, "Inlines - Emphasis and strong emphasis",
                "*a `*`*",
                "<p><em>a <code>*</code></em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example451()
        {
            // Source:
            //     _a `_`_
            // 
            // Expected result:
            //     <p><em>a <code>_</code></em></p>
            
            ExecuteExampleTest(451, "Inlines - Emphasis and strong emphasis",
                "_a `_`_",
                "<p><em>a <code>_</code></em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example452()
        {
            // Source:
            //     **a<http://foo.bar/?q=**>
            // 
            // Expected result:
            //     <p>**a<a href="http://foo.bar/?q=**">http://foo.bar/?q=**</a></p>
            
            ExecuteExampleTest(452, "Inlines - Emphasis and strong emphasis",
                "**a<http://foo.bar/?q=**>",
                "<p>**a<a href=\"http://foo.bar/?q=**\">http://foo.bar/?q=**</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Emphasis and strong emphasis")]
        public void Example453()
        {
            // Source:
            //     __a<http://foo.bar/?q=__>
            // 
            // Expected result:
            //     <p>__a<a href="http://foo.bar/?q=__">http://foo.bar/?q=__</a></p>
            
            ExecuteExampleTest(453, "Inlines - Emphasis and strong emphasis",
                "__a<http://foo.bar/?q=__>",
                "<p>__a<a href=\"http://foo.bar/?q=__\">http://foo.bar/?q=__</a></p>");
        }
        // 
        // 
        // 
        // ## Links
        // 
        // A link contains [link text] (the visible text), a [link destination]
        // (the URI that is the link destination), and optionally a [link title].
        // There are two basic kinds of links in Markdown.  In [inline links] the
        // destination and title are given immediately after the link text.  In
        // [reference links] the destination and title are defined elsewhere in
        // the document.
        // 
        // A [link text](@) consists of a sequence of zero or more
        // inline elements enclosed by square brackets (`[` and `]`).  The
        // following rules apply:
        // 
        // - Links may not contain other links, at any level of nesting. If
        //   multiple otherwise valid link definitions appear nested inside each
        //   other, the inner-most definition is used.
        // 
        // - Brackets are allowed in the [link text] only if (a) they
        //   are backslash-escaped or (b) they appear as a matched pair of brackets,
        //   with an open bracket `[`, a sequence of zero or more inlines, and
        //   a close bracket `]`.
        // 
        // - Backtick [code spans], [autolinks], and raw [HTML tags] bind more tightly
        //   than the brackets in link text.  Thus, for example,
        //   `` [foo`]` `` could not be a link text, since the second `]`
        //   is part of a code span.
        // 
        // - The brackets in link text bind more tightly than markers for
        //   [emphasis and strong emphasis]. Thus, for example, `*[foo*](url)` is a link.
        // 
        // A [link destination](@) consists of either
        // 
        // - a sequence of zero or more characters between an opening `<` and a
        //   closing `>` that contains no spaces, line breaks, or unescaped
        //   `<` or `>` characters, or
        // 
        // - a nonempty sequence of characters that does not include
        //   ASCII space or control characters, and includes parentheses
        //   only if (a) they are backslash-escaped or (b) they are part of
        //   a balanced pair of unescaped parentheses that is not itself
        //   inside a balanced pair of unescaped parentheses.
        // 
        // A [link title](@)  consists of either
        // 
        // - a sequence of zero or more characters between straight double-quote
        //   characters (`"`), including a `"` character only if it is
        //   backslash-escaped, or
        // 
        // - a sequence of zero or more characters between straight single-quote
        //   characters (`'`), including a `'` character only if it is
        //   backslash-escaped, or
        // 
        // - a sequence of zero or more characters between matching parentheses
        //   (`(...)`), including a `)` character only if it is backslash-escaped.
        // 
        // Although [link titles] may span multiple lines, they may not contain
        // a [blank line].
        // 
        // An [inline link](@) consists of a [link text] followed immediately
        // by a left parenthesis `(`, optional [whitespace], an optional
        // [link destination], an optional [link title] separated from the link
        // destination by [whitespace], optional [whitespace], and a right
        // parenthesis `)`. The link's text consists of the inlines contained
        // in the [link text] (excluding the enclosing square brackets).
        // The link's URI consists of the link destination, excluding enclosing
        // `<...>` if present, with backslash-escapes in effect as described
        // above.  The link's title consists of the link title, excluding its
        // enclosing delimiters, with backslash-escapes in effect as described
        // above.
        // 
        // Here is a simple inline link:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example454()
        {
            // Source:
            //     [link](/uri "title")
            // 
            // Expected result:
            //     <p><a href="/uri" title="title">link</a></p>
            
            ExecuteExampleTest(454, "Inlines - Links",
                "[link](/uri \"title\")",
                "<p><a href=\"/uri\" title=\"title\">link</a></p>");
        }
        // 
        // 
        // The title may be omitted:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example455()
        {
            // Source:
            //     [link](/uri)
            // 
            // Expected result:
            //     <p><a href="/uri">link</a></p>
            
            ExecuteExampleTest(455, "Inlines - Links",
                "[link](/uri)",
                "<p><a href=\"/uri\">link</a></p>");
        }
        // 
        // 
        // Both the title and the destination may be omitted:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example456()
        {
            // Source:
            //     [link]()
            // 
            // Expected result:
            //     <p><a href="">link</a></p>
            
            ExecuteExampleTest(456, "Inlines - Links",
                "[link]()",
                "<p><a href=\"\">link</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example457()
        {
            // Source:
            //     [link](<>)
            // 
            // Expected result:
            //     <p><a href="">link</a></p>
            
            ExecuteExampleTest(457, "Inlines - Links",
                "[link](<>)",
                "<p><a href=\"\">link</a></p>");
        }
        // 
        // 
        // The destination cannot contain spaces or line breaks,
        // even if enclosed in pointy brackets:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example458()
        {
            // Source:
            //     [link](/my uri)
            // 
            // Expected result:
            //     <p>[link](/my uri)</p>
            
            ExecuteExampleTest(458, "Inlines - Links",
                "[link](/my uri)",
                "<p>[link](/my uri)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example459()
        {
            // Source:
            //     [link](</my uri>)
            // 
            // Expected result:
            //     <p>[link](&lt;/my uri&gt;)</p>
            
            ExecuteExampleTest(459, "Inlines - Links",
                "[link](</my uri>)",
                "<p>[link](&lt;/my uri&gt;)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example460()
        {
            // Source:
            //     [link](foo
            //     bar)
            // 
            // Expected result:
            //     <p>[link](foo
            //     bar)</p>
            
            ExecuteExampleTest(460, "Inlines - Links",
                "[link](foo\r\nbar)",
                "<p>[link](foo\r\nbar)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example461()
        {
            // Source:
            //     [link](<foo
            //     bar>)
            // 
            // Expected result:
            //     <p>[link](<foo
            //     bar>)</p>
            
            ExecuteExampleTest(461, "Inlines - Links",
                "[link](<foo\r\nbar>)",
                "<p>[link](<foo\r\nbar>)</p>");
        }
        // 
        // Parentheses inside the link destination may be escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example462()
        {
            // Source:
            //     [link](\(foo\))
            // 
            // Expected result:
            //     <p><a href="(foo)">link</a></p>
            
            ExecuteExampleTest(462, "Inlines - Links",
                "[link](\\(foo\\))",
                "<p><a href=\"(foo)\">link</a></p>");
        }
        // 
        // One level of balanced parentheses is allowed without escaping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example463()
        {
            // Source:
            //     [link]((foo)and(bar))
            // 
            // Expected result:
            //     <p><a href="(foo)and(bar)">link</a></p>
            
            ExecuteExampleTest(463, "Inlines - Links",
                "[link]((foo)and(bar))",
                "<p><a href=\"(foo)and(bar)\">link</a></p>");
        }
        // 
        // However, if you have parentheses within parentheses, you need to escape
        // or use the `<...>` form:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example464()
        {
            // Source:
            //     [link](foo(and(bar)))
            // 
            // Expected result:
            //     <p>[link](foo(and(bar)))</p>
            
            ExecuteExampleTest(464, "Inlines - Links",
                "[link](foo(and(bar)))",
                "<p>[link](foo(and(bar)))</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example465()
        {
            // Source:
            //     [link](foo(and\(bar\)))
            // 
            // Expected result:
            //     <p><a href="foo(and(bar))">link</a></p>
            
            ExecuteExampleTest(465, "Inlines - Links",
                "[link](foo(and\\(bar\\)))",
                "<p><a href=\"foo(and(bar))\">link</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example466()
        {
            // Source:
            //     [link](<foo(and(bar))>)
            // 
            // Expected result:
            //     <p><a href="foo(and(bar))">link</a></p>
            
            ExecuteExampleTest(466, "Inlines - Links",
                "[link](<foo(and(bar))>)",
                "<p><a href=\"foo(and(bar))\">link</a></p>");
        }
        // 
        // 
        // Parentheses and other symbols can also be escaped, as usual
        // in Markdown:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example467()
        {
            // Source:
            //     [link](foo\)\:)
            // 
            // Expected result:
            //     <p><a href="foo):">link</a></p>
            
            ExecuteExampleTest(467, "Inlines - Links",
                "[link](foo\\)\\:)",
                "<p><a href=\"foo):\">link</a></p>");
        }
        // 
        // 
        // A link can contain fragment identifiers and queries:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example468()
        {
            // Source:
            //     [link](#fragment)
            //     
            //     [link](http://example.com#fragment)
            //     
            //     [link](http://example.com?foo=3#frag)
            // 
            // Expected result:
            //     <p><a href="#fragment">link</a></p>
            //     <p><a href="http://example.com#fragment">link</a></p>
            //     <p><a href="http://example.com?foo=3#frag">link</a></p>
            
            ExecuteExampleTest(468, "Inlines - Links",
                "[link](#fragment)\r\n\r\n[link](http://example.com#fragment)\r\n\r\n[link](http://example.com?foo=3#frag)",
                "<p><a href=\"#fragment\">link</a></p>\r\n<p><a href=\"http://example.com#fragment\">link</a></p>\r\n<p><a href=\"http://example.com?foo=3#frag\">link</a></p>");
        }
        // 
        // 
        // Note that a backslash before a non-escapable character is
        // just a backslash:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example469()
        {
            // Source:
            //     [link](foo\bar)
            // 
            // Expected result:
            //     <p><a href="foo%5Cbar">link</a></p>
            
            ExecuteExampleTest(469, "Inlines - Links",
                "[link](foo\\bar)",
                "<p><a href=\"foo%5Cbar\">link</a></p>");
        }
        // 
        // 
        // URL-escaping should be left alone inside the destination, as all
        // URL-escaped characters are also valid URL characters. Entity and
        // numerical character references in the destination will be parsed
        // into the corresponding Unicode code points, as usual.  These may
        // be optionally URL-escaped when written as HTML, but this spec
        // does not enforce any particular policy for rendering URLs in
        // HTML or other formats.  Renderers may make different decisions
        // about how to escape or normalize URLs in the output.
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example470()
        {
            // Source:
            //     [link](foo%20b&auml;)
            // 
            // Expected result:
            //     <p><a href="foo%20b%C3%A4">link</a></p>
            
            ExecuteExampleTest(470, "Inlines - Links",
                "[link](foo%20b&auml;)",
                "<p><a href=\"foo%20b%C3%A4\">link</a></p>");
        }
        // 
        // 
        // Note that, because titles can often be parsed as destinations,
        // if you try to omit the destination and keep the title, you'll
        // get unexpected results:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example471()
        {
            // Source:
            //     [link]("title")
            // 
            // Expected result:
            //     <p><a href="%22title%22">link</a></p>
            
            ExecuteExampleTest(471, "Inlines - Links",
                "[link](\"title\")",
                "<p><a href=\"%22title%22\">link</a></p>");
        }
        // 
        // 
        // Titles may be in single quotes, double quotes, or parentheses:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example472()
        {
            // Source:
            //     [link](/url "title")
            //     [link](/url 'title')
            //     [link](/url (title))
            // 
            // Expected result:
            //     <p><a href="/url" title="title">link</a>
            //     <a href="/url" title="title">link</a>
            //     <a href="/url" title="title">link</a></p>
            
            ExecuteExampleTest(472, "Inlines - Links",
                "[link](/url \"title\")\r\n[link](/url 'title')\r\n[link](/url (title))",
                "<p><a href=\"/url\" title=\"title\">link</a>\r\n<a href=\"/url\" title=\"title\">link</a>\r\n<a href=\"/url\" title=\"title\">link</a></p>");
        }
        // 
        // 
        // Backslash escapes and entity and numeric character references
        // may be used in titles:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example473()
        {
            // Source:
            //     [link](/url "title \"&quot;")
            // 
            // Expected result:
            //     <p><a href="/url" title="title &quot;&quot;">link</a></p>
            
            ExecuteExampleTest(473, "Inlines - Links",
                "[link](/url \"title \\\"&quot;\")",
                "<p><a href=\"/url\" title=\"title &quot;&quot;\">link</a></p>");
        }
        // 
        // 
        // Nested balanced quotes are not allowed without escaping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example474()
        {
            // Source:
            //     [link](/url "title "and" title")
            // 
            // Expected result:
            //     <p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>
            
            ExecuteExampleTest(474, "Inlines - Links",
                "[link](/url \"title \"and\" title\")",
                "<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>");
        }
        // 
        // 
        // But it is easy to work around this by using a different quote type:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example475()
        {
            // Source:
            //     [link](/url 'title "and" title')
            // 
            // Expected result:
            //     <p><a href="/url" title="title &quot;and&quot; title">link</a></p>
            
            ExecuteExampleTest(475, "Inlines - Links",
                "[link](/url 'title \"and\" title')",
                "<p><a href=\"/url\" title=\"title &quot;and&quot; title\">link</a></p>");
        }
        // 
        // 
        // (Note:  `Markdown.pl` did allow double quotes inside a double-quoted
        // title, and its test suite included a test demonstrating this.
        // But it is hard to see a good rationale for the extra complexity this
        // brings, since there are already many ways---backslash escaping,
        // entity and numeric character references, or using a different
        // quote type for the enclosing title---to write titles containing
        // double quotes.  `Markdown.pl`'s handling of titles has a number
        // of other strange features.  For example, it allows single-quoted
        // titles in inline links, but not reference links.  And, in
        // reference links but not inline links, it allows a title to begin
        // with `"` and end with `)`.  `Markdown.pl` 1.0.1 even allows
        // titles with no closing quotation mark, though 1.0.2b8 does not.
        // It seems preferable to adopt a simple, rational rule that works
        // the same way in inline links and link reference definitions.)
        // 
        // [Whitespace] is allowed around the destination and title:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example476()
        {
            // Source:
            //     [link](   /uri
            //       "title"  )
            // 
            // Expected result:
            //     <p><a href="/uri" title="title">link</a></p>
            
            ExecuteExampleTest(476, "Inlines - Links",
                "[link](   /uri\r\n  \"title\"  )",
                "<p><a href=\"/uri\" title=\"title\">link</a></p>");
        }
        // 
        // 
        // But it is not allowed between the link text and the
        // following parenthesis:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example477()
        {
            // Source:
            //     [link] (/uri)
            // 
            // Expected result:
            //     <p>[link] (/uri)</p>
            
            ExecuteExampleTest(477, "Inlines - Links",
                "[link] (/uri)",
                "<p>[link] (/uri)</p>");
        }
        // 
        // 
        // The link text may contain balanced brackets, but not unbalanced ones,
        // unless they are escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example478()
        {
            // Source:
            //     [link [foo [bar]]](/uri)
            // 
            // Expected result:
            //     <p><a href="/uri">link [foo [bar]]</a></p>
            
            ExecuteExampleTest(478, "Inlines - Links",
                "[link [foo [bar]]](/uri)",
                "<p><a href=\"/uri\">link [foo [bar]]</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example479()
        {
            // Source:
            //     [link] bar](/uri)
            // 
            // Expected result:
            //     <p>[link] bar](/uri)</p>
            
            ExecuteExampleTest(479, "Inlines - Links",
                "[link] bar](/uri)",
                "<p>[link] bar](/uri)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example480()
        {
            // Source:
            //     [link [bar](/uri)
            // 
            // Expected result:
            //     <p>[link <a href="/uri">bar</a></p>
            
            ExecuteExampleTest(480, "Inlines - Links",
                "[link [bar](/uri)",
                "<p>[link <a href=\"/uri\">bar</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example481()
        {
            // Source:
            //     [link \[bar](/uri)
            // 
            // Expected result:
            //     <p><a href="/uri">link [bar</a></p>
            
            ExecuteExampleTest(481, "Inlines - Links",
                "[link \\[bar](/uri)",
                "<p><a href=\"/uri\">link [bar</a></p>");
        }
        // 
        // 
        // The link text may contain inline content:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example482()
        {
            // Source:
            //     [link *foo **bar** `#`*](/uri)
            // 
            // Expected result:
            //     <p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
            
            ExecuteExampleTest(482, "Inlines - Links",
                "[link *foo **bar** `#`*](/uri)",
                "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example483()
        {
            // Source:
            //     [![moon](moon.jpg)](/uri)
            // 
            // Expected result:
            //     <p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
            
            ExecuteExampleTest(483, "Inlines - Links",
                "[![moon](moon.jpg)](/uri)",
                "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>");
        }
        // 
        // 
        // However, links may not contain other links, at any level of nesting.
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example484()
        {
            // Source:
            //     [foo [bar](/uri)](/uri)
            // 
            // Expected result:
            //     <p>[foo <a href="/uri">bar</a>](/uri)</p>
            
            ExecuteExampleTest(484, "Inlines - Links",
                "[foo [bar](/uri)](/uri)",
                "<p>[foo <a href=\"/uri\">bar</a>](/uri)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example485()
        {
            // Source:
            //     [foo *[bar [baz](/uri)](/uri)*](/uri)
            // 
            // Expected result:
            //     <p>[foo <em>[bar <a href="/uri">baz</a>](/uri)</em>](/uri)</p>
            
            ExecuteExampleTest(485, "Inlines - Links",
                "[foo *[bar [baz](/uri)](/uri)*](/uri)",
                "<p>[foo <em>[bar <a href=\"/uri\">baz</a>](/uri)</em>](/uri)</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example486()
        {
            // Source:
            //     ![[[foo](uri1)](uri2)](uri3)
            // 
            // Expected result:
            //     <p><img src="uri3" alt="[foo](uri2)" /></p>
            
            ExecuteExampleTest(486, "Inlines - Links",
                "![[[foo](uri1)](uri2)](uri3)",
                "<p><img src=\"uri3\" alt=\"[foo](uri2)\" /></p>");
        }
        // 
        // 
        // These cases illustrate the precedence of link text grouping over
        // emphasis grouping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example487()
        {
            // Source:
            //     *[foo*](/uri)
            // 
            // Expected result:
            //     <p>*<a href="/uri">foo*</a></p>
            
            ExecuteExampleTest(487, "Inlines - Links",
                "*[foo*](/uri)",
                "<p>*<a href=\"/uri\">foo*</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example488()
        {
            // Source:
            //     [foo *bar](baz*)
            // 
            // Expected result:
            //     <p><a href="baz*">foo *bar</a></p>
            
            ExecuteExampleTest(488, "Inlines - Links",
                "[foo *bar](baz*)",
                "<p><a href=\"baz*\">foo *bar</a></p>");
        }
        // 
        // 
        // Note that brackets that *aren't* part of links do not take
        // precedence:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example489()
        {
            // Source:
            //     *foo [bar* baz]
            // 
            // Expected result:
            //     <p><em>foo [bar</em> baz]</p>
            
            ExecuteExampleTest(489, "Inlines - Links",
                "*foo [bar* baz]",
                "<p><em>foo [bar</em> baz]</p>");
        }
        // 
        // 
        // These cases illustrate the precedence of HTML tags, code spans,
        // and autolinks over link grouping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example490()
        {
            // Source:
            //     [foo <bar attr="](baz)">
            // 
            // Expected result:
            //     <p>[foo <bar attr="](baz)"></p>
            
            ExecuteExampleTest(490, "Inlines - Links",
                "[foo <bar attr=\"](baz)\">",
                "<p>[foo <bar attr=\"](baz)\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example491()
        {
            // Source:
            //     [foo`](/uri)`
            // 
            // Expected result:
            //     <p>[foo<code>](/uri)</code></p>
            
            ExecuteExampleTest(491, "Inlines - Links",
                "[foo`](/uri)`",
                "<p>[foo<code>](/uri)</code></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example492()
        {
            // Source:
            //     [foo<http://example.com/?search=](uri)>
            // 
            // Expected result:
            //     <p>[foo<a href="http://example.com/?search=%5D(uri)">http://example.com/?search=](uri)</a></p>
            
            ExecuteExampleTest(492, "Inlines - Links",
                "[foo<http://example.com/?search=](uri)>",
                "<p>[foo<a href=\"http://example.com/?search=%5D(uri)\">http://example.com/?search=](uri)</a></p>");
        }
        // 
        // 
        // There are three kinds of [reference link](@)s:
        // [full](#full-reference-link), [collapsed](#collapsed-reference-link),
        // and [shortcut](#shortcut-reference-link).
        // 
        // A [full reference link](@)
        // consists of a [link text] immediately followed by a [link label]
        // that [matches] a [link reference definition] elsewhere in the document.
        // 
        // A [link label](@)  begins with a left bracket (`[`) and ends
        // with the first right bracket (`]`) that is not backslash-escaped.
        // Between these brackets there must be at least one [non-whitespace character].
        // Unescaped square bracket characters are not allowed in
        // [link labels].  A link label can have at most 999
        // characters inside the square brackets.
        // 
        // One label [matches](@)
        // another just in case their normalized forms are equal.  To normalize a
        // label, perform the *Unicode case fold* and collapse consecutive internal
        // [whitespace] to a single space.  If there are multiple
        // matching reference link definitions, the one that comes first in the
        // document is used.  (It is desirable in such cases to emit a warning.)
        // 
        // The contents of the first link label are parsed as inlines, which are
        // used as the link's text.  The link's URI and title are provided by the
        // matching [link reference definition].
        // 
        // Here is a simple example:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example493()
        {
            // Source:
            //     [foo][bar]
            //     
            //     [bar]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(493, "Inlines - Links",
                "[foo][bar]\r\n\r\n[bar]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        // The rules for the [link text] are the same as with
        // [inline links].  Thus:
        // 
        // The link text may contain balanced brackets, but not unbalanced ones,
        // unless they are escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example494()
        {
            // Source:
            //     [link [foo [bar]]][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri">link [foo [bar]]</a></p>
            
            ExecuteExampleTest(494, "Inlines - Links",
                "[link [foo [bar]]][ref]\r\n\r\n[ref]: /uri",
                "<p><a href=\"/uri\">link [foo [bar]]</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example495()
        {
            // Source:
            //     [link \[bar][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri">link [bar</a></p>
            
            ExecuteExampleTest(495, "Inlines - Links",
                "[link \\[bar][ref]\r\n\r\n[ref]: /uri",
                "<p><a href=\"/uri\">link [bar</a></p>");
        }
        // 
        // 
        // The link text may contain inline content:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example496()
        {
            // Source:
            //     [link *foo **bar** `#`*][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
            
            ExecuteExampleTest(496, "Inlines - Links",
                "[link *foo **bar** `#`*][ref]\r\n\r\n[ref]: /uri",
                "<p><a href=\"/uri\">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example497()
        {
            // Source:
            //     [![moon](moon.jpg)][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
            
            ExecuteExampleTest(497, "Inlines - Links",
                "[![moon](moon.jpg)][ref]\r\n\r\n[ref]: /uri",
                "<p><a href=\"/uri\"><img src=\"moon.jpg\" alt=\"moon\" /></a></p>");
        }
        // 
        // 
        // However, links may not contain other links, at any level of nesting.
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example498()
        {
            // Source:
            //     [foo [bar](/uri)][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>[foo <a href="/uri">bar</a>]<a href="/uri">ref</a></p>
            
            ExecuteExampleTest(498, "Inlines - Links",
                "[foo [bar](/uri)][ref]\r\n\r\n[ref]: /uri",
                "<p>[foo <a href=\"/uri\">bar</a>]<a href=\"/uri\">ref</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example499()
        {
            // Source:
            //     [foo *bar [baz][ref]*][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>[foo <em>bar <a href="/uri">baz</a></em>]<a href="/uri">ref</a></p>
            
            ExecuteExampleTest(499, "Inlines - Links",
                "[foo *bar [baz][ref]*][ref]\r\n\r\n[ref]: /uri",
                "<p>[foo <em>bar <a href=\"/uri\">baz</a></em>]<a href=\"/uri\">ref</a></p>");
        }
        // 
        // 
        // (In the examples above, we have two [shortcut reference links]
        // instead of one [full reference link].)
        // 
        // The following cases illustrate the precedence of link text grouping over
        // emphasis grouping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example500()
        {
            // Source:
            //     *[foo*][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>*<a href="/uri">foo*</a></p>
            
            ExecuteExampleTest(500, "Inlines - Links",
                "*[foo*][ref]\r\n\r\n[ref]: /uri",
                "<p>*<a href=\"/uri\">foo*</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example501()
        {
            // Source:
            //     [foo *bar][ref]
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri">foo *bar</a></p>
            
            ExecuteExampleTest(501, "Inlines - Links",
                "[foo *bar][ref]\r\n\r\n[ref]: /uri",
                "<p><a href=\"/uri\">foo *bar</a></p>");
        }
        // 
        // 
        // These cases illustrate the precedence of HTML tags, code spans,
        // and autolinks over link grouping:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example502()
        {
            // Source:
            //     [foo <bar attr="][ref]">
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>[foo <bar attr="][ref]"></p>
            
            ExecuteExampleTest(502, "Inlines - Links",
                "[foo <bar attr=\"][ref]\">\r\n\r\n[ref]: /uri",
                "<p>[foo <bar attr=\"][ref]\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example503()
        {
            // Source:
            //     [foo`][ref]`
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>[foo<code>][ref]</code></p>
            
            ExecuteExampleTest(503, "Inlines - Links",
                "[foo`][ref]`\r\n\r\n[ref]: /uri",
                "<p>[foo<code>][ref]</code></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example504()
        {
            // Source:
            //     [foo<http://example.com/?search=][ref]>
            //     
            //     [ref]: /uri
            // 
            // Expected result:
            //     <p>[foo<a href="http://example.com/?search=%5D%5Bref%5D">http://example.com/?search=][ref]</a></p>
            
            ExecuteExampleTest(504, "Inlines - Links",
                "[foo<http://example.com/?search=][ref]>\r\n\r\n[ref]: /uri",
                "<p>[foo<a href=\"http://example.com/?search=%5D%5Bref%5D\">http://example.com/?search=][ref]</a></p>");
        }
        // 
        // 
        // Matching is case-insensitive:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example505()
        {
            // Source:
            //     [foo][BaR]
            //     
            //     [bar]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(505, "Inlines - Links",
                "[foo][BaR]\r\n\r\n[bar]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        // Unicode case fold is used:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example506()
        {
            // Source:
            //     [Толпой][Толпой] is a Russian word.
            //     
            //     [ТОЛПОЙ]: /url
            // 
            // Expected result:
            //     <p><a href="/url">Толпой</a> is a Russian word.</p>
            
            ExecuteExampleTest(506, "Inlines - Links",
                "[Толпой][Толпой] is a Russian word.\r\n\r\n[ТОЛПОЙ]: /url",
                "<p><a href=\"/url\">Толпой</a> is a Russian word.</p>");
        }
        // 
        // 
        // Consecutive internal [whitespace] is treated as one space for
        // purposes of determining matching:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example507()
        {
            // Source:
            //     [Foo
            //       bar]: /url
            //     
            //     [Baz][Foo bar]
            // 
            // Expected result:
            //     <p><a href="/url">Baz</a></p>
            
            ExecuteExampleTest(507, "Inlines - Links",
                "[Foo\r\n  bar]: /url\r\n\r\n[Baz][Foo bar]",
                "<p><a href=\"/url\">Baz</a></p>");
        }
        // 
        // 
        // No [whitespace] is allowed between the [link text] and the
        // [link label]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example508()
        {
            // Source:
            //     [foo] [bar]
            //     
            //     [bar]: /url "title"
            // 
            // Expected result:
            //     <p>[foo] <a href="/url" title="title">bar</a></p>
            
            ExecuteExampleTest(508, "Inlines - Links",
                "[foo] [bar]\r\n\r\n[bar]: /url \"title\"",
                "<p>[foo] <a href=\"/url\" title=\"title\">bar</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example509()
        {
            // Source:
            //     [foo]
            //     [bar]
            //     
            //     [bar]: /url "title"
            // 
            // Expected result:
            //     <p>[foo]
            //     <a href="/url" title="title">bar</a></p>
            
            ExecuteExampleTest(509, "Inlines - Links",
                "[foo]\r\n[bar]\r\n\r\n[bar]: /url \"title\"",
                "<p>[foo]\r\n<a href=\"/url\" title=\"title\">bar</a></p>");
        }
        // 
        // 
        // This is a departure from John Gruber's original Markdown syntax
        // description, which explicitly allows whitespace between the link
        // text and the link label.  It brings reference links in line with
        // [inline links], which (according to both original Markdown and
        // this spec) cannot have whitespace after the link text.  More
        // importantly, it prevents inadvertent capture of consecutive
        // [shortcut reference links]. If whitespace is allowed between the
        // link text and the link label, then in the following we will have
        // a single reference link, not two shortcut reference links, as
        // intended:
        // 
        // ``` markdown
        // [foo]
        // [bar]
        // 
        // [foo]: /url1
        // [bar]: /url2
        // ```
        // 
        // (Note that [shortcut reference links] were introduced by Gruber
        // himself in a beta version of `Markdown.pl`, but never included
        // in the official syntax description.  Without shortcut reference
        // links, it is harmless to allow space between the link text and
        // link label; but once shortcut references are introduced, it is
        // too dangerous to allow this, as it frequently leads to
        // unintended results.)
        // 
        // When there are multiple matching [link reference definitions],
        // the first is used:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example510()
        {
            // Source:
            //     [foo]: /url1
            //     
            //     [foo]: /url2
            //     
            //     [bar][foo]
            // 
            // Expected result:
            //     <p><a href="/url1">bar</a></p>
            
            ExecuteExampleTest(510, "Inlines - Links",
                "[foo]: /url1\r\n\r\n[foo]: /url2\r\n\r\n[bar][foo]",
                "<p><a href=\"/url1\">bar</a></p>");
        }
        // 
        // 
        // Note that matching is performed on normalized strings, not parsed
        // inline content.  So the following does not match, even though the
        // labels define equivalent inline content:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example511()
        {
            // Source:
            //     [bar][foo\!]
            //     
            //     [foo!]: /url
            // 
            // Expected result:
            //     <p>[bar][foo!]</p>
            
            ExecuteExampleTest(511, "Inlines - Links",
                "[bar][foo\\!]\r\n\r\n[foo!]: /url",
                "<p>[bar][foo!]</p>");
        }
        // 
        // 
        // [Link labels] cannot contain brackets, unless they are
        // backslash-escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example512()
        {
            // Source:
            //     [foo][ref[]
            //     
            //     [ref[]: /uri
            // 
            // Expected result:
            //     <p>[foo][ref[]</p>
            //     <p>[ref[]: /uri</p>
            
            ExecuteExampleTest(512, "Inlines - Links",
                "[foo][ref[]\r\n\r\n[ref[]: /uri",
                "<p>[foo][ref[]</p>\r\n<p>[ref[]: /uri</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example513()
        {
            // Source:
            //     [foo][ref[bar]]
            //     
            //     [ref[bar]]: /uri
            // 
            // Expected result:
            //     <p>[foo][ref[bar]]</p>
            //     <p>[ref[bar]]: /uri</p>
            
            ExecuteExampleTest(513, "Inlines - Links",
                "[foo][ref[bar]]\r\n\r\n[ref[bar]]: /uri",
                "<p>[foo][ref[bar]]</p>\r\n<p>[ref[bar]]: /uri</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example514()
        {
            // Source:
            //     [[[foo]]]
            //     
            //     [[[foo]]]: /url
            // 
            // Expected result:
            //     <p>[[[foo]]]</p>
            //     <p>[[[foo]]]: /url</p>
            
            ExecuteExampleTest(514, "Inlines - Links",
                "[[[foo]]]\r\n\r\n[[[foo]]]: /url",
                "<p>[[[foo]]]</p>\r\n<p>[[[foo]]]: /url</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example515()
        {
            // Source:
            //     [foo][ref\[]
            //     
            //     [ref\[]: /uri
            // 
            // Expected result:
            //     <p><a href="/uri">foo</a></p>
            
            ExecuteExampleTest(515, "Inlines - Links",
                "[foo][ref\\[]\r\n\r\n[ref\\[]: /uri",
                "<p><a href=\"/uri\">foo</a></p>");
        }
        // 
        // 
        // Note that in this example `]` is not backslash-escaped:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example516()
        {
            // Source:
            //     [bar\\]: /uri
            //     
            //     [bar\\]
            // 
            // Expected result:
            //     <p><a href="/uri">bar\</a></p>
            
            ExecuteExampleTest(516, "Inlines - Links",
                "[bar\\\\]: /uri\r\n\r\n[bar\\\\]",
                "<p><a href=\"/uri\">bar\\</a></p>");
        }
        // 
        // 
        // A [link label] must contain at least one [non-whitespace character]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example517()
        {
            // Source:
            //     []
            //     
            //     []: /uri
            // 
            // Expected result:
            //     <p>[]</p>
            //     <p>[]: /uri</p>
            
            ExecuteExampleTest(517, "Inlines - Links",
                "[]\r\n\r\n[]: /uri",
                "<p>[]</p>\r\n<p>[]: /uri</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example518()
        {
            // Source:
            //     [
            //      ]
            //     
            //     [
            //      ]: /uri
            // 
            // Expected result:
            //     <p>[
            //     ]</p>
            //     <p>[
            //     ]: /uri</p>
            
            ExecuteExampleTest(518, "Inlines - Links",
                "[\r\n ]\r\n\r\n[\r\n ]: /uri",
                "<p>[\r\n]</p>\r\n<p>[\r\n]: /uri</p>");
        }
        // 
        // 
        // A [collapsed reference link](@)
        // consists of a [link label] that [matches] a
        // [link reference definition] elsewhere in the
        // document, followed by the string `[]`.
        // The contents of the first link label are parsed as inlines,
        // which are used as the link's text.  The link's URI and title are
        // provided by the matching reference link definition.  Thus,
        // `[foo][]` is equivalent to `[foo][foo]`.
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example519()
        {
            // Source:
            //     [foo][]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(519, "Inlines - Links",
                "[foo][]\r\n\r\n[foo]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example520()
        {
            // Source:
            //     [*foo* bar][]
            //     
            //     [*foo* bar]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title"><em>foo</em> bar</a></p>
            
            ExecuteExampleTest(520, "Inlines - Links",
                "[*foo* bar][]\r\n\r\n[*foo* bar]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>");
        }
        // 
        // 
        // The link labels are case-insensitive:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example521()
        {
            // Source:
            //     [Foo][]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">Foo</a></p>
            
            ExecuteExampleTest(521, "Inlines - Links",
                "[Foo][]\r\n\r\n[foo]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">Foo</a></p>");
        }
        // 
        // 
        // 
        // As with full reference links, [whitespace] is not
        // allowed between the two sets of brackets:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example522()
        {
            // Source:
            //     [foo] 
            //     []
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a>
            //     []</p>
            
            ExecuteExampleTest(522, "Inlines - Links",
                "[foo] \r\n[]\r\n\r\n[foo]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">foo</a>\r\n[]</p>");
        }
        // 
        // 
        // A [shortcut reference link](@)
        // consists of a [link label] that [matches] a
        // [link reference definition] elsewhere in the
        // document and is not followed by `[]` or a link label.
        // The contents of the first link label are parsed as inlines,
        // which are used as the link's text.  the link's URI and title
        // are provided by the matching link reference definition.
        // Thus, `[foo]` is equivalent to `[foo][]`.
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example523()
        {
            // Source:
            //     [foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(523, "Inlines - Links",
                "[foo]\r\n\r\n[foo]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example524()
        {
            // Source:
            //     [*foo* bar]
            //     
            //     [*foo* bar]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title"><em>foo</em> bar</a></p>
            
            ExecuteExampleTest(524, "Inlines - Links",
                "[*foo* bar]\r\n\r\n[*foo* bar]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\"><em>foo</em> bar</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example525()
        {
            // Source:
            //     [[*foo* bar]]
            //     
            //     [*foo* bar]: /url "title"
            // 
            // Expected result:
            //     <p>[<a href="/url" title="title"><em>foo</em> bar</a>]</p>
            
            ExecuteExampleTest(525, "Inlines - Links",
                "[[*foo* bar]]\r\n\r\n[*foo* bar]: /url \"title\"",
                "<p>[<a href=\"/url\" title=\"title\"><em>foo</em> bar</a>]</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example526()
        {
            // Source:
            //     [[bar [foo]
            //     
            //     [foo]: /url
            // 
            // Expected result:
            //     <p>[[bar <a href="/url">foo</a></p>
            
            ExecuteExampleTest(526, "Inlines - Links",
                "[[bar [foo]\r\n\r\n[foo]: /url",
                "<p>[[bar <a href=\"/url\">foo</a></p>");
        }
        // 
        // 
        // The link labels are case-insensitive:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example527()
        {
            // Source:
            //     [Foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><a href="/url" title="title">Foo</a></p>
            
            ExecuteExampleTest(527, "Inlines - Links",
                "[Foo]\r\n\r\n[foo]: /url \"title\"",
                "<p><a href=\"/url\" title=\"title\">Foo</a></p>");
        }
        // 
        // 
        // A space after the link text should be preserved:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example528()
        {
            // Source:
            //     [foo] bar
            //     
            //     [foo]: /url
            // 
            // Expected result:
            //     <p><a href="/url">foo</a> bar</p>
            
            ExecuteExampleTest(528, "Inlines - Links",
                "[foo] bar\r\n\r\n[foo]: /url",
                "<p><a href=\"/url\">foo</a> bar</p>");
        }
        // 
        // 
        // If you just want bracketed text, you can backslash-escape the
        // opening bracket to avoid links:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example529()
        {
            // Source:
            //     \[foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p>[foo]</p>
            
            ExecuteExampleTest(529, "Inlines - Links",
                "\\[foo]\r\n\r\n[foo]: /url \"title\"",
                "<p>[foo]</p>");
        }
        // 
        // 
        // Note that this is a link, because a link label ends with the first
        // following closing bracket:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example530()
        {
            // Source:
            //     [foo*]: /url
            //     
            //     *[foo*]
            // 
            // Expected result:
            //     <p>*<a href="/url">foo*</a></p>
            
            ExecuteExampleTest(530, "Inlines - Links",
                "[foo*]: /url\r\n\r\n*[foo*]",
                "<p>*<a href=\"/url\">foo*</a></p>");
        }
        // 
        // 
        // Full references take precedence over shortcut references:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example531()
        {
            // Source:
            //     [foo][bar]
            //     
            //     [foo]: /url1
            //     [bar]: /url2
            // 
            // Expected result:
            //     <p><a href="/url2">foo</a></p>
            
            ExecuteExampleTest(531, "Inlines - Links",
                "[foo][bar]\r\n\r\n[foo]: /url1\r\n[bar]: /url2",
                "<p><a href=\"/url2\">foo</a></p>");
        }
        // 
        // 
        // In the following case `[bar][baz]` is parsed as a reference,
        // `[foo]` as normal text:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example532()
        {
            // Source:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url
            // 
            // Expected result:
            //     <p>[foo]<a href="/url">bar</a></p>
            
            ExecuteExampleTest(532, "Inlines - Links",
                "[foo][bar][baz]\r\n\r\n[baz]: /url",
                "<p>[foo]<a href=\"/url\">bar</a></p>");
        }
        // 
        // 
        // Here, though, `[foo][bar]` is parsed as a reference, since
        // `[bar]` is defined:
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example533()
        {
            // Source:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url1
            //     [bar]: /url2
            // 
            // Expected result:
            //     <p><a href="/url2">foo</a><a href="/url1">baz</a></p>
            
            ExecuteExampleTest(533, "Inlines - Links",
                "[foo][bar][baz]\r\n\r\n[baz]: /url1\r\n[bar]: /url2",
                "<p><a href=\"/url2\">foo</a><a href=\"/url1\">baz</a></p>");
        }
        // 
        // 
        // Here `[foo]` is not parsed as a shortcut reference, because it
        // is followed by a link label (even though `[bar]` is not defined):
        // 
        [TestMethod]
        [TestCategory("Inlines - Links")]
        public void Example534()
        {
            // Source:
            //     [foo][bar][baz]
            //     
            //     [baz]: /url1
            //     [foo]: /url2
            // 
            // Expected result:
            //     <p>[foo]<a href="/url1">bar</a></p>
            
            ExecuteExampleTest(534, "Inlines - Links",
                "[foo][bar][baz]\r\n\r\n[baz]: /url1\r\n[foo]: /url2",
                "<p>[foo]<a href=\"/url1\">bar</a></p>");
        }
        // 
        // 
        // 
        // ## Images
        // 
        // Syntax for images is like the syntax for links, with one
        // difference. Instead of [link text], we have an
        // [image description](@).  The rules for this are the
        // same as for [link text], except that (a) an
        // image description starts with `![` rather than `[`, and
        // (b) an image description may contain links.
        // An image description has inline elements
        // as its contents.  When an image is rendered to HTML,
        // this is standardly used as the image's `alt` attribute.
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example535()
        {
            // Source:
            //     ![foo](/url "title")
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" title="title" /></p>
            
            ExecuteExampleTest(535, "Inlines - Images",
                "![foo](/url \"title\")",
                "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example536()
        {
            // Source:
            //     ![foo *bar*]
            //     
            //     [foo *bar*]: train.jpg "train & tracks"
            // 
            // Expected result:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
            
            ExecuteExampleTest(536, "Inlines - Images",
                "![foo *bar*]\r\n\r\n[foo *bar*]: train.jpg \"train & tracks\"",
                "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example537()
        {
            // Source:
            //     ![foo ![bar](/url)](/url2)
            // 
            // Expected result:
            //     <p><img src="/url2" alt="foo bar" /></p>
            
            ExecuteExampleTest(537, "Inlines - Images",
                "![foo ![bar](/url)](/url2)",
                "<p><img src=\"/url2\" alt=\"foo bar\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example538()
        {
            // Source:
            //     ![foo [bar](/url)](/url2)
            // 
            // Expected result:
            //     <p><img src="/url2" alt="foo bar" /></p>
            
            ExecuteExampleTest(538, "Inlines - Images",
                "![foo [bar](/url)](/url2)",
                "<p><img src=\"/url2\" alt=\"foo bar\" /></p>");
        }
        // 
        // 
        // Though this spec is concerned with parsing, not rendering, it is
        // recommended that in rendering to HTML, only the plain string content
        // of the [image description] be used.  Note that in
        // the above example, the alt attribute's value is `foo bar`, not `foo
        // [bar](/url)` or `foo <a href="/url">bar</a>`.  Only the plain string
        // content is rendered, without formatting.
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example539()
        {
            // Source:
            //     ![foo *bar*][]
            //     
            //     [foo *bar*]: train.jpg "train & tracks"
            // 
            // Expected result:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
            
            ExecuteExampleTest(539, "Inlines - Images",
                "![foo *bar*][]\r\n\r\n[foo *bar*]: train.jpg \"train & tracks\"",
                "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example540()
        {
            // Source:
            //     ![foo *bar*][foobar]
            //     
            //     [FOOBAR]: train.jpg "train & tracks"
            // 
            // Expected result:
            //     <p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
            
            ExecuteExampleTest(540, "Inlines - Images",
                "![foo *bar*][foobar]\r\n\r\n[FOOBAR]: train.jpg \"train & tracks\"",
                "<p><img src=\"train.jpg\" alt=\"foo bar\" title=\"train &amp; tracks\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example541()
        {
            // Source:
            //     ![foo](train.jpg)
            // 
            // Expected result:
            //     <p><img src="train.jpg" alt="foo" /></p>
            
            ExecuteExampleTest(541, "Inlines - Images",
                "![foo](train.jpg)",
                "<p><img src=\"train.jpg\" alt=\"foo\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example542()
        {
            // Source:
            //     My ![foo bar](/path/to/train.jpg  "title"   )
            // 
            // Expected result:
            //     <p>My <img src="/path/to/train.jpg" alt="foo bar" title="title" /></p>
            
            ExecuteExampleTest(542, "Inlines - Images",
                "My ![foo bar](/path/to/train.jpg  \"title\"   )",
                "<p>My <img src=\"/path/to/train.jpg\" alt=\"foo bar\" title=\"title\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example543()
        {
            // Source:
            //     ![foo](<url>)
            // 
            // Expected result:
            //     <p><img src="url" alt="foo" /></p>
            
            ExecuteExampleTest(543, "Inlines - Images",
                "![foo](<url>)",
                "<p><img src=\"url\" alt=\"foo\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example544()
        {
            // Source:
            //     ![](/url)
            // 
            // Expected result:
            //     <p><img src="/url" alt="" /></p>
            
            ExecuteExampleTest(544, "Inlines - Images",
                "![](/url)",
                "<p><img src=\"/url\" alt=\"\" /></p>");
        }
        // 
        // 
        // Reference-style:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example545()
        {
            // Source:
            //     ![foo][bar]
            //     
            //     [bar]: /url
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" /></p>
            
            ExecuteExampleTest(545, "Inlines - Images",
                "![foo][bar]\r\n\r\n[bar]: /url",
                "<p><img src=\"/url\" alt=\"foo\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example546()
        {
            // Source:
            //     ![foo][bar]
            //     
            //     [BAR]: /url
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" /></p>
            
            ExecuteExampleTest(546, "Inlines - Images",
                "![foo][bar]\r\n\r\n[BAR]: /url",
                "<p><img src=\"/url\" alt=\"foo\" /></p>");
        }
        // 
        // 
        // Collapsed:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example547()
        {
            // Source:
            //     ![foo][]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" title="title" /></p>
            
            ExecuteExampleTest(547, "Inlines - Images",
                "![foo][]\r\n\r\n[foo]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example548()
        {
            // Source:
            //     ![*foo* bar][]
            //     
            //     [*foo* bar]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo bar" title="title" /></p>
            
            ExecuteExampleTest(548, "Inlines - Images",
                "![*foo* bar][]\r\n\r\n[*foo* bar]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>");
        }
        // 
        // 
        // The labels are case-insensitive:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example549()
        {
            // Source:
            //     ![Foo][]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="Foo" title="title" /></p>
            
            ExecuteExampleTest(549, "Inlines - Images",
                "![Foo][]\r\n\r\n[foo]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>");
        }
        // 
        // 
        // As with reference links, [whitespace] is not allowed
        // between the two sets of brackets:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example550()
        {
            // Source:
            //     ![foo] 
            //     []
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" title="title" />
            //     []</p>
            
            ExecuteExampleTest(550, "Inlines - Images",
                "![foo] \r\n[]\r\n\r\n[foo]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"foo\" title=\"title\" />\r\n[]</p>");
        }
        // 
        // 
        // Shortcut:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example551()
        {
            // Source:
            //     ![foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo" title="title" /></p>
            
            ExecuteExampleTest(551, "Inlines - Images",
                "![foo]\r\n\r\n[foo]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"foo\" title=\"title\" /></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example552()
        {
            // Source:
            //     ![*foo* bar]
            //     
            //     [*foo* bar]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="foo bar" title="title" /></p>
            
            ExecuteExampleTest(552, "Inlines - Images",
                "![*foo* bar]\r\n\r\n[*foo* bar]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"foo bar\" title=\"title\" /></p>");
        }
        // 
        // 
        // Note that link labels cannot contain unescaped brackets:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example553()
        {
            // Source:
            //     ![[foo]]
            //     
            //     [[foo]]: /url "title"
            // 
            // Expected result:
            //     <p>![[foo]]</p>
            //     <p>[[foo]]: /url &quot;title&quot;</p>
            
            ExecuteExampleTest(553, "Inlines - Images",
                "![[foo]]\r\n\r\n[[foo]]: /url \"title\"",
                "<p>![[foo]]</p>\r\n<p>[[foo]]: /url &quot;title&quot;</p>");
        }
        // 
        // 
        // The link labels are case-insensitive:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example554()
        {
            // Source:
            //     ![Foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p><img src="/url" alt="Foo" title="title" /></p>
            
            ExecuteExampleTest(554, "Inlines - Images",
                "![Foo]\r\n\r\n[foo]: /url \"title\"",
                "<p><img src=\"/url\" alt=\"Foo\" title=\"title\" /></p>");
        }
        // 
        // 
        // If you just want bracketed text, you can backslash-escape the
        // opening `!` and `[`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example555()
        {
            // Source:
            //     \!\[foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p>![foo]</p>
            
            ExecuteExampleTest(555, "Inlines - Images",
                "\\!\\[foo]\r\n\r\n[foo]: /url \"title\"",
                "<p>![foo]</p>");
        }
        // 
        // 
        // If you want a link after a literal `!`, backslash-escape the
        // `!`:
        // 
        [TestMethod]
        [TestCategory("Inlines - Images")]
        public void Example556()
        {
            // Source:
            //     \![foo]
            //     
            //     [foo]: /url "title"
            // 
            // Expected result:
            //     <p>!<a href="/url" title="title">foo</a></p>
            
            ExecuteExampleTest(556, "Inlines - Images",
                "\\![foo]\r\n\r\n[foo]: /url \"title\"",
                "<p>!<a href=\"/url\" title=\"title\">foo</a></p>");
        }
        // 
        // 
        // ## Autolinks
        // 
        // [Autolink](@)s are absolute URIs and email addresses inside
        // `<` and `>`. They are parsed as links, with the URL or email address
        // as the link label.
        // 
        // A [URI autolink](@) consists of `<`, followed by an
        // [absolute URI] not containing `<`, followed by `>`.  It is parsed as
        // a link to the URI, with the URI as the link's label.
        // 
        // An [absolute URI](@),
        // for these purposes, consists of a [scheme] followed by a colon (`:`)
        // followed by zero or more characters other than ASCII
        // [whitespace] and control characters, `<`, and `>`.  If
        // the URI includes these characters, they must be percent-encoded
        // (e.g. `%20` for a space).
        // 
        // For purposes of this spec, a [scheme](@) is any sequence
        // of 2--32 characters beginning with an ASCII letter and followed
        // by any combination of ASCII letters, digits, or the symbols plus
        // ("+"), period ("."), or hyphen ("-").
        // 
        // Here are some valid autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example557()
        {
            // Source:
            //     <http://foo.bar.baz>
            // 
            // Expected result:
            //     <p><a href="http://foo.bar.baz">http://foo.bar.baz</a></p>
            
            ExecuteExampleTest(557, "Inlines - Autolinks",
                "<http://foo.bar.baz>",
                "<p><a href=\"http://foo.bar.baz\">http://foo.bar.baz</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example558()
        {
            // Source:
            //     <http://foo.bar.baz/test?q=hello&id=22&boolean>
            // 
            // Expected result:
            //     <p><a href="http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean">http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>
            
            ExecuteExampleTest(558, "Inlines - Autolinks",
                "<http://foo.bar.baz/test?q=hello&id=22&boolean>",
                "<p><a href=\"http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean\">http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example559()
        {
            // Source:
            //     <irc://foo.bar:2233/baz>
            // 
            // Expected result:
            //     <p><a href="irc://foo.bar:2233/baz">irc://foo.bar:2233/baz</a></p>
            
            ExecuteExampleTest(559, "Inlines - Autolinks",
                "<irc://foo.bar:2233/baz>",
                "<p><a href=\"irc://foo.bar:2233/baz\">irc://foo.bar:2233/baz</a></p>");
        }
        // 
        // 
        // Uppercase is also fine:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example560()
        {
            // Source:
            //     <MAILTO:FOO@BAR.BAZ>
            // 
            // Expected result:
            //     <p><a href="MAILTO:FOO@BAR.BAZ">MAILTO:FOO@BAR.BAZ</a></p>
            
            ExecuteExampleTest(560, "Inlines - Autolinks",
                "<MAILTO:FOO@BAR.BAZ>",
                "<p><a href=\"MAILTO:FOO@BAR.BAZ\">MAILTO:FOO@BAR.BAZ</a></p>");
        }
        // 
        // 
        // Note that many strings that count as [absolute URIs] for
        // purposes of this spec are not valid URIs, because their
        // schemes are not registered or because of other problems
        // with their syntax:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example561()
        {
            // Source:
            //     <a+b+c:d>
            // 
            // Expected result:
            //     <p><a href="a+b+c:d">a+b+c:d</a></p>
            
            ExecuteExampleTest(561, "Inlines - Autolinks",
                "<a+b+c:d>",
                "<p><a href=\"a+b+c:d\">a+b+c:d</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example562()
        {
            // Source:
            //     <made-up-scheme://foo,bar>
            // 
            // Expected result:
            //     <p><a href="made-up-scheme://foo,bar">made-up-scheme://foo,bar</a></p>
            
            ExecuteExampleTest(562, "Inlines - Autolinks",
                "<made-up-scheme://foo,bar>",
                "<p><a href=\"made-up-scheme://foo,bar\">made-up-scheme://foo,bar</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example563()
        {
            // Source:
            //     <http://../>
            // 
            // Expected result:
            //     <p><a href="http://../">http://../</a></p>
            
            ExecuteExampleTest(563, "Inlines - Autolinks",
                "<http://../>",
                "<p><a href=\"http://../\">http://../</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example564()
        {
            // Source:
            //     <localhost:5001/foo>
            // 
            // Expected result:
            //     <p><a href="localhost:5001/foo">localhost:5001/foo</a></p>
            
            ExecuteExampleTest(564, "Inlines - Autolinks",
                "<localhost:5001/foo>",
                "<p><a href=\"localhost:5001/foo\">localhost:5001/foo</a></p>");
        }
        // 
        // 
        // Spaces are not allowed in autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example565()
        {
            // Source:
            //     <http://foo.bar/baz bim>
            // 
            // Expected result:
            //     <p>&lt;http://foo.bar/baz bim&gt;</p>
            
            ExecuteExampleTest(565, "Inlines - Autolinks",
                "<http://foo.bar/baz bim>",
                "<p>&lt;http://foo.bar/baz bim&gt;</p>");
        }
        // 
        // 
        // Backslash-escapes do not work inside autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example566()
        {
            // Source:
            //     <http://example.com/\[\>
            // 
            // Expected result:
            //     <p><a href="http://example.com/%5C%5B%5C">http://example.com/\[\</a></p>
            
            ExecuteExampleTest(566, "Inlines - Autolinks",
                "<http://example.com/\\[\\>",
                "<p><a href=\"http://example.com/%5C%5B%5C\">http://example.com/\\[\\</a></p>");
        }
        // 
        // 
        // An [email autolink](@)
        // consists of `<`, followed by an [email address],
        // followed by `>`.  The link's label is the email address,
        // and the URL is `mailto:` followed by the email address.
        // 
        // An [email address](@),
        // for these purposes, is anything that matches
        // the [non-normative regex from the HTML5
        // spec](https://html.spec.whatwg.org/multipage/forms.html#e-mail-state-(type=email)):
        // 
        //     /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?
        //     (?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/
        // 
        // Examples of email autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example567()
        {
            // Source:
            //     <foo@bar.example.com>
            // 
            // Expected result:
            //     <p><a href="mailto:foo@bar.example.com">foo@bar.example.com</a></p>
            
            ExecuteExampleTest(567, "Inlines - Autolinks",
                "<foo@bar.example.com>",
                "<p><a href=\"mailto:foo@bar.example.com\">foo@bar.example.com</a></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example568()
        {
            // Source:
            //     <foo+special@Bar.baz-bar0.com>
            // 
            // Expected result:
            //     <p><a href="mailto:foo+special@Bar.baz-bar0.com">foo+special@Bar.baz-bar0.com</a></p>
            
            ExecuteExampleTest(568, "Inlines - Autolinks",
                "<foo+special@Bar.baz-bar0.com>",
                "<p><a href=\"mailto:foo+special@Bar.baz-bar0.com\">foo+special@Bar.baz-bar0.com</a></p>");
        }
        // 
        // 
        // Backslash-escapes do not work inside email autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example569()
        {
            // Source:
            //     <foo\+@bar.example.com>
            // 
            // Expected result:
            //     <p>&lt;foo+@bar.example.com&gt;</p>
            
            ExecuteExampleTest(569, "Inlines - Autolinks",
                "<foo\\+@bar.example.com>",
                "<p>&lt;foo+@bar.example.com&gt;</p>");
        }
        // 
        // 
        // These are not autolinks:
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example570()
        {
            // Source:
            //     <>
            // 
            // Expected result:
            //     <p>&lt;&gt;</p>
            
            ExecuteExampleTest(570, "Inlines - Autolinks",
                "<>",
                "<p>&lt;&gt;</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example571()
        {
            // Source:
            //     < http://foo.bar >
            // 
            // Expected result:
            //     <p>&lt; http://foo.bar &gt;</p>
            
            ExecuteExampleTest(571, "Inlines - Autolinks",
                "< http://foo.bar >",
                "<p>&lt; http://foo.bar &gt;</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example572()
        {
            // Source:
            //     <m:abc>
            // 
            // Expected result:
            //     <p>&lt;m:abc&gt;</p>
            
            ExecuteExampleTest(572, "Inlines - Autolinks",
                "<m:abc>",
                "<p>&lt;m:abc&gt;</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example573()
        {
            // Source:
            //     <foo.bar.baz>
            // 
            // Expected result:
            //     <p>&lt;foo.bar.baz&gt;</p>
            
            ExecuteExampleTest(573, "Inlines - Autolinks",
                "<foo.bar.baz>",
                "<p>&lt;foo.bar.baz&gt;</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example574()
        {
            // Source:
            //     http://example.com
            // 
            // Expected result:
            //     <p>http://example.com</p>
            
            ExecuteExampleTest(574, "Inlines - Autolinks",
                "http://example.com",
                "<p>http://example.com</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Autolinks")]
        public void Example575()
        {
            // Source:
            //     foo@bar.example.com
            // 
            // Expected result:
            //     <p>foo@bar.example.com</p>
            
            ExecuteExampleTest(575, "Inlines - Autolinks",
                "foo@bar.example.com",
                "<p>foo@bar.example.com</p>");
        }
        // 
        // 
        // ## Raw HTML
        // 
        // Text between `<` and `>` that looks like an HTML tag is parsed as a
        // raw HTML tag and will be rendered in HTML without escaping.
        // Tag and attribute names are not limited to current HTML tags,
        // so custom tags (and even, say, DocBook tags) may be used.
        // 
        // Here is the grammar for tags:
        // 
        // A [tag name](@) consists of an ASCII letter
        // followed by zero or more ASCII letters, digits, or
        // hyphens (`-`).
        // 
        // An [attribute](@) consists of [whitespace],
        // an [attribute name], and an optional
        // [attribute value specification].
        // 
        // An [attribute name](@)
        // consists of an ASCII letter, `_`, or `:`, followed by zero or more ASCII
        // letters, digits, `_`, `.`, `:`, or `-`.  (Note:  This is the XML
        // specification restricted to ASCII.  HTML5 is laxer.)
        // 
        // An [attribute value specification](@)
        // consists of optional [whitespace],
        // a `=` character, optional [whitespace], and an [attribute
        // value].
        // 
        // An [attribute value](@)
        // consists of an [unquoted attribute value],
        // a [single-quoted attribute value], or a [double-quoted attribute value].
        // 
        // An [unquoted attribute value](@)
        // is a nonempty string of characters not
        // including spaces, `"`, `'`, `=`, `<`, `>`, or `` ` ``.
        // 
        // A [single-quoted attribute value](@)
        // consists of `'`, zero or more
        // characters not including `'`, and a final `'`.
        // 
        // A [double-quoted attribute value](@)
        // consists of `"`, zero or more
        // characters not including `"`, and a final `"`.
        // 
        // An [open tag](@) consists of a `<` character, a [tag name],
        // zero or more [attributes], optional [whitespace], an optional `/`
        // character, and a `>` character.
        // 
        // A [closing tag](@) consists of the string `</`, a
        // [tag name], optional [whitespace], and the character `>`.
        // 
        // An [HTML comment](@) consists of `<!--` + *text* + `-->`,
        // where *text* does not start with `>` or `->`, does not end with `-`,
        // and does not contain `--`.  (See the
        // [HTML5 spec](http://www.w3.org/TR/html5/syntax.html#comments).)
        // 
        // A [processing instruction](@)
        // consists of the string `<?`, a string
        // of characters not including the string `?>`, and the string
        // `?>`.
        // 
        // A [declaration](@) consists of the
        // string `<!`, a name consisting of one or more uppercase ASCII letters,
        // [whitespace], a string of characters not including the
        // character `>`, and the character `>`.
        // 
        // A [CDATA section](@) consists of
        // the string `<![CDATA[`, a string of characters not including the string
        // `]]>`, and the string `]]>`.
        // 
        // An [HTML tag](@) consists of an [open tag], a [closing tag],
        // an [HTML comment], a [processing instruction], a [declaration],
        // or a [CDATA section].
        // 
        // Here are some simple open tags:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example576()
        {
            // Source:
            //     <a><bab><c2c>
            // 
            // Expected result:
            //     <p><a><bab><c2c></p>
            
            ExecuteExampleTest(576, "Inlines - Raw HTML",
                "<a><bab><c2c>",
                "<p><a><bab><c2c></p>");
        }
        // 
        // 
        // Empty elements:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example577()
        {
            // Source:
            //     <a/><b2/>
            // 
            // Expected result:
            //     <p><a/><b2/></p>
            
            ExecuteExampleTest(577, "Inlines - Raw HTML",
                "<a/><b2/>",
                "<p><a/><b2/></p>");
        }
        // 
        // 
        // [Whitespace] is allowed:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example578()
        {
            // Source:
            //     <a  /><b2
            //     data="foo" >
            // 
            // Expected result:
            //     <p><a  /><b2
            //     data="foo" ></p>
            
            ExecuteExampleTest(578, "Inlines - Raw HTML",
                "<a  /><b2\r\ndata=\"foo\" >",
                "<p><a  /><b2\r\ndata=\"foo\" ></p>");
        }
        // 
        // 
        // With attributes:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example579()
        {
            // Source:
            //     <a foo="bar" bam = 'baz <em>"</em>'
            //     _boolean zoop:33=zoop:33 />
            // 
            // Expected result:
            //     <p><a foo="bar" bam = 'baz <em>"</em>'
            //     _boolean zoop:33=zoop:33 /></p>
            
            ExecuteExampleTest(579, "Inlines - Raw HTML",
                "<a foo=\"bar\" bam = 'baz <em>\"</em>'\r\n_boolean zoop:33=zoop:33 />",
                "<p><a foo=\"bar\" bam = 'baz <em>\"</em>'\r\n_boolean zoop:33=zoop:33 /></p>");
        }
        // 
        // 
        // Custom tag names can be used:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example580()
        {
            // Source:
            //     Foo <responsive-image src="foo.jpg" />
            // 
            // Expected result:
            //     <p>Foo <responsive-image src="foo.jpg" /></p>
            
            ExecuteExampleTest(580, "Inlines - Raw HTML",
                "Foo <responsive-image src=\"foo.jpg\" />",
                "<p>Foo <responsive-image src=\"foo.jpg\" /></p>");
        }
        // 
        // 
        // Illegal tag names, not parsed as HTML:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example581()
        {
            // Source:
            //     <33> <__>
            // 
            // Expected result:
            //     <p>&lt;33&gt; &lt;__&gt;</p>
            
            ExecuteExampleTest(581, "Inlines - Raw HTML",
                "<33> <__>",
                "<p>&lt;33&gt; &lt;__&gt;</p>");
        }
        // 
        // 
        // Illegal attribute names:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example582()
        {
            // Source:
            //     <a h*#ref="hi">
            // 
            // Expected result:
            //     <p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>
            
            ExecuteExampleTest(582, "Inlines - Raw HTML",
                "<a h*#ref=\"hi\">",
                "<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>");
        }
        // 
        // 
        // Illegal attribute values:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example583()
        {
            // Source:
            //     <a href="hi'> <a href=hi'>
            // 
            // Expected result:
            //     <p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>
            
            ExecuteExampleTest(583, "Inlines - Raw HTML",
                "<a href=\"hi'> <a href=hi'>",
                "<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>");
        }
        // 
        // 
        // Illegal [whitespace]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example584()
        {
            // Source:
            //     < a><
            //     foo><bar/ >
            // 
            // Expected result:
            //     <p>&lt; a&gt;&lt;
            //     foo&gt;&lt;bar/ &gt;</p>
            
            ExecuteExampleTest(584, "Inlines - Raw HTML",
                "< a><\r\nfoo><bar/ >",
                "<p>&lt; a&gt;&lt;\r\nfoo&gt;&lt;bar/ &gt;</p>");
        }
        // 
        // 
        // Missing [whitespace]:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example585()
        {
            // Source:
            //     <a href='bar'title=title>
            // 
            // Expected result:
            //     <p>&lt;a href='bar'title=title&gt;</p>
            
            ExecuteExampleTest(585, "Inlines - Raw HTML",
                "<a href='bar'title=title>",
                "<p>&lt;a href='bar'title=title&gt;</p>");
        }
        // 
        // 
        // Closing tags:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example586()
        {
            // Source:
            //     </a></foo >
            // 
            // Expected result:
            //     <p></a></foo ></p>
            
            ExecuteExampleTest(586, "Inlines - Raw HTML",
                "</a></foo >",
                "<p></a></foo ></p>");
        }
        // 
        // 
        // Illegal attributes in closing tag:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example587()
        {
            // Source:
            //     </a href="foo">
            // 
            // Expected result:
            //     <p>&lt;/a href=&quot;foo&quot;&gt;</p>
            
            ExecuteExampleTest(587, "Inlines - Raw HTML",
                "</a href=\"foo\">",
                "<p>&lt;/a href=&quot;foo&quot;&gt;</p>");
        }
        // 
        // 
        // Comments:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example588()
        {
            // Source:
            //     foo <!-- this is a
            //     comment - with hyphen -->
            // 
            // Expected result:
            //     <p>foo <!-- this is a
            //     comment - with hyphen --></p>
            
            ExecuteExampleTest(588, "Inlines - Raw HTML",
                "foo <!-- this is a\r\ncomment - with hyphen -->",
                "<p>foo <!-- this is a\r\ncomment - with hyphen --></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example589()
        {
            // Source:
            //     foo <!-- not a comment -- two hyphens -->
            // 
            // Expected result:
            //     <p>foo &lt;!-- not a comment -- two hyphens --&gt;</p>
            
            ExecuteExampleTest(589, "Inlines - Raw HTML",
                "foo <!-- not a comment -- two hyphens -->",
                "<p>foo &lt;!-- not a comment -- two hyphens --&gt;</p>");
        }
        // 
        // 
        // Not comments:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example590()
        {
            // Source:
            //     foo <!--> foo -->
            //     
            //     foo <!-- foo--->
            // 
            // Expected result:
            //     <p>foo &lt;!--&gt; foo --&gt;</p>
            //     <p>foo &lt;!-- foo---&gt;</p>
            
            ExecuteExampleTest(590, "Inlines - Raw HTML",
                "foo <!--> foo -->\r\n\r\nfoo <!-- foo--->",
                "<p>foo &lt;!--&gt; foo --&gt;</p>\r\n<p>foo &lt;!-- foo---&gt;</p>");
        }
        // 
        // 
        // Processing instructions:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example591()
        {
            // Source:
            //     foo <?php echo $a; ?>
            // 
            // Expected result:
            //     <p>foo <?php echo $a; ?></p>
            
            ExecuteExampleTest(591, "Inlines - Raw HTML",
                "foo <?php echo $a; ?>",
                "<p>foo <?php echo $a; ?></p>");
        }
        // 
        // 
        // Declarations:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example592()
        {
            // Source:
            //     foo <!ELEMENT br EMPTY>
            // 
            // Expected result:
            //     <p>foo <!ELEMENT br EMPTY></p>
            
            ExecuteExampleTest(592, "Inlines - Raw HTML",
                "foo <!ELEMENT br EMPTY>",
                "<p>foo <!ELEMENT br EMPTY></p>");
        }
        // 
        // 
        // CDATA sections:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example593()
        {
            // Source:
            //     foo <![CDATA[>&<]]>
            // 
            // Expected result:
            //     <p>foo <![CDATA[>&<]]></p>
            
            ExecuteExampleTest(593, "Inlines - Raw HTML",
                "foo <![CDATA[>&<]]>",
                "<p>foo <![CDATA[>&<]]></p>");
        }
        // 
        // 
        // Entity and numeric character references are preserved in HTML
        // attributes:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example594()
        {
            // Source:
            //     foo <a href="&ouml;">
            // 
            // Expected result:
            //     <p>foo <a href="&ouml;"></p>
            
            ExecuteExampleTest(594, "Inlines - Raw HTML",
                "foo <a href=\"&ouml;\">",
                "<p>foo <a href=\"&ouml;\"></p>");
        }
        // 
        // 
        // Backslash escapes do not work in HTML attributes:
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example595()
        {
            // Source:
            //     foo <a href="\*">
            // 
            // Expected result:
            //     <p>foo <a href="\*"></p>
            
            ExecuteExampleTest(595, "Inlines - Raw HTML",
                "foo <a href=\"\\*\">",
                "<p>foo <a href=\"\\*\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Raw HTML")]
        public void Example596()
        {
            // Source:
            //     <a href="\"">
            // 
            // Expected result:
            //     <p>&lt;a href=&quot;&quot;&quot;&gt;</p>
            
            ExecuteExampleTest(596, "Inlines - Raw HTML",
                "<a href=\"\\\"\">",
                "<p>&lt;a href=&quot;&quot;&quot;&gt;</p>");
        }
        // 
        // 
        // ## Hard line breaks
        // 
        // A line break (not in a code span or HTML tag) that is preceded
        // by two or more spaces and does not occur at the end of a block
        // is parsed as a [hard line break](@) (rendered
        // in HTML as a `<br />` tag):
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example597()
        {
            // Source:
            //     foo  
            //     baz
            // 
            // Expected result:
            //     <p>foo<br />
            //     baz</p>
            
            ExecuteExampleTest(597, "Inlines - Hard line breaks",
                "foo  \r\nbaz",
                "<p>foo<br />\r\nbaz</p>");
        }
        // 
        // 
        // For a more visible alternative, a backslash before the
        // [line ending] may be used instead of two spaces:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example598()
        {
            // Source:
            //     foo\
            //     baz
            // 
            // Expected result:
            //     <p>foo<br />
            //     baz</p>
            
            ExecuteExampleTest(598, "Inlines - Hard line breaks",
                "foo\\\r\nbaz",
                "<p>foo<br />\r\nbaz</p>");
        }
        // 
        // 
        // More than two spaces can be used:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example599()
        {
            // Source:
            //     foo       
            //     baz
            // 
            // Expected result:
            //     <p>foo<br />
            //     baz</p>
            
            ExecuteExampleTest(599, "Inlines - Hard line breaks",
                "foo       \r\nbaz",
                "<p>foo<br />\r\nbaz</p>");
        }
        // 
        // 
        // Leading spaces at the beginning of the next line are ignored:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example600()
        {
            // Source:
            //     foo  
            //          bar
            // 
            // Expected result:
            //     <p>foo<br />
            //     bar</p>
            
            ExecuteExampleTest(600, "Inlines - Hard line breaks",
                "foo  \r\n     bar",
                "<p>foo<br />\r\nbar</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example601()
        {
            // Source:
            //     foo\
            //          bar
            // 
            // Expected result:
            //     <p>foo<br />
            //     bar</p>
            
            ExecuteExampleTest(601, "Inlines - Hard line breaks",
                "foo\\\r\n     bar",
                "<p>foo<br />\r\nbar</p>");
        }
        // 
        // 
        // Line breaks can occur inside emphasis, links, and other constructs
        // that allow inline content:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example602()
        {
            // Source:
            //     *foo  
            //     bar*
            // 
            // Expected result:
            //     <p><em>foo<br />
            //     bar</em></p>
            
            ExecuteExampleTest(602, "Inlines - Hard line breaks",
                "*foo  \r\nbar*",
                "<p><em>foo<br />\r\nbar</em></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example603()
        {
            // Source:
            //     *foo\
            //     bar*
            // 
            // Expected result:
            //     <p><em>foo<br />
            //     bar</em></p>
            
            ExecuteExampleTest(603, "Inlines - Hard line breaks",
                "*foo\\\r\nbar*",
                "<p><em>foo<br />\r\nbar</em></p>");
        }
        // 
        // 
        // Line breaks do not occur inside code spans
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example604()
        {
            // Source:
            //     `code  
            //     span`
            // 
            // Expected result:
            //     <p><code>code span</code></p>
            
            ExecuteExampleTest(604, "Inlines - Hard line breaks",
                "`code  \r\nspan`",
                "<p><code>code span</code></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example605()
        {
            // Source:
            //     `code\
            //     span`
            // 
            // Expected result:
            //     <p><code>code\ span</code></p>
            
            ExecuteExampleTest(605, "Inlines - Hard line breaks",
                "`code\\\r\nspan`",
                "<p><code>code\\ span</code></p>");
        }
        // 
        // 
        // or HTML tags:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example606()
        {
            // Source:
            //     <a href="foo  
            //     bar">
            // 
            // Expected result:
            //     <p><a href="foo  
            //     bar"></p>
            
            ExecuteExampleTest(606, "Inlines - Hard line breaks",
                "<a href=\"foo  \r\nbar\">",
                "<p><a href=\"foo  \r\nbar\"></p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example607()
        {
            // Source:
            //     <a href="foo\
            //     bar">
            // 
            // Expected result:
            //     <p><a href="foo\
            //     bar"></p>
            
            ExecuteExampleTest(607, "Inlines - Hard line breaks",
                "<a href=\"foo\\\r\nbar\">",
                "<p><a href=\"foo\\\r\nbar\"></p>");
        }
        // 
        // 
        // Hard line breaks are for separating inline content within a block.
        // Neither syntax for hard line breaks works at the end of a paragraph or
        // other block element:
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example608()
        {
            // Source:
            //     foo\
            // 
            // Expected result:
            //     <p>foo\</p>
            
            ExecuteExampleTest(608, "Inlines - Hard line breaks",
                "foo\\",
                "<p>foo\\</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example609()
        {
            // Source:
            //     foo  
            // 
            // Expected result:
            //     <p>foo</p>
            
            ExecuteExampleTest(609, "Inlines - Hard line breaks",
                "foo  ",
                "<p>foo</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example610()
        {
            // Source:
            //     ### foo\
            // 
            // Expected result:
            //     <h3>foo\</h3>
            
            ExecuteExampleTest(610, "Inlines - Hard line breaks",
                "### foo\\",
                "<h3>foo\\</h3>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Hard line breaks")]
        public void Example611()
        {
            // Source:
            //     ### foo  
            // 
            // Expected result:
            //     <h3>foo</h3>
            
            ExecuteExampleTest(611, "Inlines - Hard line breaks",
                "### foo  ",
                "<h3>foo</h3>");
        }
        // 
        // 
        // ## Soft line breaks
        // 
        // A regular line break (not in a code span or HTML tag) that is not
        // preceded by two or more spaces or a backslash is parsed as a
        // softbreak.  (A softbreak may be rendered in HTML either as a
        // [line ending] or as a space. The result will be the same in
        // browsers. In the examples here, a [line ending] will be used.)
        // 
        [TestMethod]
        [TestCategory("Inlines - Soft line breaks")]
        public void Example612()
        {
            // Source:
            //     foo
            //     baz
            // 
            // Expected result:
            //     <p>foo
            //     baz</p>
            
            ExecuteExampleTest(612, "Inlines - Soft line breaks",
                "foo\r\nbaz",
                "<p>foo\r\nbaz</p>");
        }
        // 
        // 
        // Spaces at the end of the line and beginning of the next line are
        // removed:
        // 
        [TestMethod]
        [TestCategory("Inlines - Soft line breaks")]
        public void Example613()
        {
            // Source:
            //     foo 
            //      baz
            // 
            // Expected result:
            //     <p>foo
            //     baz</p>
            
            ExecuteExampleTest(613, "Inlines - Soft line breaks",
                "foo \r\n baz",
                "<p>foo\r\nbaz</p>");
        }
        // 
        // 
        // A conforming parser may render a soft line break in HTML either as a
        // line break or as a space.
        // 
        // A renderer may also provide an option to render soft line breaks
        // as hard line breaks.
        // 
        // ## Textual content
        // 
        // Any characters not given an interpretation by the above rules will
        // be parsed as plain textual content.
        // 
        [TestMethod]
        [TestCategory("Inlines - Textual content")]
        public void Example614()
        {
            // Source:
            //     hello $.;'there
            // 
            // Expected result:
            //     <p>hello $.;'there</p>
            
            ExecuteExampleTest(614, "Inlines - Textual content",
                "hello $.;'there",
                "<p>hello $.;'there</p>");
        }
        // 
        // 
        [TestMethod]
        [TestCategory("Inlines - Textual content")]
        public void Example615()
        {
            // Source:
            //     Foo χρῆν
            // 
            // Expected result:
            //     <p>Foo χρῆν</p>
            
            ExecuteExampleTest(615, "Inlines - Textual content",
                "Foo χρῆν",
                "<p>Foo χρῆν</p>");
        }
        // 
        // 
        // Internal spaces are preserved verbatim:
        // 
        [TestMethod]
        [TestCategory("Inlines - Textual content")]
        public void Example616()
        {
            // Source:
            //     Multiple     spaces
            // 
            // Expected result:
            //     <p>Multiple     spaces</p>
            
            ExecuteExampleTest(616, "Inlines - Textual content",
                "Multiple     spaces",
                "<p>Multiple     spaces</p>");
        }
        // 
        // 
        // <!-- END TESTS -->
        // 
        // # Appendix: A parsing strategy
        // 
        // In this appendix we describe some features of the parsing strategy
        // used in the CommonMark reference implementations.
        // 
        // ## Overview
        // 
        // Parsing has two phases:
        // 
        // 1. In the first phase, lines of input are consumed and the block
        // structure of the document---its division into paragraphs, block quotes,
        // list items, and so on---is constructed.  Text is assigned to these
        // blocks but not parsed. Link reference definitions are parsed and a
        // map of links is constructed.
        // 
        // 2. In the second phase, the raw text contents of paragraphs and headings
        // are parsed into sequences of Markdown inline elements (strings,
        // code spans, links, emphasis, and so on), using the map of link
        // references constructed in phase 1.
        // 
        // At each point in processing, the document is represented as a tree of
        // **blocks**.  The root of the tree is a `document` block.  The `document`
        // may have any number of other blocks as **children**.  These children
        // may, in turn, have other blocks as children.  The last child of a block
        // is normally considered **open**, meaning that subsequent lines of input
        // can alter its contents.  (Blocks that are not open are **closed**.)
        // Here, for example, is a possible document tree, with the open blocks
        // marked by arrows:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //          list_item
        //            paragraph
        //              "Qui *quodsi iracundia*"
        //       -> list_item
        //         -> paragraph
        //              "aliquando id"
        // ```
        // 
        // ## Phase 1: block structure
        // 
        // Each line that is processed has an effect on this tree.  The line is
        // analyzed and, depending on its contents, the document may be altered
        // in one or more of the following ways:
        // 
        // 1. One or more open blocks may be closed.
        // 2. One or more new blocks may be created as children of the
        //    last open block.
        // 3. Text may be added to the last (deepest) open block remaining
        //    on the tree.
        // 
        // Once a line has been incorporated into the tree in this way,
        // it can be discarded, so input can be read in a stream.
        // 
        // For each line, we follow this procedure:
        // 
        // 1. First we iterate through the open blocks, starting with the
        // root document, and descending through last children down to the last
        // open block.  Each block imposes a condition that the line must satisfy
        // if the block is to remain open.  For example, a block quote requires a
        // `>` character.  A paragraph requires a non-blank line.
        // In this phase we may match all or just some of the open
        // blocks.  But we cannot close unmatched blocks yet, because we may have a
        // [lazy continuation line].
        // 
        // 2.  Next, after consuming the continuation markers for existing
        // blocks, we look for new block starts (e.g. `>` for a block quote.
        // If we encounter a new block start, we close any blocks unmatched
        // in step 1 before creating the new block as a child of the last
        // matched block.
        // 
        // 3.  Finally, we look at the remainder of the line (after block
        // markers like `>`, list markers, and indentation have been consumed).
        // This is text that can be incorporated into the last open
        // block (a paragraph, code block, heading, or raw HTML).
        // 
        // Setext headings are formed when we see a line of a paragraph
        // that is a [setext heading underline].
        // 
        // Reference link definitions are detected when a paragraph is closed;
        // the accumulated text lines are parsed to see if they begin with
        // one or more reference link definitions.  Any remainder becomes a
        // normal paragraph.
        // 
        // We can see how this works by considering how the tree above is
        // generated by four lines of Markdown:
        // 
        // ``` markdown
        // > Lorem ipsum dolor
        // sit amet.
        // > - Qui *quodsi iracundia*
        // > - aliquando id
        // ```
        // 
        // At the outset, our document model is just
        // 
        // ``` tree
        // -> document
        // ```
        // 
        // The first line of our text,
        // 
        // ``` markdown
        // > Lorem ipsum dolor
        // ```
        // 
        // causes a `block_quote` block to be created as a child of our
        // open `document` block, and a `paragraph` block as a child of
        // the `block_quote`.  Then the text is added to the last open
        // block, the `paragraph`:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //     -> paragraph
        //          "Lorem ipsum dolor"
        // ```
        // 
        // The next line,
        // 
        // ``` markdown
        // sit amet.
        // ```
        // 
        // is a "lazy continuation" of the open `paragraph`, so it gets added
        // to the paragraph's text:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //     -> paragraph
        //          "Lorem ipsum dolor\nsit amet."
        // ```
        // 
        // The third line,
        // 
        // ``` markdown
        // > - Qui *quodsi iracundia*
        // ```
        // 
        // causes the `paragraph` block to be closed, and a new `list` block
        // opened as a child of the `block_quote`.  A `list_item` is also
        // added as a child of the `list`, and a `paragraph` as a child of
        // the `list_item`.  The text is then added to the new `paragraph`:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //       -> list_item
        //         -> paragraph
        //              "Qui *quodsi iracundia*"
        // ```
        // 
        // The fourth line,
        // 
        // ``` markdown
        // > - aliquando id
        // ```
        // 
        // causes the `list_item` (and its child the `paragraph`) to be closed,
        // and a new `list_item` opened up as child of the `list`.  A `paragraph`
        // is added as a child of the new `list_item`, to contain the text.
        // We thus obtain the final tree:
        // 
        // ``` tree
        // -> document
        //   -> block_quote
        //        paragraph
        //          "Lorem ipsum dolor\nsit amet."
        //     -> list (type=bullet tight=true bullet_char=-)
        //          list_item
        //            paragraph
        //              "Qui *quodsi iracundia*"
        //       -> list_item
        //         -> paragraph
        //              "aliquando id"
        // ```
        // 
        // ## Phase 2: inline structure
        // 
        // Once all of the input has been parsed, all open blocks are closed.
        // 
        // We then "walk the tree," visiting every node, and parse raw
        // string contents of paragraphs and headings as inlines.  At this
        // point we have seen all the link reference definitions, so we can
        // resolve reference links as we go.
        // 
        // ``` tree
        // document
        //   block_quote
        //     paragraph
        //       str "Lorem ipsum dolor"
        //       softbreak
        //       str "sit amet."
        //     list (type=bullet tight=true bullet_char=-)
        //       list_item
        //         paragraph
        //           str "Qui "
        //           emph
        //             str "quodsi iracundia"
        //       list_item
        //         paragraph
        //           str "aliquando id"
        // ```
        // 
        // Notice how the [line ending] in the first paragraph has
        // been parsed as a `softbreak`, and the asterisks in the first list item
        // have become an `emph`.
        // 
        // ### An algorithm for parsing nested emphasis and links
        // 
        // By far the trickiest part of inline parsing is handling emphasis,
        // strong emphasis, links, and images.  This is done using the following
        // algorithm.
        // 
        // When we're parsing inlines and we hit either
        // 
        // - a run of `*` or `_` characters, or
        // - a `[` or `![`
        // 
        // we insert a text node with these symbols as its literal content, and we
        // add a pointer to this text node to the [delimiter stack](@).
        // 
        // The [delimiter stack] is a doubly linked list.  Each
        // element contains a pointer to a text node, plus information about
        // 
        // - the type of delimiter (`[`, `![`, `*`, `_`)
        // - the number of delimiters,
        // - whether the delimiter is "active" (all are active to start), and
        // - whether the delimiter is a potential opener, a potential closer,
        //   or both (which depends on what sort of characters precede
        //   and follow the delimiters).
        // 
        // When we hit a `]` character, we call the *look for link or image*
        // procedure (see below).
        // 
        // When we hit the end of the input, we call the *process emphasis*
        // procedure (see below), with `stack_bottom` = NULL.
        // 
        // #### *look for link or image*
        // 
        // Starting at the top of the delimiter stack, we look backwards
        // through the stack for an opening `[` or `![` delimiter.
        // 
        // - If we don't find one, we return a literal text node `]`.
        // 
        // - If we do find one, but it's not *active*, we remove the inactive
        //   delimiter from the stack, and return a literal text node `]`.
        // 
        // - If we find one and it's active, then we parse ahead to see if
        //   we have an inline link/image, reference link/image, compact reference
        //   link/image, or shortcut reference link/image.
        // 
        //   + If we don't, then we remove the opening delimiter from the
        //     delimiter stack and return a literal text node `]`.
        // 
        //   + If we do, then
        // 
        //     * We return a link or image node whose children are the inlines
        //       after the text node pointed to by the opening delimiter.
        // 
        //     * We run *process emphasis* on these inlines, with the `[` opener
        //       as `stack_bottom`.
        // 
        //     * We remove the opening delimiter.
        // 
        //     * If we have a link (and not an image), we also set all
        //       `[` delimiters before the opening delimiter to *inactive*.  (This
        //       will prevent us from getting links within links.)
        // 
        // #### *process emphasis*
        // 
        // Parameter `stack_bottom` sets a lower bound to how far we
        // descend in the [delimiter stack].  If it is NULL, we can
        // go all the way to the bottom.  Otherwise, we stop before
        // visiting `stack_bottom`.
        // 
        // Let `current_position` point to the element on the [delimiter stack]
        // just above `stack_bottom` (or the first element if `stack_bottom`
        // is NULL).
        // 
        // We keep track of the `openers_bottom` for each delimiter
        // type (`*`, `_`).  Initialize this to `stack_bottom`.
        // 
        // Then we repeat the following until we run out of potential
        // closers:
        // 
        // - Move `current_position` forward in the delimiter stack (if needed)
        //   until we find the first potential closer with delimiter `*` or `_`.
        //   (This will be the potential closer closest
        //   to the beginning of the input -- the first one in parse order.)
        // 
        // - Now, look back in the stack (staying above `stack_bottom` and
        //   the `openers_bottom` for this delimiter type) for the
        //   first matching potential opener ("matching" means same delimiter).
        // 
        // - If one is found:
        // 
        //   + Figure out whether we have emphasis or strong emphasis:
        //     if both closer and opener spans have length >= 2, we have
        //     strong, otherwise regular.
        // 
        //   + Insert an emph or strong emph node accordingly, after
        //     the text node corresponding to the opener.
        // 
        //   + Remove any delimiters between the opener and closer from
        //     the delimiter stack.
        // 
        //   + Remove 1 (for regular emph) or 2 (for strong emph) delimiters
        //     from the opening and closing text nodes.  If they become empty
        //     as a result, remove them and remove the corresponding element
        //     of the delimiter stack.  If the closing node is removed, reset
        //     `current_position` to the next element in the stack.
        // 
        // - If none in found:
        // 
        //   + Set `openers_bottom` to the element before `current_position`.
        //     (We know that there are no openers for this kind of closer up to and
        //     including this point, so this puts a lower bound on future searches.)
        // 
        //   + If the closer at `current_position` is not a potential opener,
        //     remove it from the delimiter stack (since we know it can't
        //     be a closer either).
        // 
        //   + Advance `current_position` to the next element in the stack.
        // 
        // After we're done, we remove all delimiters above `stack_bottom` from the
        // delimiter stack.
        // 
    }
}
