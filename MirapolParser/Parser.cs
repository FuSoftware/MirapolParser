using System;
using System.Collections.Generic;
using System.Text;
using MirapolParser.AST;

namespace MirapolParser
{
    class Parser
    {
        TokenStream input;
        List<ASTNode> ASTNodes;

        public Parser(TokenStream input)
        {
            this.input = input;
        }

        public ASTNode ParseNext()
        {
            Token t = this.input.Peek();
            if (t == null) return null;

            switch (t.Type)
            {
                case TokenType.COMMAND:
                    return ParseMacro();
                case TokenType.TEXT:
                    return ParseText();
                case TokenType.WHITESPACE:
                    return ParseText();
                default:
                    return null;
            }
        }

        private ASTNode ParseMacro()
        {
            this.input.Next(); //Skip blackslash
            Token t = this.input.Next();

            if(t.Value == "if")
            {
                return ParseConditional();
            }

            return null;
        }

        private ASTNode ParseConditional()
        {
            IfNode n = new IfNode();
            n.AddCondition(ParseSection(), ParseSection());

            Token t = this.input.Peek();

            if(t.Type == TokenType.COMMAND)
            {
                this.input.Next();
                if (this.input.Peek().GetTypeString() == "elsif")
                {
                    this.input.Next();
                    n.AddCondition(ParseSection(), ParseSection());
                }
            }

            return n;
        }

        private ASTNode ParseSection() //{...}
        {
            while(this.input.Peek().Type != TokenType.CLOSE_BRACKET)
            {
                //While we don't close the section
                Token t = this.input.Peek(); // Read the next token
            }

            return null;
        }

        private ASTNode ParseText()
        {
            return new StrNode(input.Next().Value);
        }

        public dynamic ParseVariable()
        {
            return null;
        }
    }
}
