using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    enum Token
    {
        Illegal,
        Print,
        Semi,
        String,
        Number,
        BoolTrue,
        BoolFalse,
        EndFile,
        Plus,
        Minus,
        Multiply,
        Divide,
        OpenBracket,
        CloseBracket,
        VariableAssignment,
        NumberVariable,
        StringVariable,
        BoolVariable,
        VariableName,
        And,
        Or,
        Not,
        FunctionDef,
        FunctionCall,
        OpenBrace,
        CloseBrace,
        Argument,
        ArgumentName,
        FunctionName
    }
}
