using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class MacroNode : ASTNode
    {
        public string Name { get; }
        public List<ASTNode> parameters = new List<ASTNode>();

        public MacroNode(string name, List<ASTNode> parameters) : base("macro")
        {
            this.Name = name;
            this.parameters = parameters;
        }

        override public string GetValue()
        {
            return "";
        }

        override protected string GetSelfJS()
        {
            string p = "";

            for(int i = 0; i < this.parameters.Count; i++)
            {
                p += parameters[i].ToJS() + ",";
            }

            p = p.TrimEnd(',');

            string s = String.Format("{0}({1})", this.Name, p);

            return s;
        }
    }
}
