using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NickBuhro.Markdown.Tests.SpecificationParser.Lexing
{
    internal sealed class LexerEnumerator: IEnumerator<Token>
    {
        private static class Const
        {
            public const string Header1Prefix = "# ";
            public const string Header2Prefix = "## ";
            public const string ExampleStartLine = "```````````````````````````````` example";
            public const string ExampleDelimLien = ".";
            public const string ExampleEndLine = "````````````````````````````````";
        }
        
        private readonly Action[] _scanStrategies;
        private readonly StringReader _sr;
        private bool _disposed;
        private bool _eof;

        private string _currentSourceLine;
        private Action _currentScanStrategy;
        

        public LexerEnumerator(string source)
        {
            _sr = new StringReader(source);

            _scanStrategies = new Action[]
            {
                ScanLineFromTextContext,
                ScanLineFromMarkdownContext,
                ScanLineFromHtmlContext
            };
            _currentScanStrategy = _scanStrategies[0];
        }

        public void Dispose()
        {
            if (!_disposed && (_sr != null))
            {
                _sr.Dispose();
                _disposed = true;
            }
        }

#region IEnumerator impl

        public Token Current { get; private set; }
        object IEnumerator.Current => Current;

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public bool MoveNext()
        {
            if (_disposed) throw new ObjectDisposedException("LexerEnumerator");
            if (_eof) return false;

            _currentSourceLine = _sr.ReadLine();
            if (_currentSourceLine == null)
            {
                _eof = true;
                return false;
            }
            
            _currentScanStrategy();
            return true;
        }

#endregion
        
        private void ScanLineFromTextContext()
        {
            if (_currentSourceLine.StartsWith(Const.Header1Prefix))
            {
                var value = _currentSourceLine.Substring(Const.Header1Prefix.Length);
                Current = new Token(TokenType.Header1, _currentSourceLine, value);
                return;
            }

            if (_currentSourceLine.StartsWith(Const.Header2Prefix))
            {
                var value = _currentSourceLine.Substring(Const.Header2Prefix.Length);
                Current = new Token(TokenType.Header2, _currentSourceLine, value);
                return;
            }

            if (_currentSourceLine == Const.ExampleStartLine)
            {
                Current = new Token(TokenType.ExampleStart, _currentSourceLine);
                _currentScanStrategy = _scanStrategies[1];
                return;
            }

            Current = new Token(TokenType.TextLine, _currentSourceLine);
        }

        private void ScanLineFromMarkdownContext()
        {
            if (_currentSourceLine == Const.ExampleDelimLien)
            {
                Current = new Token(TokenType.ExampleDelim, _currentSourceLine);
                _currentScanStrategy = _scanStrategies[2];
                return;
            }
            
            Current = new Token(TokenType.ExampleMarkdownLine, _currentSourceLine);
        }

        private void ScanLineFromHtmlContext()
        {
            if (_currentSourceLine == Const.ExampleEndLine)
            {
                Current = new Token(TokenType.ExampleEnd, _currentSourceLine);
                _currentScanStrategy = _scanStrategies[0];
                return;
            }

            Current = new Token(TokenType.ExampleHtmlLine, _currentSourceLine);
        }
    }
}
