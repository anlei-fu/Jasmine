using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public  interface IVariableTable
    {
         IVariableTable Parnet { get; }
         void UnsetAll();
         void Unset(string name);
         Any GetVariable(string name);
         Any Declare(string name);
         Any Declare(string name, Any obj);
        void Reset(string name, Any obj);
    }
}
