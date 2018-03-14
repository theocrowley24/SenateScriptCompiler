using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class PrintStatement : Statement
    {
        private readonly Expression _expression;
        private Symbol _symbol;

        public PrintStatement(Expression expression)
        {
            _expression = expression;
        }

        public override bool Execute()
        {
            _symbol = _expression.Evaluate();

            switch (_symbol.Type)
            {
                case Enums.Type.String:
                    Console.WriteLine(_symbol.StringValue);
                    return true;
                case Enums.Type.Bool:
                    Console.WriteLine(_symbol.BoolValue);
                    return true;
                case Enums.Type.Number:
                    Console.WriteLine(_symbol.NumberValue);
                    return true;
                default:
                    return false;
            }
        }
    }
}
