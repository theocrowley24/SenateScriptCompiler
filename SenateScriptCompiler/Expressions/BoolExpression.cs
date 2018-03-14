using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class BoolExpression : Expression
    {

        private Symbol _symbol;

        public BoolExpression(bool value)
        {
            _symbol = new Symbol
            {
                Type = Enums.Type.Bool,
                BoolValue = value
            };
        }

        public override Symbol Evaluate()
        {
            return _symbol;
        }

    }
}
