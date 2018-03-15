using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    class StringExpression : Expression
    {
        private readonly GeneralSymbol _variableSymbol;

        public StringExpression(string value)
        {
            _variableSymbol = new GeneralSymbol
            {
                Type = Enums.Type.String,
                StringValue = value
            };
        }

        public override GeneralSymbol  Evaluate()
        {
            return _variableSymbol;
        }
    }
}
