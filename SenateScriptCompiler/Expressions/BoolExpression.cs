using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class BoolExpression : Expression
    {

        private GeneralSymbol _variableSymbol;

        public BoolExpression(bool value)
        {
            _variableSymbol = new GeneralSymbol
            {
                Type = Enums.Type.Bool,
                BoolValue = value
            };
        }

        public override GeneralSymbol Evaluate()
        {
            return _variableSymbol;
        }

    }
}
