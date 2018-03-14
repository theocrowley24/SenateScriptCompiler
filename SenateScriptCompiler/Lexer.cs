using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SenateScriptCompiler.Helpers;

namespace SenateScriptCompiler
{
    class Lexer
    {
        private readonly String _expression;
        private int _index;
        private readonly int _length;

        protected Token CurrentToken;
        protected Token LastToken;

        protected String StringValue;
        protected Double NumberValue;
        protected Boolean BoolValue;
        protected String VariableName;

        private readonly Dictionary<Token, String> _tokenValues;

        public Lexer(String expression)
        {
            _expression = expression;
            _length = expression.Length;
            _index = 0;

            _tokenValues = new Dictionary<Token, string>
            {
                { Token.Print, "PRINT" },
                { Token.Semi, ";" },
                { Token.NumberVariable, "NUMBER" },
                { Token.BoolVariable, "BOOL" },
                { Token.StringVariable, "STRING" },
                { Token.BoolTrue, "TRUE" },
                { Token.BoolFalse, "FALSE" },
                { Token.And, "AND" },
                { Token.Or, "OR" }
            };
        }

        public void GetNextToken()
        {
            
            LastToken = CurrentToken;

            //Skips whitespace in expression
            while (_index < _length && (_expression[_index].ToString() == " " || _expression[_index] == '\t' || _expression[_index] == '\r' || _expression[_index] == '\n'))
            {
                _index++;
            }

            if (_index == _length)
            {
                CurrentToken = Token.EndFile;
                return;
            }

            //Check if current index of expression is a statement
            if (LexerHelper.IsStartOfStatement(_expression[_index]))
            {
                string statementString = "";

                while (_expression[_index].ToString() != " " && _expression[_index] != ';')
                {
                    statementString += _expression[_index];
                    _index++;
                }

                if (_tokenValues.Any(x => x.Value == statementString))
                {
                    CurrentToken = _tokenValues.First(x => x.Value == statementString).Key;
                    return;
                }
            }


            //Checks if current index of expression is start of a quoted string expression
            if (LexerHelper.IsStartOfString(_expression[_index]))
            {
                string str = "";
                _index++;

                while (_expression[_index] != '\"')
                {
                    str += _expression[_index];
                    _index++;
                }

                CurrentToken = Token.String;
                StringValue = str;

                _index++;
                return;
            }

            //Checks if current index is a mathmematical operator
            switch (_expression[_index])
            {
                case '+':
                    CurrentToken = Token.Plus;
                    _index++;
                    return;
                case '-':
                    CurrentToken = Token.Minus;
                    _index++;
                    return;
                case '/':
                    CurrentToken = Token.Divide;
                    _index++;
                    return;
                case '*':
                    CurrentToken = Token.Multiply;
                    _index++;
                    return;
                case '(':
                    CurrentToken = Token.OpenBracket;
                    _index++;
                    return;
                case ')':
                    CurrentToken = Token.CloseBracket;
                    _index++;
                    return;
                case '!':
                    CurrentToken = Token.Not;
                    _index++;
                    return;
            }

            //Checks if current index of expression is start of variable name
            if (LexerHelper.IsStartOfVariableName(_expression[_index]))
            {
                string variableName = "";

                while (LexerHelper.IsStartOfVariableName(_expression[_index]))
                {
                    variableName += _expression[_index];
                    _index++;
                }
                
                CurrentToken = Token.VariableName;
                VariableName = variableName;

                return;

            }

            if (_expression[_index] == '=')
            {
                CurrentToken = Token.VariableAssignment;
                _index++;
                return;
            }

            //Checks if current index of expression is start of a numerical value
            if (LexerHelper.IsNumber(_expression[_index]))
            {
                string number = "";

                while (LexerHelper.IsNumber(_expression[_index]))
                {
                    number += _expression[_index];
                    _index++;
                }

                CurrentToken = Token.Number;
                NumberValue = Double.Parse(number);

                return;
            }

            //Checks if current index is semi colon
            if (_expression[_index] == ';')
            {
                CurrentToken = Token.Semi;
                _index++;
                return;
            }
        }
    }
}
