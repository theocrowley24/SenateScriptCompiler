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
            switch (_operator)
            {
                case Operator.Plus:
                    if (_expression.Evaluate().Type != Enums.Type.Number)
                        throw new Exception("Incorrect type");
                    return _expression.Evaluate();
                case Operator.Minus:
                    if (_expression.Evaluate().Type != Enums.Type.Number)
                        throw new Exception("Incorrect type");
                    Symbol symbolMinus = _expression.Evaluate();
                    symbolMinus.Minus();
                    return symbolMinus;
                case Operator.Not:
                    if (_expression.Evaluate().Type != Enums.Type.Bool)
                        throw new Exception("Incorrect type");
                    Symbol symbolNot = _expression.Evaluate();
                    symbolNot.Not();
                    return symbolNot;

            }

            return null;
        }
    }
}
