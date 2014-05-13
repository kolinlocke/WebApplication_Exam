#region Copyright(c) Carlos Segura Sanz - www.ideseg.com

// Copyright (c) 2009 Carlos Segura Sanz - www.ideseg.com
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST
{
    public abstract class ASTNodeBase
    {
        private readonly ASTNodeBase _leftNode;
        private readonly ASTNodeBase _rightNode;

        protected ASTNodeBase(ASTNodeBase leftNode, ASTNodeBase rightNode)
        {
            _leftNode = leftNode;
            _rightNode = rightNode;
        }

        public ASTNodeBase LeftNode
        {
            get { return _leftNode; }
        }

        public ASTNodeBase RightNode
        {
            get { return _rightNode; }
        }

        public virtual string PreCode()
        {
            return string.Empty;
        }

        public virtual string PostCode()
        {
            return string.Empty;
        }
    }
}