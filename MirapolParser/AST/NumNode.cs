using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser.AST
{
    class NumNode : ASTNode
    {
        float n = 0.0f;

        public NumNode(float n) : base("num")
        {
            this.n = n;
        }

        public NumNode(int n)
        {
            this.n = n;
        }

        public NumNode(string n)
        {
            bool ok = float.TryParse(n, out this.n);
        }

        public int ToInt()
        {
            return (int)this.ToFloat();
        }

        public float ToFloat()
        {
            return this.n;
        }

        override public string GetValue()
        {
            return this.n.ToString();
        }
    }
}
