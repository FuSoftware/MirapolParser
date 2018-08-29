using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class LinkNode : ASTNode
    {
        ASTNode To;
        ASTNode Label;

        public LinkNode(ASTNode to) : base("link")
        {
            this.To = to;
            this.Label = to;
        }

        public LinkNode(ASTNode to, ASTNode label) : base("link")
        {
            this.To = to;
            this.Label = label;
        }

        override public string GetValue()
        {
            return "";
        }

        override protected string GetSelfJS()
        {
            return string.Format("<a data-passage=\"{0}\">{1}</a>", this.To.ToJS(), this.Label.ToJS());
        }
    }
}
