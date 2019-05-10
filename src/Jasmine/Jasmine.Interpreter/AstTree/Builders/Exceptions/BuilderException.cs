using System;

namespace Jasmine.Interpreter.AstTree.Builders.Exceptions
{
    public  class BuilderException:Exception
    {
        public BuilderException(string msg, string builderName, int line,int lineNumber)
               :base($"Syntax error:{msg},In builder {builderName},At line {line},At line number {lineNumber}")
        {

        }
    }
}
