using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    static class SymbolTable
    {
        private static Hashtable table;

        static SymbolTable()
        {
            table = new Hashtable();
        }

        public static bool Add(string variableName, Symbol symbol)
        {
            if (table.ContainsKey(variableName))
                throw new Exception("Variable already exists!");

            table.Add(variableName, symbol);
            return true;
        }

        public static Symbol Get(string variableName)
        {
            if (!table.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            return table[variableName] as Symbol;
        }

        public static bool Assign(string variableName, Symbol symbol)
        {
            if (!table.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            table[variableName] = symbol;
            return true;
        }
    }
}
