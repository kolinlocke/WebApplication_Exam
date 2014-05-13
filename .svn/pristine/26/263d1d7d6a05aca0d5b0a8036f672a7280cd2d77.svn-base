#region Copyright(c) Carlos Segura Sanz - www.ideseg.com

// Copyright (c) 2009 Carlos Segura Sanz - www.ideseg.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.LexScanner
{
    public class Scanner : ScannerBase
    {
        private const string STOP_CHARS = "@(),!=><&|";

        private readonly Dictionary<string, TokenType> _operators =
            new Dictionary<string, TokenType>
                {
                    {
                        "||", TokenType.OR
                        },
                    {
                        "&&", TokenType.AND
                        },
                    {
                        "<", TokenType.LESS
                        },
                    {
                        "<=", TokenType.LESS_EQ
                        },
                    {
                        ">", TokenType.GREATER
                        },
                    {
                        ">=", TokenType.GREATER_EQ
                        },
                    {
                        "=", TokenType.EQ
                        },
                    {
                        "==", TokenType.EQ
                        },
                    {
                        "<>", TokenType.NOT_EQ
                        },
                    {
                        "!=", TokenType.NOT_EQ
                        },
                    {
                        ",", TokenType.COMMA
                        },
                    {
                        "(", TokenType.LEFT_PARENTHESIS
                        },
                    {
                        ")", TokenType.RIGHT_PARENTHESIS
                        }
                };

        private readonly Dictionary<string, TokenType> _reservedWords =
            new Dictionary<string, TokenType>
                {
                    {
                        "LIKE", TokenType.LIKE
                        },
                    {
                        "FALSE", TokenType.FALSE
                        },
                    {
                        "GROUPBY", TokenType.GROUPBY
                        },
                    {
                        "IS", TokenType.IS
                        },
                    {
                        "NOT", TokenType.NOT
                        },
                    {
                        "NULL", TokenType.NULL
                        },
                    {
                        "TRUE", TokenType.TRUE
                        },
                    {
                        "ORDERBY", TokenType.ORDERBY
                        },
                    {
                        "WHERE", TokenType.WHERE
                        },
                    {
                        "AND", TokenType.AND
                        },
                    {
                        "OR", TokenType.OR
                        },
                    {
                        "ASC", TokenType.ASC
                        },
                    {
                        "DESC", TokenType.DESC
                        },
                    { 
                        "BEGINS", TokenType.BEGINS 
                        },
                };

        private readonly TokenFactory _tokenFactory;
        private bool _inBracket;
        private bool _inQuotes;
        private bool _inSharp;
        private Token _token;

        public Scanner(string input) : base(input)
        {
            _tokenFactory = new TokenFactory();
        }

        /// <summary>
        /// Checks the correct brackets and quotes.
        /// </summary>
        /// <exception cref="ScannerException"><c>LexerException</c>.</exception>
        protected override void CheckCorrectBracketsAndQuotes()
        {
            if (_inQuotes)
            {
                throw new ScannerException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Quote expected at {0}", CurrentPosition));
            }
            if (_inBracket)
            {
                throw new ScannerException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Bracket expected at {0}", CurrentPosition));
            }
            if (_inSharp)
            {
                throw new ScannerException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Sharp expected at {0}", CurrentPosition));
            }
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <returns>The token Type</returns>
        /// <exception cref="ScannerException"><c>LexerException</c>.</exception>
        public override Token GetToken()
        {
            SaveScannerState();

            EatSpaces();

            if (EndOfLine())
            {
                return new Token(TokenType.EOL);
            }

            MarkStartOfToken();

            if (char.IsLetter(CurrentChar))
            {
                _token = ScanReservedWordOrSymbol();
            }
            else if (Char.IsDigit(CurrentChar))
            {
                _token = ScanNumber();
            }
            else if (CurrentChar == '[')
            {
                _token = ScanBracketSymbol();
            }
            else if (CurrentChar == '"')
            {
                _token = ScanString();
            }
            else if (CurrentChar == '#')
            {
                _token = ScanDate();
            }
            else if (STOP_CHARS.IndexOf(CurrentChar) != -1 && !_inQuotes)
            {
                _token = ScanOperator();
            }

            if (_token == null)
            {
                throw new ScannerException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Unknow character at {0}", CurrentPosition));
            }

            SetScannerStateToken(_token);

            return _token;
        }

        /// <summary>
        /// Scans the reserved word or symbol.
        /// </summary>
        /// <returns>TokenType</returns>
        private Token ScanReservedWordOrSymbol()
        {
            string word = string.Empty;

            while (!EndOfLine() && char.IsLetterOrDigit(CurrentChar))
            {
                word += CharMoveNext;
            }

            if (_reservedWords.ContainsKey(word.ToUpper(CultureInfo.CurrentCulture)))
            {
                return _tokenFactory.CreateReservedWord(_reservedWords[word.ToUpper(CultureInfo.InvariantCulture)]);
            }

            return new Token(TokenType.FIELD, TokenValueType.Identifier, word);
        }

        /// <summary>
        /// Scans the bracket symbol.
        /// </summary>
        /// <returns>TokenType.FIELD</returns>
        private Token ScanBracketSymbol()
        {
            string words = string.Empty;

            _inBracket = true;

            SkipChar();

            while (!EndOfLine() && CurrentChar != ']')
            {
                words += CharMoveNext;
            }

            SkipChar();

            _inBracket = false;

            return _tokenFactory.CreateField(words);
        }

        /// <summary>
        /// Scans a number.
        /// </summary>
        /// <returns>TokenType.VALUE</returns>
        private Token ScanNumber()
        {
            string number = string.Empty;

            while (!EndOfLine()
                   && (char.IsDigit(CurrentChar)
                       || CurrentChar == '.'
                       || CurrentChar == ','))
            {
                number += CharMoveNext;
            }

            return _tokenFactory.CreateNumber(number);
        }

        /// <summary>
        /// Scans a string.
        /// </summary>
        /// <returns>TokenType.VALUE</returns>
        private Token ScanString()
        {
            string words = string.Empty;

            _inQuotes = true;

            SkipChar();

            while (!EndOfLine() && CurrentChar != '"')
            {
                words += CharMoveNext;
            }

            SkipChar();

            _inQuotes = false;

            return _tokenFactory.CreateString(words);
        }

        /// <summary>
        /// Scans a Date.
        /// </summary>
        /// <returns>TokenType.VALUE</returns>
        private Token ScanDate()
        {
            string date = string.Empty;

            _inSharp = true;

            SkipChar();

            while (!EndOfLine() && CurrentChar != '#')
            {
                date += CharMoveNext;
            }

            SkipChar();

            _inSharp = false;

            return _tokenFactory.CreateDate(date);
        }

        /// <summary>
        /// Scans an operator.
        /// </summary>
        /// <returns>TokenType with the operator value</returns>
        private Token ScanOperator()
        {
            string oper = string.Empty;

            while (!EndOfLine()
                   && STOP_CHARS.IndexOf(CurrentChar) != -1)
            {
                if (CurrentChar == '(' || CurrentChar == ')')
                {
                    oper += CharMoveNext;
                    break;
                }

                oper += CharMoveNext;
            }

            if (!_operators.ContainsKey(oper))
            {
                throw new ScannerException(
                    string.Format(CultureInfo.CurrentCulture,
                                  "Illegal operator {0}", oper));
            }

            return _tokenFactory.CreateOperator(_operators[oper]);
        }
    }
}