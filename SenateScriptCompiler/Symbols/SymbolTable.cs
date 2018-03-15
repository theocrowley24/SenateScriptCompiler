using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    class SymbolTable
    {
        private Hashtable table;

        public SymbolTable()
        {
            table = new Hashtable();
        }

        public bool Add(string variableName, Symbol symbol)
        {
            if (table.ContainsKey(variableName))
                throw new Exception("Variable already exists!");

            table.Add(variableName, symbol);
            return true;
        }

        public Symbol Get(string variableName)
        {
            if (!table.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            return table[variableName] as Symbol;
        }

        public bool Assign(string variableName, Symbol symbol)
        {
            if (!table.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            table[variableName] = symbol;
            return true;
        }
    }
}
