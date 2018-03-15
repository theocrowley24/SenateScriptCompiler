using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler.Expressions
{
    abstract class Expression
    {
        public abstract GeneralSymbol Evaluate();
    }
}
