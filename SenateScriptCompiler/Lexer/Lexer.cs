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
        protected Token SecondLastToken;
        protected Token ThirdLastToken;

        protected String StringValue;
        protected Double NumberValue;
        protected String VariableName;
        protected String FunctionName;

        private readonly Dictionary<Token, String> _tokenValues;
        private readonly Dictionary<Token, char> _singleCharValues;

        public Lexer(String expression)
        {
            _expression = expression;
            _length = expression.Length;
            _index = 0;

            LexerHelper.LineNumber = 1;

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
                { Token.Or, "OR" },
                { Token.FunctionDef, "FUNC" },
                { Token.FunctionCall, "CALL" },
                { Token.Argument, "ARG" },
                { Token.Plus, "+" },
                { Token.Minus, "-" },
                { Token.Divide, "/" },
                { Token.Multiply, "*" },
                { Token.OpenBracket, "(" },
                { Token.CloseBracket, ")" },
                { Token.Not, "!" },
                { Token.VariableAssignment, "=" },
                { Token.OpenBrace, "{" },
                { Token.CloseBrace, "}" },
                { Token.Comma, "," }
            };

            _singleCharValues = new Dictionary<Token, char>
            {
                { Token.Plus, '+' },
                { Token.Minus, '-' },
                { Token.Divide, '/' },
                { Token.Multiply, '*' },
                { Token.OpenBracket, '(' },
                { Token.CloseBracket, ')' },
                { Token.Not, '!' },
                { Token.VariableAssignment, '=' },
                { Token.OpenBrace, '{' },
                { Token.CloseBrace, '}' },
                { Token.Comma, ',' }
            };
        }

        public void GetNextToken()
        {
            ThirdLastToken = SecondLastToken;
            SecondLastToken = LastToken;
            LastToken = CurrentToken;

            //Skips whitespace in expression
            while (_index < _length && (_expression[_index].ToString() == " " || _expression[_index] == '\t' || _expression[_index] == '\r' || _expression[_index] == '\n'))
            {

                if (_expression[_index] == '\n')
                    LexerHelper.LineNumber++;

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

                while (_expression[_index].ToString() != " " && _expression[_index] != ';' && _expression[_index] != ',')
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

            //Checks if current index is a single char token

            /*
            if (_singleCharValues.Any(x => x.Value == _expression[_index]))
            {
                CurrentToken = _singleCharValues.First(x => x.Value == _expression[_index]).Key;
                _index++;
                return;
            }
            */

            
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
                case '=':
                    CurrentToken = Token.VariableAssignment;
                    _index++;
                    return;
                case ';':
                    CurrentToken = Token.Semi;
                    _index++;
                    return;
                case '{':
                    CurrentToken = Token.OpenBrace;
                    _index++;
                    return;
                case '}':
                    CurrentToken = Token.CloseBrace;
                    _index++;
                    return;
                case ',':
                    CurrentToken = Token.Comma;
                    _index++;
                    return;
            }
            

            //Checks if current index of expression is start of variable name
            if (_expression[_index] == '$')
            {
                string variableName = "";

                string[] charactersToIgnore = { " ", ";", ")", "+", "!", "(", "-", "*", "/", "{", "\r", "\n", "," };

                while (!charactersToIgnore.Contains(_expression[_index].ToString()))
                {
                    variableName += _expression[_index];
                    _index++;
                }

                CurrentToken = Token.VariableName;
                VariableName = variableName;

                return;
            }

            //Checks if current inde of expression is start of function name
            if (_expression[_index] == '@')
            {
                string functionName = "";

                string[] charactersToIgnore = { " ", ";", ")", "+", "!", "(", "-", "*", "/", "\r", "\n" };

                while (!charactersToIgnore.Contains(_expression[_index].ToString()))
                {
                    functionName += _expression[_index];
                    _index++;
                }

                CurrentToken = Token.FunctionName;
                FunctionName = functionName;

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
        }
    }
}
