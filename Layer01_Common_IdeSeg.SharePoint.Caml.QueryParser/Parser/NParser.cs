#region Copyright(c) Carlos Segura Sanz - www.ideseg.com

// Copyright (c) 2009 Carlos Segura Sanz - www.ideseg.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System.Globalization;
using Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST;
using Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST.Base;
using Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.LexScanner;

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.Parser
{
    /// <summary>
    /// Parser
    /// Analyzing a sequence of tokens to determine its grammatical structure 
    /// with respect to a given formal grammar. (see rules)
    /// </summary>
    public class NParser
    {
        private readonly ASTNodeFactoryBase _astFactory;
        private readonly Scanner _scanner;
        private int _parenthesisDepth;
        private Token _token;

        /// <summary>
        /// Initializes a new instance of the <see cref="NParser"/> class.
        /// </summary>
        /// <param name="textQuery">The parser output.</param>
        public NParser(string textQuery)
        {
            _scanner = new Scanner(textQuery);
            _astFactory = new ASTNodeFactory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NParser"/> class.
        /// </summary>
        /// <param name="textQuery">The text query.</param>
        /// <param name="astFactory">The ast factory.</param>
        public NParser(string textQuery, ASTNodeFactoryBase astFactory)
            : this(textQuery)
        {
            _astFactory = astFactory;
        }

        /// <summary>
        /// Parser
        /// </summary>
        /// <returns>An AST</returns>
        public Query Parse()
        {
            Where where = null;
            OrderBy orderBy = null;
            GroupBy groupBy = null;

            _token = _scanner.GetToken();

            if (_token.Ttype == TokenType.WHERE)
            {
                where = WhereRule();
            }
            if (_token.Ttype == TokenType.ORDERBY)
            {
                orderBy = OrderByRule();
            }
            if (_token.Ttype == TokenType.GROUPBY)
            {
                groupBy = GroupByRule();
            }

            Sequence sequence =
                _astFactory.CreateSequence(where,
                                           _astFactory.CreateSequence(orderBy,
                                                                      _astFactory.CreateSequence(groupBy, null)));

            Query expression = _astFactory.CreateQuery(sequence);

            return expression;
        }

        /// <summary>
        /// WHERE expression Rule
        ///  | BooleanExpression
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParserException">Parenthesis mismath!!!</exception>
        private Where WhereRule()
        {
            Expression expression = BooleanOperatorRule();

            if (_parenthesisDepth != 0)
            {
                throw new ParserException("Parenthesis mismath!!!");
            }

            return _astFactory.CreateWhere(expression);
        }


        /// <summary>
        /// BOOLEAN Operation expression Rule
        ///  | BooleanRule
        ///  | BooleanRule AND BooleanOperatorRule
        ///  | BooleanRule OR BooleanOperatorRule
        /// <returns></returns>
        private Expression BooleanOperatorRule()
        {
            Expression booleanExp = BooleanRule();

            _token = _scanner.GetToken();

            switch (_token.Ttype)
            {
                case TokenType.AND:
                    return _astFactory.CreateBooleanAnd(booleanExp,
                                                        BooleanOperatorRule());
                case TokenType.OR:
                    return _astFactory.CreateBooleanOr(booleanExp,
                                                       BooleanOperatorRule());
                case TokenType.RIGHT_PARENTHESIS:
                    _parenthesisDepth--;
                    break;
            }

            return booleanExp;
        }


        /// <summary>
        /// BOOLEAN expression Rule
        ///  | '(' BooleanRule ')'
        ///  | OperationRule
        /// <returns></returns>
        private Expression BooleanRule()
        {
            _token = _scanner.GetToken();

            switch (_token.Ttype)
            {
                case TokenType.LEFT_PARENTHESIS:
                    _parenthesisDepth++;
                    return BooleanOperatorRule();

                case TokenType.FIELD:
                    FieldNode fieldNode = _astFactory.CreateFieldNode(_token.Value);
                    return _astFactory.CreateExpression(ComparationRule(fieldNode));

                default:
                    throw new ParserException("Field expected.");
            }
        }


        /// <summary>
        /// COMPARATION Expression Rule
        ///  | FieldRule COMPARATOR ValueRule
        /// </summary>
        /// <returns>AST Operation Tree</returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private Operation ComparationRule(FieldNode field)
        {
            _token = _scanner.GetToken();

            switch (_token.Ttype)
            {
                case TokenType.LESS:
                    return _astFactory.CreateComparationLess(field,
                                                             ValueRule());
                case TokenType.GREATER:
                    return _astFactory.CreateComparationGreater(field,
                                                                ValueRule());
                case TokenType.LESS_EQ:
                    return _astFactory.CreateComparationLessEqual(field,
                                                                  ValueRule());
                case TokenType.GREATER_EQ:
                    return _astFactory.CreateComparationGraterEqual(field,
                                                                    ValueRule());
                case TokenType.EQ:
                    return IsNullOrEqualRule(field);
                case TokenType.NOT_EQ:
                    return IsNotNullOrNotEqualRule(field);
                case TokenType.IS:
                    return IsNullOrNotIsNullRule(field);

                case TokenType.LIKE:
                    return _astFactory.CreateComparationContains(field, ValueRule());

                case TokenType.BEGINS:
                    return _astFactory.CreateComparationBegin(field, ValueRule());

                default:
                    throw new ParserException(
                        string.Format(CultureInfo.InvariantCulture,
                                      "Invalid Operation at {0}",
                                      _scanner.CurrentPosition));
            }
        }

        /// <summary>
        /// Determines the correct oepration.
        /// If the value token after an Equal, is null the correct 
        /// operation is IsNull
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The operation</returns>
        private Operation IsNullOrEqualRule(FieldNode field)
        {
            ValueNode valueNode = ValueRule();

            if (valueNode == null)
            {
                return _astFactory.CreateComparationIsNull(field);
            }

            return _astFactory.CreateComparationEqual(field, valueNode);
        }

        /// <summary>
        /// Determines the correct oepration.
        /// If the value token after a distinct operator, is null the correct 
        /// operation is IsNotNull
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>The operation</returns>
        private Operation IsNotNullOrNotEqualRule(FieldNode field)
        {
            ValueNode valueNode = ValueRule();

            // TODO: Introduce a class for Null Values
            if (valueNode == null)
            {
                return _astFactory.CreateComparationIsNotNull(field);
            }

            return _astFactory.CreateComparationNotEqual(field, valueNode);
        }

        private Operation IsNullOrNotIsNullRule(FieldNode field)
        {
            _token = _scanner.GetToken();
            switch (_token.Ttype)
            {
                case TokenType.NULL:
                    return _astFactory.CreateComparationIsNull(field);
                case TokenType.NOT:
                    _token = _scanner.GetToken();
                    if (_token.Ttype == TokenType.NULL)
                    {
                        return _astFactory.CreateComparationIsNotNull(field);
                    }
                    throw new ParserException(
                        string.Format(CultureInfo.InvariantCulture,
                                      "Missing NULL at {0}",
                                      _scanner.CurrentPosition));
                default:
                    throw new ParserException(
                        string.Format(CultureInfo.InvariantCulture,
                                      "Missing NULL at {0}",
                                      _scanner.CurrentPosition));
            }
        }


        /// <summary>
        /// GROUPBY
        ///     | FieldListRule
        /// </summary>
        /// <returns>AST GroupBy Tree</returns>
        private GroupBy GroupByRule()
        {
            return _astFactory.CreateGroupBy(FieldListRule());
        }

        /// <summary>
        /// FIELDLIST
        ///     | FieldNode
        ///     | FieldNode, FIELDLIST
        /// </summary>
        /// <returns>AST FieldList Tree</returns>
        private FieldList FieldListRule()
        {
            FieldNode fieldNode = FieldRule();
            _token = _scanner.GetToken();

            if (_token.Ttype == TokenType.COMMA)
            {
                return _astFactory.CreateFieldList(fieldNode, FieldListRule());
            }

            return _astFactory.CreateFieldList(fieldNode);
        }


        /// <summary>
        /// ORDERBY
        ///  | FieldList
        /// </summary>
        /// <returns>AST Orderby Tree</returns>
        private OrderBy OrderByRule()
        {
            return _astFactory.CreateOrderBy(FieldsListOrderRule());
        }

        /// <summary>
        /// FIELDLISTORDER
        ///     | FieldNodeOrder
        ///     | FieldNodeOrder, FIELDLISTORDER
        /// </summary>
        /// <returns>AST FieldList Tree</returns>
        private FieldList FieldsListOrderRule()
        {
            FieldNode fieldNodeWithOrder = FieldNodeOrderRule();

            _token = _scanner.GetToken();

            if (_token.Ttype == TokenType.COMMA)
            {
                return _astFactory.CreateFieldList(fieldNodeWithOrder,
                                                   FieldsListOrderRule());
            }

            return _astFactory.CreateFieldList(fieldNodeWithOrder);
        }


        /// <summary>
        /// FIELDNODEORDER
        ///     | FieldNode DESC
        ///     | FieldNode ASC
        ///     | FieldNode
        /// </summary>
        /// <returns>AST FieldNode</returns>
        private FieldNode FieldNodeOrderRule()
        {
            FieldNode fieldNode = FieldRule();

            _token = _scanner.GetToken();

            switch (_token.Ttype)
            {
                case TokenType.DESC:
                    fieldNode.Ascending = false;
                    return fieldNode;
                case TokenType.ASC:
                    fieldNode.Ascending = true;
                    return fieldNode;
                default:
                    _scanner.BackToken();
                    break;
            }

            return fieldNode;
        }

        /// <summary>
        /// FIELD expression Rule
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private FieldNode FieldRule()
        {
            _token = _scanner.GetToken();

            if (_token.Ttype == TokenType.FIELD)
            {
                FieldNode fieldNode = _astFactory.CreateFieldNode(_token.Value);
                return fieldNode;
            }

            throw new ParserException(
                string.Format(CultureInfo.InvariantCulture,
                              "Field name expected at {0}",
                              _scanner.CurrentPosition));
        }

        /// <summary>
        /// VALUE expression Rule
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private ValueNode ValueRule()
        {
            _token = _scanner.GetToken();

            if (_token.Ttype == TokenType.VALUE)
            {
                return _astFactory.CreateValueNode(_token.Value,
                                                   _token.ValueType.ToString());
            }

            if (_token.Ttype == TokenType.NULL)
            {
                return null;
            }

            throw new ParserException(
                string.Format(CultureInfo.InvariantCulture,
                              "TokenValue expected at {0}",
                              _scanner.CurrentPosition));
        }
    }
}