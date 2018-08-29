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
        LOCAL_VARIABLE,
        IF,
        ELSIF,
        ELSE
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
            return this.Type.ToString();
        }

        override public string ToString()
        {
            return this.GetTypeString() + " : " + this.Value.Trim();
        }
    }
}
