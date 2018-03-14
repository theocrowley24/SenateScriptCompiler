using System;
using System.Collections.Generic;
using System.Text;

namespace SenateScriptCompiler
{
    class Symbol
    {
        public Enums.Type Type;
        public String StringValue;
        public Double NumberValue;
        public Boolean BoolValue;

        public void Not()
        {
            BoolValue = !BoolValue;
        }

        public void Minus()
        {
            NumberValue *= -1;
        }
    }
}
