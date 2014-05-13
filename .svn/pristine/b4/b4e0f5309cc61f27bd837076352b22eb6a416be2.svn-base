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
using Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST.CAML;

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST
{
    // TODO: Extend with CAML.Net
    public class ASTNodeCAMLFactory : ASTNodeFactoryBase
    {
        public override Query CreateQuery(Sequence sequence)
        {
            return new CAMLQuery(sequence);
        }

        public override Where CreateWhere(Expression expressionExp)
        {
            return new CAMLWhere(expressionExp);
        }

        public override GroupBy CreateGroupBy(FieldList fields)
        {
            return new CAMLGroupBy(fields);
        }

        public override OrderBy CreateOrderBy(FieldList fields)
        {
            return new CAMLOrderBy(fields);
        }

        public override BooleanAnd CreateBooleanAnd(Expression left, Expression right)
        {
            return new CAMLBooleanAnd(left, right);
        }

        public override BooleanOr CreateBooleanOr(Expression left, Expression right)
        {
            return new CAMLBooleanOr(left, right);
        }

        public override Operation CreateComparationBegin(FieldNode field, ValueNode value)
        {
            return new CAMLOpBeginsWith(field, value);
        }

        public override Operation CreateComparationContains(FieldNode field, ValueNode value)
        {
            return new CAMLOpContains(field, value);
        }

        public override Operation CreateComparationEqual(FieldNode field, ValueNode value)
        {
            return new CAMLOpEqual(field, value);
        }

        public override Operation CreateComparationGreater(FieldNode field, ValueNode value)
        {
            return new CAMLOpGreater(field, value);
        }

        public override Operation CreateComparationGraterEqual(FieldNode field, ValueNode value)
        {
            return new CAMLOpGreaterEqual(field, value);
        }

        public override Operation CreateComparationLess(FieldNode field, ValueNode value)
        {
            return new CAMLOpLess(field, value);
        }

        public override Operation CreateComparationLessEqual(FieldNode field, ValueNode value)
        {
            return new CAMLOpLessEqual(field, value);
        }

        public override Operation CreateComparationNotEqual(FieldNode field, ValueNode value)
        {
            return new CAMLOpNotEqual(field, value);
        }

        public override Operation CreateComparationIsNull(FieldNode field)
        {
            return new CAMLOpIsNull(field);
        }

        public override Operation CreateComparationIsNotNull(FieldNode field)
        {
            return new CAMLOpIsNotNull(field);
        }

        public override FieldNode CreateFieldNode(string fieldName)
        {
            return new CAMLFieldNode(fieldName);
        }

        public override FieldNode CreateFieldNode(string fieldName, bool order)
        {
            return new CAMLFieldNode(fieldName, order);
        }

        public override ValueNode CreateValueNode(string value, string valueType)
        {
            return new CAMLValueNode(value, valueType);
        }

        public override ValueNode CreateValueNode(string value, ValueNodeType valueType)
        {
            return new CAMLValueNode(value, valueType);
        }
    }
}