using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class VariableAssignmentStatement : Statement
    {
        private readonly Expression _expression;
        private GeneralSymbol _variableSymbol;
        private readonly String _variableName;

        public VariableAssignmentStatement(string variableName, Expression value)
        {
            _expression = value;
            _variableName = variableName;
        }

        public override bool Execute()
        {

            _variableSymbol = _expression.Evaluate();

            if (GlobalSymbolTable.table.GetVariableSymbol(_variableName).Type != _variableSymbol.Type)
                throw new Exception("Cannot assign type " + _variableSymbol.Type + " to type " + GlobalSymbolTable.table.GetVariableSymbol(_variableName).Type);

            GlobalSymbolTable.table.AssignVariable(_variableName, _variableSymbol);

            return true;
        }
    }
}
