using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Enums;

namespace SenateScriptCompiler.Expressions
{
    class UnaryExpression : Expression
    {
        private readonly Expression _expression;
        private readonly Operator _operator;

        public UnaryExpression(Expression expression, Operator op)
        {
            _expression = expression;
            _operator = op;
        }

        public override Symbol Evaluate()
        {
            if (_expression.Evaluate().Type != Enums.Type.Number)
                throw new Exception("Incorrect type");

            switch (_operator)
            {
                case Operator.Plus:
                    return _expression.Evaluate();
                //Probably won't work
                //Yeah it doesn't work
                /*
                case Operator.Minus:
                    Symbol symbol = new Symbol();
                    symbol.Type = Enums.Type.Number;
                    symbol.NumberValue = _expression.Evaluate().NumberValue * -1;
                    _expression.SetSymbol(symbol);
                    return _expression.Evaluate();
                */
            }

            return null;
        }

        public override void SetSymbol(Symbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}
