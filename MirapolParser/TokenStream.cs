using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MirapolParser;

namespace MirapolParser
{
    class TokenStream
    {
        InputStream input;
        Token current;

        public TokenStream(InputStream input)
        {
            this.input = input;
        }

        delegate bool Predicate(char c);

        bool IsLetter(char c)
        {
            return char.IsLetter(c);
        }

        bool IsLetterOrDigit(char c)
        {
            return char.IsLetterOrDigit(c);
        }

        bool IsWhitespace(char c)
        {
            return char.IsWhiteSpace(c);
        }

        bool IsTextOrWhitespace(char c)
        {
            return IsText(c) || IsWhitespace(c);
        }

        bool IsTextOrWhitespaceOrPunctuation(char c)
        {
            return IsTextOrWhitespace(c) || IsPunctuation(c);
        }

        bool IsNumeric(char c)
        {
            return char.IsNumber(c);
        }

        bool IsText(char c)
        {
            return char.IsLetterOrDigit(c);
        }

        bool IsPunctuation(char c)
        {
            return (char.IsPunctuation(c) || char.IsSymbol(c)) && !IsSpecialChar(c) && !IsOpenBracket(c) && !IsCloseBracket(c);
        }

        bool IsOpenBracket(char c)
        {
            return @"{".Contains(c);
        }

        bool IsCloseBracket(char c)
        {
            return @"}".Contains(c);
        }

        bool IsSpecialChar(char c)
        {
            return @"\_$".Contains(c);
        }

        bool IsBacklash(char c)
        {
            return @"\".Contains(c);
        }

        bool IsDollar(char c)
        {
            return @"$".Contains(c);
        }

        bool IsUnderscore(char c)
        {
            return @"_".Contains(c);
        }
        bool IsNotPunc(char c)
        {
            return !IsSpecialChar(c);
        }

        string ReadWhile(Predicate predicate)
        {
            string str = "";
            while (!input.eof() && predicate(input.peek()))
                str += input.next();
            return str;
        }

        private Token ReadNext()
        {
            if (input.eof()) return null;
            char ch = input.peek();

            if (IsBacklash(ch))
            {
                this.input.next();
                return new Token(TokenType.COMMAND, ReadWhile(IsText));
            }
            else if(IsDollar(ch))
            {
                this.input.next();
                return new Token(TokenType.VARIABLE, ReadWhile(IsText));
            }
            else if (IsUnderscore(ch))
            {
                this.input.next();
                return new Token(TokenType.LOCAL_VARIABLE, ReadWhile(IsText));
            }
            else if (IsOpenBracket(ch))
            {
                return new Token(TokenType.OPEN_BRACKET, input.next());
            }
            else if (IsCloseBracket(ch))
            {
                return new Token(TokenType.CLOSE_BRACKET, input.next());
            }
            else if(IsSpecialChar(ch))
            {
                return new Token(TokenType.SPECIAL, input.next());
            }
            else if (IsText(ch))
            {
                return new Token(TokenType.TEXT, ReadWhile(IsTextOrWhitespaceOrPunctuation));
            }
            else if (IsWhitespace(ch))
            {
                return new Token(TokenType.TEXT, ReadWhile(IsTextOrWhitespaceOrPunctuation));
            }
            else if (IsPunctuation(ch))
            {
                return new Token(TokenType.TEXT, ReadWhile(IsTextOrWhitespaceOrPunctuation));
            }
            else
            {
                this.input.croak("Can't handle character: " + ch);
                return null;
            }
        }

        public Token Peek()
        {
            if(current == null)
            { 
                current = this.ReadNext();
            }

            return current;
        }

        public Token Next()
        {
            Token t = current;
            current = null;

            if(t == null)
            {
                t = this.ReadNext();
            }
            return t;
        }

        public bool EOF()
        {
            return this.Peek() == null;
        }
    }
}
