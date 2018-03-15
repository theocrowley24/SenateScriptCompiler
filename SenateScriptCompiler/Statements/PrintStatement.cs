using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Expressions;

namespace SenateScriptCompiler.Statements
{
    class PrintStatement : Statement
    {
        private readonly Expression _expression;
        private GeneralSymbol _variableSymbol;

        public PrintStatement(Expression expression)
        {
            _expression = expression;
        }

        public override bool Execute()
        {
            _variableSymbol = _expression.Evaluate();

            switch (_variableSymbol.Type)
            {
                case Enums.Type.String:
                    Console.WriteLine(_variableSymbol.StringValue);
                    return true;
                case Enums.Type.Bool:
                    Console.WriteLine(_variableSymbol.BoolValue);
                    return true;
                case Enums.Type.Number:
                    Console.WriteLine(_variableSymbol.NumberValue);
                    return true;
                default:
                    return false;
            }
        }
    }
}
