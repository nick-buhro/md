using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Generating
{
    internal sealed class CustomStringBuilder
    {
        private readonly StringBuilder _sb = new StringBuilder();

        private bool _newLine;

        public string IndentString { get; set; }

        public int IndentLevel { get; set; }


        public CustomStringBuilder()
        {
            IndentString = "    ";
            _newLine = true;
        }


        public void Write(string value)
        {
            if (_newLine)
            {
                WriteIndent();
                _newLine = false;
            }
            _sb.Append(value);
        }

        public void WriteLine(string value = null)
        {
            if (_newLine)
            {
                WriteIndent();
                _newLine = false;
            }
            _sb.AppendLine(value);
            _newLine = true;
        }
        
        public override string ToString()
        {
            return _sb.ToString();
        }


        private void WriteIndent()
        {
            for (var i = 0; i < IndentLevel; i++)
                _sb.Append(IndentString);
        }
    }
}
