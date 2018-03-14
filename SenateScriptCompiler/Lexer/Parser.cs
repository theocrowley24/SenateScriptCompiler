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
            DateTime startTime = DateTime.Now;

            //Executes every statement in sequence
            foreach (Statement statement in _statements)
            {
                statement.Execute();
            }

            Console.WriteLine();
            Console.WriteLine("Time taken to execute: " + (DateTime.Now - startTime));
        }

        public bool Parse()
        {
            DateTime startTime = DateTime.Now;

            //Loops until it reaches the end of the file
            while (CurrentToken != Token.EndFile)
            {
                GetNextToken();


                //Array containing all accepted tokens by the print statement
                Token[] acceptedPrintTokens = new[]
                {
                    Token.String,
                    Token.BoolTrue,
                    Token.BoolFalse,
                    Token.Number,
                    Token.VariableName,
                    Token.Not,
                    Token.Minus
                };

                //PRINT statement check/parse
                if (LastToken == Token.Print && LastToken != CurrentToken && acceptedPrintTokens.Contains(CurrentToken))
                {
                    _statements.Add(new PrintStatement(EvaluateExpression()));

                    if (CurrentToken != Token.Semi)
                    {
                        throw new Exception("Expecting a ;");
                    }
                }

                //Checks for variable decleration
                if (CurrentToken == Token.NumberVariable || CurrentToken == Token.StringVariable || CurrentToken == Token.BoolVariable)
                {
                    GetNextToken();

                    if (CurrentToken == Token.VariableName)
                    {
                        GetNextToken();

                        if (CurrentToken == Token.VariableAssignment)
                        {
                            GetNextToken();

                            if (CurrentToken == Token.String || CurrentToken == Token.Number || CurrentToken == Token.BoolFalse || CurrentToken == Token.BoolTrue || CurrentToken == Token.Minus)
                            {
                                //Finds the type of variable which is being declared
                                Enums.Type type = Enums.Type.Null;

                                Dictionary<Token, Enums.Type> tokenConversion = new Dictionary<Token, Enums.Type>
                                {
                                    { Token.StringVariable, Enums.Type.String },
                                    { Token.NumberVariable, Enums.Type.Number },
                                    { Token.BoolVariable, Enums.Type.Bool }
                                };

                                type = tokenConversion[ThirdLastToken];

                                _statements.Add(new VariableDeclerationStatement(type, VariableName, EvaluateExpression()));
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
                if (CurrentToken == Token.VariableName && LastToken == Token.Semi)
                {
                    GetNextToken();

                    if (CurrentToken == Token.VariableAssignment)
                    {
                        GetNextToken();

                        if (CurrentToken == Token.String || CurrentToken == Token.Number || CurrentToken == Token.BoolTrue || CurrentToken == Token.BoolFalse || CurrentToken == Token.Not || CurrentToken == Token.VariableName)
                        {
                            _statements.Add(new VariableAssignmentStatement(VariableName, EvaluateExpression()));
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

                if (CurrentToken == Token.VariableName && (LastToken != Token.BoolVariable || LastToken != Token.StringVariable || LastToken != Token.NumberVariable))
                {
                    throw new Exception("Variable does not exists in scope");
                }

            }

            Console.WriteLine("Time taken to compile: " + (DateTime.Now - startTime) + "\n");
            return true;
        }

        public Expression EvaluateExpression()
        {

            Expression retValue = EvaluateTerm();

            while (CurrentToken == Token.Plus || CurrentToken == Token.Minus || CurrentToken == Token.Or || CurrentToken == Token.And)
            {
                GetNextToken();

                var token = LastToken;

                Expression expression = EvaluateExpression();

                Dictionary<Token, Operator> tokenConversion = new Dictionary<Token, Operator>
                {
                    { Token.Plus, Operator.Plus },
                    { Token.Minus, Operator.Minus },
                    { Token.Or, Operator.Or },
                    { Token.And, Operator.And },
                };

                retValue = new BinaryExpression(retValue, expression, tokenConversion[token]);
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
            } else if (CurrentToken == Token.BoolTrue)
            {
                retValue = new BoolExpression(true);
                GetNextToken();
            } else if (CurrentToken == Token.BoolFalse)
            {
                retValue = new BoolExpression(false);
                GetNextToken();
            } else if (CurrentToken == Token.Not)
            {
                GetNextToken();
                retValue = EvaluateFactor();

                retValue = new UnaryExpression(retValue, Operator.Not);
            }
            else
            {
                throw new Exception("Illegal token");
            }

            return retValue;
        }
    }
}
