using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class NumericalExpression : Expression
    {

        private Symbol _symbol;

        public NumericalExpression(double value)
        {
            _symbol = new Symbol
            {
                Type = Enums.Type.Number,
                NumberValue = value
            };
        }

        public override Symbol Evaluate()
        {
            return _symbol;
        }

    }
}
