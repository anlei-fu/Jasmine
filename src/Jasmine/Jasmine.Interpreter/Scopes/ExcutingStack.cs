using Jasmine.Interpreter.TypeSystem;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Interpreter.Scopes
{
    public class ExcutingStack
    {
        private IVariableTable _currentVarible;
        private Stack<Any> _outputStack;
        public void PushNewVaribleTable()
        {

        }
        public void PopVaribleTable()
        {

        }
        public Any Pop()
        {
           return _outputStack.Pop();
        }
        public void Push(Any any)
        {
            _outputStack.Push(any);
        }


    }
}
