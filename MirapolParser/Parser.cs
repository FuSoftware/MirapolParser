using System;
using System.Collections.Generic;
using System.Text;
using MirapolParser.AST;

namespace MirapolParser
{
    class Parser
    {
        public TokenStream Input { get; }
        List<ASTNode> ASTNodes;

        public Parser(string input)
        {
            this.Input = new TokenStream(input);
        }

        public Parser(TokenStream input)
        {
            this.Input = input;
        }

        public ASTNode ParseAll()
        {
            ASTNode root = new ASTNode();
            ASTNode temp = ParseNext();

            while (temp != null)
            {
                root.AddChild(temp);
                temp = ParseNext();
            }

            return root;
        }

        public ASTNode ParseNext()
        {
            if (this.Input.EOF()) return null;
            Token t = this.Input.Peek();

            switch (t.Type)
            {
                case TokenType.COMMAND:
                    return ParseCommand();
                case TokenType.TEXT:
                    return ParseText();
                case TokenType.WHITESPACE:
                    return ParseText();
                case TokenType.OPEN_BRACKET:
                    return ParseSection();
                case TokenType.IF:
                    return ParseConditional();
                default:
                    this.Input.Input.Croak(string.Format("Found an invalid token {0}", t.GetTypeString()));
                    break;
            }

            return null;
        }

        private ASTNode ParseCommand()
        {
            Token macro_name = this.Input.Next();
            //Console.WriteLine("Parsing Command {0}", macro_name.Value);

            Token post_macro = this.Input.Peek();

            if(post_macro.Type == TokenType.OPEN_BRACKET)
            {
                //Macro
                return ParseMacro(macro_name.Value);
            }
            else
            {
                //Variable
                return new VarNode(macro_name.Value);
            }
        }

        private MacroNode ParseMacro(string macro_name)
        {
            //Console.WriteLine("Parsing Macro {0}", macro_name);
            List<ASTNode> parameters = new List<ASTNode>();
            while(this.Input.Peek().Type == TokenType.OPEN_BRACKET)
            {
                parameters.Add(ParseSection());
            }

            return new MacroNode(macro_name, parameters);
        }

        private ASTNode ParseConditional()
        {
            //Console.WriteLine("Parsing Conditional");
            IfNode n = new IfNode();
            n.AddCondition(ParseSection(), ParseSection());

            Token t = this.Input.Peek();

            if(t.Type == TokenType.COMMAND)
            {
                this.Input.Next();
                if (this.Input.Peek().GetTypeString() == "elsif")
                {
                    this.Input.Next();
                    n.AddCondition(ParseSection(), ParseSection());
                }
            }

            return n;
        }

        private ASTNode ParseSection() //{...}
        {
            //Console.WriteLine("Parsing Section");
            ASTNode root = new ASTNode();
            //When we reach here, the next token is a {
            while(this.Input.Peek().Type != TokenType.CLOSE_BRACKET)
            {
                //While we don't close the section
                Token t = this.Input.Next(); // Read the next token
                root.AddChild(ParseNext());
            }

            this.Input.Next(); //Passes the closing bracket

            return root;
        }

        private ASTNode ParseText()
        {
            //Console.WriteLine("Parsing Text");
            return new StrNode(Input.Next().Value);
        }

        public dynamic ParseVariable()
        {
            return null;
        }
    }
}
