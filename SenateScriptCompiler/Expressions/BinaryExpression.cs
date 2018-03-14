using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Enums;
using Type = SenateScriptCompiler.Enums.Type;

namespace SenateScriptCompiler.Expressions
{
    class BinaryExpression : Expression
    {
        private readonly Expression _expression1;
        private readonly Expression _expression2;
        private readonly Operator _operator;

        public BinaryExpression(Expression expression1, Expression expression2, Operator op)
        {
            _expression1 = expression1;
            _expression2 = expression2;
            _operator = op;
        }

        public override Symbol Evaluate()
        {
            
            if (_expression1.Evaluate().Type != _expression2.Evaluate().Type)
                throw new Exception("Type mismatch!");

            Symbol symbol = new Symbol();

            switch (_operator)
            {
                case Operator.Plus:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        symbol.Type = Type.Number;
                        symbol.NumberValue = _expression1.Evaluate().NumberValue + _expression2.Evaluate().NumberValue;
                    } else if (_expression1.Evaluate().Type == Type.String)
                    {
                        symbol.Type = Type.String;
                        symbol.StringValue = _expression1.Evaluate().StringValue + _expression2.Evaluate().StringValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return symbol;
                case Operator.Minus:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        symbol.Type = Type.Number;
                        symbol.NumberValue = _expression1.Evaluate().NumberValue - _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return symbol;
                case Operator.Multiply:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        symbol.Type = Type.Number;
                        symbol.NumberValue = _expression1.Evaluate().NumberValue * _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return symbol;
                case Operator.Divide:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        symbol.Type = Type.Number;
                        symbol.NumberValue = _expression1.Evaluate().NumberValue / _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return symbol;

            }

            return null;
        }

        public override void SetSymbol(Symbol symbol)
        {
            throw new NotImplementedException();
        }
    }
}
