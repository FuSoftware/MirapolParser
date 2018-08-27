using System;
using System.Collections.Generic;
using System.Text;
using MirapolParser.AST;

namespace MirapolParser
{
    class TestCases
    {
        public static string HelloWorld = @"Hello \World !";
        public static string SimpleFunction = @"\macro{link}{to,text}{[[_to|_text]]}";
        public static string HelloLatex = @"
            \begin{document} 
                This is my first \LaTeX document.
            \end{document}";

        static public void TestJSCreationFunction()
        {
            VarNode m_1 = new VarNode("link");
            StrNode m_2 = new StrNode("to,text");
            StrNode m_3 = new StrNode("[[_to|_text]]");

            MacroNode m = new MacroNode("macro", new List<ASTNode>(){ m_1, m_2, m_3 });

            Console.WriteLine(m.ToTaggedJS());
        }
    }
}
