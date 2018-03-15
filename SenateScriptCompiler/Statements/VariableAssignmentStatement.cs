using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class VariableAssignmentStatement : Statement
    {
        private readonly Expression _expression;
        private Symbol _symbol;
        private readonly String _variableName;

        public VariableAssignmentStatement(string variableName, Expression value)
        {
            _expression = value;
            _variableName = variableName;
        }

        public override bool Execute()
        {

            _symbol = _expression.Evaluate();

            if (GlobalSymbolTable.table.Get(_variableName).Type != _symbol.Type)
                throw new Exception("Cannot assign type " + _symbol.Type + " to type " + GlobalSymbolTable.table.Get(_variableName).Type);

            GlobalSymbolTable.table.Assign(_variableName, _symbol);

            return true;
        }
    }
}
