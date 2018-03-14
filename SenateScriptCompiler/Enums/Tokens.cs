using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    enum Token
    {
        Print,
        Semi,
        String,
        Number,
        Bool,
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
        VariableName
    }
}
