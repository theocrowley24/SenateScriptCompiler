using System;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("NUMBER x = 5;" + 
                                       "NUMBER y = 10;" +
                                       "PRINT x+y;" +
                                       "x = 10;");

            parser.Parse();
            parser.Run();  
        }
    }
}
