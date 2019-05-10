using Jasmine.Interpreter.AstTree;
using Jasmine.Interpreter.TypeSystem;
using System.Collections.Generic;

namespace Jasmine.Interpreter.Scopes
{
    public class ExcutingStack
    {
        private Dictionary<AstNode, Any> _map = new Dictionary<AstNode, Any>();
        public void Push(AstNode node,Any any)
        {

        }

        public Any Get(AstNode node)
        {
            return null;
        }

        public void Clear()
        {

        }

    }
}
