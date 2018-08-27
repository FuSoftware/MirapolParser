using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser
{
    enum TokenType
    {
        COMMAND,
        SPECIAL,
        WHITESPACE,
        TEXT,
        PUNCTUATION,
        OPEN_BRACKET,
        CLOSE_BRACKET,
        VARIABLE,
        LOCAL_VARIABLE
    }

    class Token
    {

        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        public Token(TokenType type, char value)
        {
            this.Type = type;
            this.Value = value.ToString();
        }

        public string GetTypeString()
        {
            switch(this.Type)
            {
                case TokenType.COMMAND:
                    return "command";
                case TokenType.SPECIAL:
                    return "special";
                case TokenType.WHITESPACE:
                    return "whitespace";
                case TokenType.TEXT:
                    return "text";
                case TokenType.PUNCTUATION:
                    return "punctuation";
                case TokenType.OPEN_BRACKET:
                    return "bracket_o";
                case TokenType.CLOSE_BRACKET:
                    return "bracket_c";
                case TokenType.VARIABLE:
                    return "gvar";
                case TokenType.LOCAL_VARIABLE:
                    return "lvar";
                default:
                    return "";
            }
        }

        override public string ToString()
        {
            return this.GetTypeString() + " : " + this.Value.Trim();
        }
    }
}
