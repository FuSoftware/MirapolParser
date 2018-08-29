using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser
{
    class InputStream
    {
        public long Pos { set;  get; }
        public long Col { set;  get; }
        public long Line { set;  get; }
        private char[] input;

        public InputStream(string input)
        {
            this.input = input.ToCharArray();
            Pos = 0;
            Col = 0;
            Line = 0;
    }

        public char Next()
        {
            var ch = this.input[Pos++];
            if (ch == '\n')
            {
                Line++;
                Col = 0;
            }
            else
            {
                Col++;
            }  
            return ch;
        }
        public char Peek()
        {
            return this.input[Pos];
        }
        public bool EOF()
        {
            return this.Pos >= this.input.LongLength;
        }
        public void Croak(string msg)
        {
            throw new Exception(msg + " (" + Line + ":" + Col + ")");
        }
    }
}
