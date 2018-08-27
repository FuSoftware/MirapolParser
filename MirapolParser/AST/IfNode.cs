using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class IfNode : ASTNode
    {
        struct Condition
        {
            public ASTNode Cond { get; }
            public ASTNode Code { get; }

            public Condition(ASTNode cond, ASTNode code)
            {
                this.Cond = cond;
                this.Code = code;
            }
        }

        List<Condition> conditions = new List<Condition>();

        public IfNode() : base("if")
        {

        }

        public void AddCondition(ASTNode cond, ASTNode code)
        {
            this.conditions.Add(new Condition(cond, code));
        }

        override public string GetValue()
        {
            return "";
        }

        override protected string GetSelfJS()
        {
            string s = "";

            for (int i = 0; i < this.conditions.Count; i++)
            {
                Condition c = this.conditions[i];
                s += (i == 0) ? "if" : "else if";
                s += "(" + c.Cond.ToJS() + "){" + c.Code.ToJS() + "}";
            }
            return s;
        }
    }
}
