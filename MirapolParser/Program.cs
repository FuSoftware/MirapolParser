using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MirapolParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTokens(TestCases.SimpleFunction, true);
            TestCases.TestJSCreationFunction();
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static void TestTimingTokens(string input, int count = 100)
        {
            List<long> t = new List<long>();
            for(int i=0;i<count;i++)
            {
                t.Add(TestTokens(input, false));
            }
            Console.WriteLine("Parsed {0} times in {1} ms (Avg : {2} ms)", count, t.Sum(), t.Average());
        }

        static long TestTokens(string input, bool print = false)
        {
            Stopwatch sw = new Stopwatch();
            List<Token> tokens = new List<Token>();

            InputStream ins = new InputStream(input);
            TokenStream ts = new TokenStream(ins);
            Token t = null;

            sw.Start();
            do
            {
                t = ts.Next();

                if (t != null)
                {
                    tokens.Add(t);
                }

            } while (t != null);
            sw.Stop();

            if(print)
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    Console.WriteLine(tokens[i].ToString());
                }

                Console.WriteLine("Parsing done in {0} ms", sw.ElapsedMilliseconds);
            }

            return sw.ElapsedMilliseconds;
        }
    }
}
