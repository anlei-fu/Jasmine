using Jasmine.Interpreter.Excutor;

namespace Jasmine.Interpreter.Scopes
{
    public interface  IInstructBuilder
    {
         Instruct Build(StackFrame interpreter);
    }
}
