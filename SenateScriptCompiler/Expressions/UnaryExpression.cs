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

        public override GeneralSymbol Evaluate()
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
                    GeneralSymbol variableSymbolMinus = _expression.Evaluate();
                    variableSymbolMinus.Minus();
                    return variableSymbolMinus;
                case Operator.Not:
                    if (_expression.Evaluate().Type != Enums.Type.Bool)
                        throw new Exception("! cannot be applied to type " + _expression.Evaluate().Type);
                    GeneralSymbol variableSymbolNot = _expression.Evaluate();
                    variableSymbolNot.Not();
                    return variableSymbolNot;

            }

            return null;
        }
    }
}
