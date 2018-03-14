using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    static class SymbolTable
    {
        static private Dictionary<string, Symbol> table;

        static SymbolTable()
        {
            table = new Dictionary<string, Symbol>();
        }

        static public bool Add(string variableName, Symbol symbol)
        {
            table.Add(variableName, symbol);
            return true;
        }

        static public Symbol Get(string variableName)
        {
            return table[variableName];
        }

        static public bool Assign(string variableName, Symbol symbol)
        {
            table[variableName] = symbol;
            return true;
        }
    }
}
