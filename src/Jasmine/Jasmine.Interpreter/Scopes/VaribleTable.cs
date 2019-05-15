using Jasmine.Interpreter.TypeSystem;
using System;

namespace Jasmine.Interpreter.Scopes
{
    public class VaribleTable : IVariableTable
    {
        public IVariableTable Parnet => throw new NotImplementedException();

        public Any Declare(string name)
        {
            throw new NotImplementedException();
        }

        public Any Declare(string name, Any obj)
        {
            throw new NotImplementedException();
        }

        public Any GetVariable(string name)
        {
            throw new NotImplementedException();
        }

        public void Reset(string name, Any obj)
        {
            throw new NotImplementedException();
        }

        public void Unset(string name)
        {
            throw new NotImplementedException();
        }

        public void UnsetAll()
        {
            throw new NotImplementedException();
        }
    }
}
