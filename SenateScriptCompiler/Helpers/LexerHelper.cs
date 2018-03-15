
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SenateScriptCompiler.Helpers
{
    class LexerHelper
    {
        public static int LineNumber;

        public static bool IsStartOfStatement(char c)
        {
            char[] statementCharacters =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
            };

            return statementCharacters.Contains(c);
        }


        public static bool IsStartOfString(char c)
        {
            return c == '\"';
        }

        public static bool IsNumber(char c)
        {
            char[] numbers =
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

            return numbers.Contains(c);
        }

        public static bool IsStartOfVariableName(char c)
        {
            char[] variableCharacters =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
            };

            return variableCharacters.Contains(c);
        }

        public static bool IsStartOfBool(char c)
        {
            return c == 'T' || c == 'F';
        }
    }
}
