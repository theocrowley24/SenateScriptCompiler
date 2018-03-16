using System;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler
{
    class Program
    {
        static void Main(string[] args)
        {

            String code = System.IO.File.ReadAllText(@"C:\Users\Theo\Desktop\SenateTestCode.txt");

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("<---- CODE TO COMPILE ---->\n");
            Console.WriteLine(code);
            Console.WriteLine();
            Console.WriteLine("<------------------------->\n");

            Parser parser = new Parser(code);
            parser.Run();
        }
    }
}
