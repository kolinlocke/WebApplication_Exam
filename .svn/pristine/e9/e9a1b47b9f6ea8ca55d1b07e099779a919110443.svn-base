#region Copyright(c) Carlos Segura Sanz - www.ideseg.com

// Copyright (c) 2009 Carlos Segura Sanz - www.ideseg.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.LexScanner
{
    public abstract class ScannerBase
    {
        private readonly string _input;
        private ScannerState _currentState;
        private ScannerState _prevoiusState;

        protected ScannerBase(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ScannerException("Input can´t be null or empty");
            }

            _input = input;
            _currentState = new ScannerState();
        }


        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>The current position.</value>
        public int CurrentPosition
        {
            get { return _currentState.CurrentPosition; }
        }

        /// <summary>
        /// Gets the start position.
        /// </summary>
        /// <value>The start position.</value>
        public int StartPosition
        {
            get { return _currentState.StartPosition; }
        }

        /// <summary>
        /// Gets the current char.
        /// </summary>
        /// <value>The current char.</value>
        protected char CurrentChar
        {
            get { return _input[CurrentPosition]; }
        }

        /// <summary>
        /// Gets the char and move to the next.
        /// </summary>
        /// <returns>The current char</returns>
        protected char CharMoveNext
        {
            get
            {
                char currentChar = _input[_currentState.CurrentPosition];
                SkipChar();
                return currentChar;
            }
        }

        /// <summary>
        /// Eats spaces.
        /// </summary>
        protected void EatSpaces()
        {
            while (!EndOfLine() && CurrentChar == ' ')
            {
                SkipChar();
            }
        }

        /// <summary>
        /// Skips the char.
        /// </summary>
        /// <exception cref="ScannerException"><c>LexerException</c>.</exception>
        protected void SkipChar()
        {
            if (!EndOfLine())
            {
                _currentState.CurrentPosition++;
            }
        }

        /// <summary>
        /// Check for the end of line
        /// </summary>
        /// <returns>True if EOL</returns>
        /// <exception cref="ScannerException"><c>LexerException</c>.</exception>
        protected bool EndOfLine()
        {
            if (_currentState.CurrentPosition + 1 > _input.Length)
            {
                CheckCorrectBracketsAndQuotes();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Marks the start of token.
        /// </summary>
        protected void MarkStartOfToken()
        {
            _currentState.StartPosition = _currentState.CurrentPosition;
        }

        /// <summary>
        /// Checks the correct brackets and quotes.
        /// </summary>
        /// <exception cref="ScannerException"><c>LexerException</c>.</exception>
        protected abstract void CheckCorrectBracketsAndQuotes();

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <returns>The TokenType</returns>
        public abstract Token GetToken();

        /// <summary>
        /// Saves the state of the scanner.
        /// </summary>
        protected void SaveScannerState()
        {
            _prevoiusState = new ScannerState(_currentState);
        }

        /// <summary>
        /// Restores the state of the scanner.
        /// </summary>
        protected void RestoreScannerState()
        {
            _currentState = _prevoiusState;
        }

        /// <summary>
        /// Sets the scanner state token.
        /// </summary>
        /// <param name="lastToken">The last token.</param>
        protected void SetScannerStateToken(Token lastToken)
        {
            _currentState.ReadToken = lastToken;
        }

        public void BackToken()
        {
            RestoreScannerState();
        }
    }
}