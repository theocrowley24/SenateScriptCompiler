using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SenateScriptCompiler.Enums;
using SenateScriptCompiler.Expressions;
using SenateScriptCompiler.Statements;

namespace SenateScriptCompiler
{
    class Parser : Lexer
    {

        private readonly List<Statement> _statements;

        public Parser(string expression) : base(expression)
        {
            _statements = new List<Statement>();
        }

        public void Run()
        {
            foreach (Statement statement in _statements)
            {
                statement.Execute();
            }
        }

        public bool Parse()
        {
            while (CurrentToken != Token.EndFile)
            {
                GetNextToken();

                //Checks for a print statement

                Token[] acceptedPrintTokens = new[]
                {
                    Token.String,
                    Token.Bool,
                    Token.Number,
                    Token.VariableName
                };

                if (LastToken == Token.Print && LastToken != CurrentToken && acceptedPrintTokens.Contains(CurrentToken))
                {
                    _statements.Add(new PrintStatement(EvaluateExpression()));

                    if (CurrentToken != Token.Semi)
                    {
                        throw new Exception("Expecting a ;");
                    }
                }

                //Checks for variable decleration
                if (CurrentToken == Token.NumberVariable || CurrentToken == Token.StringVariable)
                {
                    GetNextToken();

                    if (CurrentToken == Token.VariableName)
                    {
                        GetNextToken();

                        if (CurrentToken == Token.VariableAssignment)
                        {
                            GetNextToken();

                            if (CurrentToken == Token.String)
                            {
                                _statements.Add(new VariableDeclerationStatement(VariableName, EvaluateExpression()));
                            } else if (CurrentToken == Token.Number)
                            {
                                _statements.Add(new VariableDeclerationStatement(VariableName, EvaluateExpression()));
                            }
                            else
                            {
                                throw new Exception("Unknown type");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid variable name");
                    }
                }

                //Checks for variable assignment
                if (CurrentToken == Token.VariableName)
                {
                    GetNextToken();

                    if (CurrentToken == Token.VariableAssignment)
                    {
                        GetNextToken();

                        if (CurrentToken == Token.String || CurrentToken == Token.Number || CurrentToken == Token.Bool)
                        {
                            if (CurrentToken == Token.String)
                            {
                                _statements.Add(new VariableAssignmentStatement(VariableName, EvaluateExpression()));
                            } else if (CurrentToken == Token.Number)
                            {
                                _statements.Add(new VariableAssignmentStatement(VariableName, EvaluateExpression()));
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown type");
                        }
                    }
                    else
                    {
                        throw new Exception("Missing = after variable name");
                    }
                }

            }

            return true;
        }

        public Expression EvaluateExpression()
        {

            Expression retValue = EvaluateTerm();

            while (CurrentToken == Token.Plus || CurrentToken == Token.Minus)
            {
                GetNextToken();

                var token = LastToken;

                Expression expression = EvaluateExpression();
                retValue = new BinaryExpression(retValue, expression, token == Token.Plus ? Operator.Plus : Operator.Minus);
            }

            return retValue;
        }

        public Expression EvaluateTerm()
        {
            Expression retValue = EvaluateFactor();

            while (CurrentToken == Token.Multiply || CurrentToken == Token.Divide)
            {
                GetNextToken();

                var token = LastToken;

                Expression expression = EvaluateTerm();

                retValue = new BinaryExpression(retValue, expression, token == Token.Multiply ? Operator.Multiply : Operator.Divide);
            }

            return retValue;
        }

        public Expression EvaluateFactor()
        {
            Expression retValue = null;

            if (CurrentToken == Token.Number)
            {
                retValue = new NumericalExpression(NumberValue);
                GetNextToken();
            }
            else if (CurrentToken == Token.OpenBracket)
            {
                GetNextToken();
                retValue = EvaluateExpression();

                if (CurrentToken != Token.CloseBracket)
                {
                    throw new Exception("Missing closing bracket");
                }

                GetNextToken();
            } else if (CurrentToken == Token.Plus || CurrentToken == Token.Minus)
            {
                GetNextToken();
                retValue = EvaluateFactor();

                retValue = new UnaryExpression(retValue, LastToken == Token.Plus ? Operator.Plus : Operator.Minus);
            } else if (CurrentToken == Token.String)
            {
                retValue = new StringExpression(StringValue);
                GetNextToken();
            } else if (CurrentToken == Token.VariableName)
            {
                retValue = new VariableExpression(VariableName);
                GetNextToken();
            }
            else
            {
                throw new Exception("Illegal token");
            }

            return retValue;
        }
    }
}
