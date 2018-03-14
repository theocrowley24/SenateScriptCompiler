using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class StringExpression : Expression
    {
        private readonly Symbol _symbol;

        public StringExpression(string value)
        {
            _symbol = new Symbol
            {
                Type = Enums.Type.String,
                StringValue = value
            };
        }

        public override Symbol  Evaluate()
        {
            return _symbol;
        }

        public override void SetSymbol(Symbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}
