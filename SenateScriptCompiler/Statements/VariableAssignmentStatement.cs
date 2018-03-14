using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class VariableAssignmentStatement : Statement
    {
        private readonly Symbol _symbol;
        private readonly String _variableName;

        public VariableAssignmentStatement(string variableName, Expression value)
        {
            _symbol = value.Evaluate();
            _variableName = variableName;
        }

        public override bool Execute()
        {
            SymbolTable.Assign(_variableName, _symbol);

            return true;
        }
    }
}
