using System;
using System.Collections.Generic;
using System.Text;
using SenateScriptCompiler.Symbols;

namespace SenateScriptCompiler.Statements
{
    class FunctionCallStatement : Statement
    {
        private readonly String _functionName;
        private readonly List<Argument> _arguments;

        public FunctionCallStatement(string functionName, List<Argument> arguments)
        {
            _functionName = functionName;
            _arguments = arguments;
        }

        public override bool Execute()
        {
            FunctionSymbol functionSymbol = GlobalSymbolTable.table.GetFunctionSymbol(_functionName);

            if (functionSymbol.Arguments.Count != _arguments.Count)
                throw new Exception("Incorrect number of arguments passed to function");

            //Checks that arguments passed to function are what the function is expecting
            for (int i = 0; i < _arguments.Count; i++)
            {
                //Add more descriptive exception name
                if (_arguments[i].Value.Type != functionSymbol.Arguments[i].Value.Type)
                    throw new Exception("Incorrect type passed to function");

                //Creates every argument as a variable for the function to use
                VariableDeclerationStatement variable = new VariableDeclerationStatement(_arguments[i].Value.Type, functionSymbol.Arguments[i].ArgumentName, _arguments[i].Value);
                variable.Execute();
            }

            //Executes all function statements
            foreach (Statement statement in functionSymbol.Statements)
            {
                statement?.Execute();
            }

            return true;
        }
    }
}
