#region Copyright(c) Carlos Segura Sanz, All right reserved.

//  -----------------------------------------------------------------------------
//  Copyright(c) 2008-2009 Carlos Segura Sanz, All right reserved.
// 
//  csegura@ideseg.com
//  http://www.ideseg.com
// 
//     * Attribution. You must attribute the work in the manner specified by the author or
//        licensor (but not in any way that suggests that they endorse you or your use of the
//        work).
// 
//     * Noncommercial. You may not use this work for commercial purposes.
// 
//     * No Derivative Works. You may not alter, transform, or build upon this work without        author authorization
//     * For any reuse or distribution, you must make clear to others the license terms of this           work. The best way to do this is contact with author.
//     * Any of the above conditions can be waived if you get permission from the copyright            holder.
//     * Nothing in this license impairs or restricts the author's moral rights.
// 
//  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED
//  WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
//  MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
//  EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
//  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED  TO,  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF  LIABILITY,  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING   NEGLIGENCE OR  OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS  SOFTWARE, EVEN IF  ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//  -----------------------------------------------------------------------------

#endregion

using System.Diagnostics;
using IdeSeg.SharePoint.Caml.QueryParser.AST;
using IdeSeg.SharePoint.Caml.QueryParser.AST.Base;
using IdeSeg.SharePoint.Caml.QueryParser.LexScanner;

namespace IdeSeg.SharePoint.Caml.QueryParser.Parser
{
    /// <summary>
    /// Parser
    /// Analyzing a sequence of tokens to determine its grammatical structure 
    /// with respect to a given formal grammar. (see rules)
    /// </summary>
    public class NParser
    {
        private readonly Scanner _scanner;
        private readonly ASTNodeCAMLFactory _astFactory;
        private int _parenthesisDepth;

