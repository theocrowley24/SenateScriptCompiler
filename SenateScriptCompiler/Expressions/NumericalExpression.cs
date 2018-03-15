using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class NumericalExpression : Expression
    {

        private GeneralSymbol _variableSymbol;

        public NumericalExpression(double value)
        {
            _variableSymbol = new GeneralSymbol
            {
                Type = Enums.Type.Number,
                NumberValue = value
            };
        }

        public override GeneralSymbol Evaluate()
        {
            return _variableSymbol;
        }

    }
}
