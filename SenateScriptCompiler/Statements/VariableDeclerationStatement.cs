using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class VariableDeclerationStatement : Statement
    {
        private readonly GeneralSymbol _variableSymbol;
        private readonly String _variableName;

        public VariableDeclerationStatement(Enums.Type type, string variableName, Expression value)
        {

            _variableSymbol = value.Evaluate();
            _variableName = variableName;

            if (_variableSymbol.Type != type)
                throw new Exception("Cannot declare type " + _variableSymbol.Type.ToString() + " as " + type);
        }

        public VariableDeclerationStatement(Enums.Type type, string variableName, GeneralSymbol symbol)
        {
            _variableSymbol = symbol;
            _variableName = variableName;

            if (_variableSymbol.Type != type)
                throw new Exception("Cannot declare type " + _variableSymbol.Type.ToString() + " as " + type);
        }

        public override bool Execute()
        {
            GlobalSymbolTable.table.AddToVariableTable(_variableName, _variableSymbol);
            
            return true;
        }
    }
}
