using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class ASTNode
    {
        public string Type { get; }
        public List<ASTNode> Children = new List<ASTNode>();

        public ASTNode()
        {
            this.Type = "node";
        }

        public ASTNode(string type)
        {
            this.Type = type;
        }

        public void AddChild(ASTNode child)
        {
            this.Children.Add(child);
        }

        virtual public string GetValue()
        {
            return "";
        }

        protected string GetChildrenJS()
        {
            string s = "";

            for (int i = 0; i < this.Children.Count; i++)
            {
                s += this.Children[i].ToJS();
            }

            return s;
        }

        public string ToTaggedJS()
        {
            return "<script>" + this.ToJS() + "</script>";
        }

        virtual protected string GetSelfJS()
        {
            return "";
        }

        public string ToJS()
        {
            return GetChildrenJS() + GetSelfJS() ;
        }
    }
}