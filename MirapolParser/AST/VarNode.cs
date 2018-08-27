using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class VarNode : ASTNode
    {
        public string Name { get; }
        public bool Local { get; }
        public VarNode(string name, bool local = false) : base("var")
        {
            this.Name = name;
            this.Local = local;
        }

        override public string GetValue()
        {
            return "";
        }

        override protected string GetSelfJS()
        {
            if (this.Local)
            {
                return "_" + this.Name;
            }
            else
            {
                return "window.mirapol.variables." + this.Name;
            }
            
        }
    }
}
