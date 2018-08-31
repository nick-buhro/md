using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NickBuhro.Markdown
{
    public interface ICompiler
    {
        void Compile(TextReader source, TextWriter writer);
    }
}
