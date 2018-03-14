using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class VariableExpression : Expression
    {
        private readonly string _variableName;

        public VariableExpression(string variableName)
        {
            _variableName = variableName;
        }

        public override Symbol Evaluate()
        {
            return SymbolTable.Get(_variableName);
        }

        public override void SetSymbol(Symbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}