        /// <summary>
        /// Initializes a new instance of the <see cref="NParser"/> class.
        /// </summary>
        /// <param name="textQuery">The parser output.</param>
        public NParser(string textQuery)
        {
            _scanner = new Scanner(textQuery);
            _astFactory = new ASTNodeCAMLFactory();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NParser"/> class.
        /// </summary>
        /// <param name="textQuery">The text query.</param>
        /// <param name="astFactory">The ast factory.</param>
        public NParser(string textQuery, ASTNodeCAMLFactory astFactory)
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

            if (_scanner.GetTokenType() == TokenType.WHERE)
            {
                where = WhereExpressionRule();
            }
            if (_scanner.LastToken == TokenType.ORDERBY)
            {
                orderBy = OrderByExpressionRule();
            }
            if (_scanner.LastToken == TokenType.GROUPBY)
            {
                groupBy = GroupByExpressionRule();
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
        public Where WhereExpressionRule()
        {
            BooleanExp booleanExp = BooleanExpressionRule();
            
            if (_parenthesisDepth != 0)
            {
                Debug.WriteLine(_parenthesisDepth);
                //throw new ParserException("Parenthesis mismath!!!");
            }

            return _astFactory.CreateWhere(booleanExp);
        }

             
        /// <summary>
        /// BOOLEAN expression Rule
        ///  | '(' BooleanExpressionRule ')'
        ///  | SingleExpressionRule
        ///  | SingleExpressionRule AND BooleanExpressionRule
        ///  | SingleExpressionRule OR BooleanExpressionRule
        /// </summary>
        /// <returns></returns>
        public BooleanExp BooleanExpressionRule()
        {

            switch (_scanner.GetTokenType())
            {
                case TokenType.LEFT_PARENTHESIS:
                    _parenthesisDepth++;
                    return _astFactory.CreateSingleExpression(BooleanExpressionRule());

                case TokenType.RIGHT_PARENTHESIS:
                    _parenthesisDepth--;
                    return null;
                default:
                    _scanner.BackToken();
                    break;
            }

            SingleOperation expression = SingleExpressionRule();

            switch (_scanner.GetTokenType())
            {
                case TokenType.AND:
                    return _astFactory.CreateBooleanAnd(expression,
                                                        BooleanExpressionRule());
                case TokenType.OR:
                    return _astFactory.CreateBooleanOr(expression,
                                                     BooleanExpressionRule());                 
            }

            return expression;
        }

        /// <summary>
        /// SINGLE Expression Rule
        ///  | ( BooleanExpressionRule ) 
        ///  | ComparationExpressionRule
        /// </summary>
        /// <returns>AST SimpleOperation Tree</returns>
        private SingleOperation SingleExpressionRule()
        {

           
            Operation operation = ComparationExpressionRule();

           
            return _astFactory.CreateSingleExpression(operation);
        }

        /// <summary>
        /// COMPARATION Expression Rule
        ///  | FieldExpressionRule COMPARATOR ValueExpressionRule
        /// </summary>
        /// <returns>AST Operation Tree</returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private Operation ComparationExpressionRule()
        {
            FieldNode fieldNode = FieldExpressionRule();

            switch (_scanner.GetTokenType())
            {
                case TokenType.LESS:
                    return _astFactory.CreateComparationLess(fieldNode,
                                                             ValueExpressionRule());
                case TokenType.GREATER:
                    return _astFactory.CreateComparationGreater(fieldNode,
                                                                ValueExpressionRule());
                case TokenType.LESS_EQ:
                    return _astFactory.CreateComparationLessEqual(fieldNode,
                                                                  ValueExpressionRule());
                case TokenType.GREATER_EQ:
                    return _astFactory.CreateComparationGraterEqual(fieldNode,
                                                                    ValueExpressionRule());
                case TokenType.EQ:
                    return _astFactory.CreateComparationEqual(fieldNode,
                                                              ValueExpressionRule());
                case TokenType.NOT_EQ:
                    return _astFactory.CreateComparationNotEqual(fieldNode,
                                                                 ValueExpressionRule());
                case TokenType.IS:
                    switch (_scanner.GetTokenType())
                    {
                        case TokenType.NULL:
                            return _astFactory.CreateComparationIsNull(fieldNode);
                        case TokenType.NOT:
                            if (_scanner.GetTokenType() == TokenType.NULL)
                            {
                                return _astFactory.CreateComparationIsNotNull(fieldNode);
                            }
                            throw new ParserException(
                                    string.Format("Missing NULL at {0}",
                                                  _scanner.CurrentPosition));
                        default:
                            throw new ParserException(
                                    string.Format("Missing NULL at {0}",
                                                  _scanner.CurrentPosition));
                    }
                case TokenType.IS_NOT_NULL:
                    return _astFactory.CreateComparationIsNotNull(fieldNode);
                case TokenType.LIKE:
                    return _astFactory.CreateComparationContains(fieldNode,
                                                                 ValueExpressionRule());
                default:
                    throw new ParserException(
                            string.Format("Invalid Operation at {0}", _scanner.CurrentPosition));
            }
        }


        /// <summary>
        /// GROUPBY
        ///     | FieldListExpressionRule
        /// </summary>
        /// <returns>AST GroupBy Tree</returns>
        private GroupBy GroupByExpressionRule()
        {
            return _astFactory.CreateGroupBy(FieldListExpressionRule());
        }

        /// <summary>
        /// FIELDLIST
        ///     | FieldNode
        ///     | FieldNode, FIELDLIST
        /// </summary>
        /// <returns>AST FieldList Tree</returns>
        private FieldList FieldListExpressionRule()
        {
            FieldNode fieldNode = FieldExpressionRule();

            if (_scanner.GetTokenType() == TokenType.COMMA)
            {
                return _astFactory.CreateFieldList(fieldNode, FieldListExpressionRule());
            }

            return _astFactory.CreateFieldList(fieldNode);
        }


        /// <summary>
        /// ORDERBY
        ///  | FieldList
        /// </summary>
        /// <returns>AST Orderby Tree</returns>
        private OrderBy OrderByExpressionRule()
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
            FieldNode fieldNodeWithOrder = FieldNodeOrderExpressionRule();

            if (_scanner.LastToken == TokenType.COMMA
                || _scanner.GetTokenType() == TokenType.COMMA)
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
        private FieldNode FieldNodeOrderExpressionRule()
        {
            FieldNode fieldNode = FieldExpressionRule();

            switch (_scanner.GetTokenType())
            {
                case TokenType.DESC:
                    fieldNode.Ascending = false;
                    return fieldNode;
                case TokenType.ASC:
                    fieldNode.Ascending = true;
                    return fieldNode;
            }

            return fieldNode;
        }

        /// <summary>
        /// FIELD expression Rule
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private FieldNode FieldExpressionRule()
        {
            if (_scanner.GetTokenType() == TokenType.FIELD)
            {
                FieldNode fieldNode = _astFactory.CreateFieldNode(_scanner.TokenValue);
                return fieldNode;
            }

            throw new ParserException(
                    string.Format("Field name expected at {0}", _scanner.CurrentPosition));
        }

        /// <summary>
        /// VALUE expression Rule
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ParserException"><c>ParserException</c>.</exception>
        private ValueNode ValueExpressionRule()
        {
            if (_scanner.GetTokenType() == TokenType.VALUE)
            {
                return _astFactory.CreateValueNode(_scanner.TokenValue,
                                                   _scanner.TokenValueType.ToString());
            }

            throw new ParserException(
                    string.Format("TokenValue expected at {0}", _scanner.CurrentPosition));
        }
    }
}