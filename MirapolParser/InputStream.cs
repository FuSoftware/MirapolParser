using System;
using System.Collections.Generic;
using System.Text;

namespace MirapolParser
{
    class InputStream
    {
        long pos = 0;
        long col = 0;
        long line = 0;
        private char[] input;

        public InputStream(string input)
        {
            this.input = input.ToCharArray();
        }

        public char next()
        {
            var ch = this.input[pos++];
            if (ch == '\n')
            {
                line++;
                col = 0;
            }
            else
            {
                col++;
            }  
            return ch;
        }
        public char peek()
        {
            return this.input[pos];
        }
        public bool eof()
        {
            return this.pos >= this.input.LongLength;
        }
        public void croak(string msg)
        {
            throw new Exception(msg + " (" + line + ":" + col + ")");
        }
    }
}
