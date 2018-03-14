using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class VariableDeclerationStatement : Statement
    {
        private readonly Symbol _symbol;
        private readonly String _variableName;

        public VariableDeclerationStatement(string variableName, Expression value)
        {
            _symbol = value.Evaluate();
            _variableName = variableName;
        }

        public override bool Execute()
        {
            SymbolTable.Add(_variableName, _symbol);
            
            return true;
        }
    }
}
