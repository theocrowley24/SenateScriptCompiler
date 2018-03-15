using System;
using System.Collections;

namespace SenateScriptCompiler.Symbols
{
    class SymbolTable
    {
        private readonly Hashtable _variableTable;
        private readonly Hashtable _functionTable;

        public SymbolTable()
        {
            _variableTable = new Hashtable();
            _functionTable = new Hashtable();
        }

        public bool AddToVariableTable(string variableName, GeneralSymbol variableSymbol)
        {
            if (_variableTable.ContainsKey(variableName))
                throw new Exception("Variable already exists!");

            _variableTable.Add(variableName, variableSymbol);
            return true;
        }

        public bool AddToFunctionTable(string functionName, FunctionSymbol functionSymbol)
        {
            if (_functionTable.ContainsKey(functionName))
                throw new Exception("Function is already defined!");

            _functionTable.Add(functionName, functionSymbol);
            return true;
        }

        public GeneralSymbol GetVariableSymbol(string variableName)
        {
            if (!_variableTable.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            return _variableTable[variableName] as GeneralSymbol;
        }

        public FunctionSymbol GetFunctionSymbol(string functionName)
        {
            if (!_functionTable.ContainsKey(functionName))
                throw new Exception("Function is not defined!");

            return _functionTable[functionName] as FunctionSymbol;
        }

        public bool AssignVariable(string variableName, GeneralSymbol variableSymbol)
        {
            if (!_variableTable.ContainsKey(variableName))
                throw new Exception("Variable does not exist in current context!");

            _variableTable[variableName] = variableSymbol;
            return true;
        }
    }
}
