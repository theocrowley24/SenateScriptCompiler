using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Symbols;

namespace SenateScriptCompiler.Statements
{
    class FunctionDefinitionStatement : Statement
    {
        private readonly List<Argument> _arguments;
        private readonly String _functionName;

        internal List<Statement> Statements { get; set; }

        public FunctionDefinitionStatement(List<Statement> statements, List<Argument> arguments, string functionName)
        {
            Statements = statements;
            _functionName = functionName;
            _arguments = arguments;
        }

        public override bool Execute()
        {
            FunctionSymbol functionSymbol = new FunctionSymbol
            {
                Arguments = _arguments,
                Statements = Statements
            };

            GlobalSymbolTable.table.AddToFunctionTable(_functionName, functionSymbol);

            return true;
        }
    }
}
