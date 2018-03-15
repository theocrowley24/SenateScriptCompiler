using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    static class GlobalSymbolTable
    {
        public static SymbolTable table;

        static GlobalSymbolTable() => table = new SymbolTable();
    }
}
