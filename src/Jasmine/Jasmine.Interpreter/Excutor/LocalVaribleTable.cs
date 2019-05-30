using Jasmine.Interpreter.TypeSystem;
using System.Collections.Generic;

namespace Jasmine.Interpreter.Excutor
{
    public   class LocalVaribleTable
    {
        public LocalVaribelTableScope Scope { get; set; }
        public LocalVaribleTable Parent { get; set; }

        private IDictionary<string, LocalVaribleTable> _fastCache;
        public void Declare(string name,Any value)
        {

        }

        public void Reset(string name,Any value)
        {

        }

        public Any Get(string name)
        {
            return null;
        }

        public void Unset(string name)
        {

        }
    }

    public enum LocalVaribelTableScope
    {
        Block,
        Loop,
        Function
    }
}
