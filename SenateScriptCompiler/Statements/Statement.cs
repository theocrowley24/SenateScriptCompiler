using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    abstract class Statement
    {
        public abstract bool Execute();
    }
}
