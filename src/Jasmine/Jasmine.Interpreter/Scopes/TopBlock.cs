using Jasmine.Interpreter.Scopes.Exceptions;
using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public class TopBlock : BreakableBlock
    {
        public TopBlock(BreakableBlock parent) : base(parent)
        {
        }
        public TopBlock():base(null)
        {

        }

        public override void Break()
        {
            throw new InvalidGrammerException();
        }

        public override void Catch(JError error)
        {
            throw new InvalidGrammerException();
        }

        public override void Continue()
        {
            throw new InvalidGrammerException();
        }

        public override void Excute()
        {
            throw new System.NotImplementedException();
        }

        public override void Return(Any result)
        {
            throw new InvalidGrammerException();
        }
    }
}
