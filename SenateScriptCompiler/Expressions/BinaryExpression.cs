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

        public override GeneralSymbol Evaluate()
        {
            
            if (_expression1.Evaluate().Type != _expression2.Evaluate().Type)
                throw new Exception("Type mismatch!");

            GeneralSymbol variableSymbol = new GeneralSymbol();

            switch (_operator)
            {
                case Operator.Plus:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        variableSymbol.Type = Type.Number;
                        variableSymbol.NumberValue = _expression1.Evaluate().NumberValue + _expression2.Evaluate().NumberValue;
                    } else if (_expression1.Evaluate().Type == Type.String)
                    {
                        variableSymbol.Type = Type.String;
                        variableSymbol.StringValue = _expression1.Evaluate().StringValue + _expression2.Evaluate().StringValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;
                case Operator.Minus:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        variableSymbol.Type = Type.Number;
                        variableSymbol.NumberValue = _expression1.Evaluate().NumberValue - _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;
                case Operator.Multiply:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        variableSymbol.Type = Type.Number;
                        variableSymbol.NumberValue = _expression1.Evaluate().NumberValue * _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;
                case Operator.Divide:
                    if (_expression1.Evaluate().Type == Type.Number)
                    {
                        variableSymbol.Type = Type.Number;
                        variableSymbol.NumberValue = _expression1.Evaluate().NumberValue / _expression2.Evaluate().NumberValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;

                case Operator.And:
                    if (_expression1.Evaluate().Type == Type.Bool)
                    {
                        variableSymbol.Type = Type.Bool;
                        variableSymbol.BoolValue = _expression1.Evaluate().BoolValue && _expression2.Evaluate().BoolValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;
                case Operator.Or:
                    if (_expression1.Evaluate().Type == Type.Bool)
                    {
                        variableSymbol.Type = Type.Bool;
                        variableSymbol.BoolValue = _expression1.Evaluate().BoolValue || _expression2.Evaluate().BoolValue;
                    }
                    else
                    {
                        throw new Exception("Incorrect type");
                    }

                    return variableSymbol;

            }

            return null;
        }

    }
}
