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

namespace Layer01_Common_IdeSeg.SharePoint.Caml.QueryParser.AST
{
    public class CodeGenerator
    {
        private readonly ASTNodeBase _tree;
        private string _code;

        public CodeGenerator(ASTNodeBase tree)
        {
            if (tree == null)
            {
                throw new ArgumentException("Tree can´t be null", "tree");
            }

            _code = string.Empty;
            _tree = tree;
        }

        public string Code
        {
            get { return _code; }
        }

        public void Generate()
        {
            GenerateInternal(_tree);
        }

        private void GenerateInternal(ASTNodeBase node)
        {
            AddCode(node.PreCode());

            if (node.LeftNode != null)
            {
                GenerateInternal(node.LeftNode);
            }

            if (node.RightNode != null)
            {
                GenerateInternal(node.RightNode);
            }

            AddCode(node.PostCode());
        }

        private void AddCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                _code += code;
            }
        }
    }
}