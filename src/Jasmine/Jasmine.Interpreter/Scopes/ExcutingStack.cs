using Jasmine.Interpreter.AstTree;
using Jasmine.Interpreter.TypeSystem;
using System.Collections.Generic;

namespace Jasmine.Interpreter.Scopes
{
    public class ExcutingStack:IVariableTable
    {
       
        
        private Dictionary<AstNode, Any> _map = new Dictionary<AstNode, Any>();

        public IVariableTable Parnet { get; set; }

        public void Push(AstNode node,Any any)
        {
            if (!_map.ContainsKey(node))
                _map.Add(node, any);
            else
                _map[node] = any;
        }

        public Any Get(AstNode node)
        {
            return _map[node];
        }

        public void Clear()
        {

        }

        public void UnsetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Unset(string name)
        {
            throw new System.NotImplementedException();
        }

        public Any GetVariable(string name)
        {
            throw new System.NotImplementedException();
        }

        public Any Declare(string name)
        {
            throw new System.NotImplementedException();
        }

        public Any Declare(string name, Any obj)
        {
            throw new System.NotImplementedException();
        }

        public void Reset(string name, Any obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
