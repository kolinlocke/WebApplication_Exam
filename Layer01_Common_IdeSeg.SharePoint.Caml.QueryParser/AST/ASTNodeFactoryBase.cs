#region Copyright(c) Carlos Segura Sanz - www.ideseg.com

// Copyright (c) 2009 Carlos Segura Sanz - www.ideseg.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST.Base;

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST
{
    public abstract class ASTNodeFactoryBase
    {
        #region CAML

        public virtual Query CreateQuery(Sequence sequence)
        {
            return new Query(sequence);
        }

        #endregion

        #region Sequence

        public virtual Sequence CreateSequence(Where where, Sequence sequence)
        {
            return new Sequence(where, sequence);
        }

        public virtual Sequence CreateSequence(OrderBy orderBy, Sequence sequence)
        {
            return new Sequence(orderBy, sequence);
        }

        public virtual Sequence CreateSequence(GroupBy groupBy, Sequence sequence)
        {
            return new Sequence(groupBy, sequence);
        }

        #endregion

        #region Where

        public virtual Where CreateWhere(Expression expressionExp)
        {
            return new Where(expressionExp);
        }

        #endregion

        #region GrupBy

        public virtual GroupBy CreateGroupBy(FieldList fields)
        {
            return new GroupBy(fields);
        }

        #endregion

        #region OrderBy

        public virtual OrderBy CreateOrderBy(FieldList fields)
        {
            return new OrderBy(fields);
        }

        #endregion

        #region Expressions

        public virtual BooleanAnd CreateBooleanAnd(Expression left, Expression right)
        {
            return new BooleanAnd(left, right);
        }

        public virtual BooleanOr CreateBooleanOr(Expression left, Expression right)
        {
            return new BooleanOr(left, right);
        }

        public virtual Expression CreateExpression(Operation operation)
        {
            return new Expression(operation);
        }

        #endregion

        #region FieldOperation

        public virtual Operation CreateOperation(FieldNode field, ValueNode value)
        {
            return new Operation(field, value);
        }

        #endregion

        #region Comparations

        public virtual Operation CreateComparationBegin(FieldNode field, ValueNode value)
        {
            return new OpBeginsWith(field, value);
        }

        public virtual Operation CreateComparationContains(FieldNode field, ValueNode value)
        {
            return new OpContains(field, value);
        }

        public virtual Operation CreateComparationEqual(FieldNode field, ValueNode value)
        {
            return new OpEqual(field, value);
        }

        public virtual Operation CreateComparationGreater(FieldNode field, ValueNode value)
        {
            return new OpGreater(field, value);
        }

        public virtual Operation CreateComparationGraterEqual(FieldNode field, ValueNode value)
        {
            return new OpGreaterEqual(field, value);
        }

        public virtual Operation CreateComparationLess(FieldNode field, ValueNode value)
        {
            return new OpLess(field, value);
        }

        public virtual Operation CreateComparationLessEqual(FieldNode field, ValueNode value)
        {
            return new OpLessEqual(field, value);
        }

        public virtual Operation CreateComparationNotEqual(FieldNode field, ValueNode value)
        {
            return new OpNotEqual(field, value);
        }

        public virtual Operation CreateComparationIsNull(FieldNode field)
        {
            return new OpIsNull(field);
        }

        public virtual Operation CreateComparationIsNotNull(FieldNode field)
        {
            return new OpIsNotNull(field);
        }

        #endregion

        #region Terminals

        public virtual FieldNode CreateFieldNode(string fieldName)
        {
            return new FieldNode(fieldName);
        }

        public virtual FieldNode CreateFieldNode(string fieldName, bool ascendingOrder)
        {
            return new FieldNode(fieldName, ascendingOrder);
        }

        public virtual ValueNode CreateValueNode(string value, string valueType)
        {
            return new ValueNode(value, valueType);
        }

        public virtual ValueNode CreateValueNode(string value, ValueNodeType valueType)
        {
            return new ValueNode(value, valueType);
        }

        public virtual TrueNode CreateTrueNode()
        {
            return new TrueNode();
        }

        public virtual FalseNode CreateFalseNode()
        {
            return new FalseNode();
        }

        public virtual FieldList CreateFieldList(FieldNode fieldLeft, FieldList fields)
        {
            return new FieldList(fieldLeft, fields);
        }

        public virtual FieldList CreateFieldList(FieldNode fieldLeft)
        {
            return new FieldList(fieldLeft);
        }

        #endregion
    }
}