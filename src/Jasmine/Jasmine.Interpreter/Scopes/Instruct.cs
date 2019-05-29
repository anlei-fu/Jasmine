using Jasmine.Interpreter.Excutor;
using Jasmine.Interpreter.Tokenizers;
using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public abstract class Instruct : IExcutor
    {
        public abstract string Name { get; }

        public Any[] Parameters { get; set; }
        public OperatorType OperatorType { get; set; }
        public abstract void Excute(StackFrame interpreter);
    }

   




}
