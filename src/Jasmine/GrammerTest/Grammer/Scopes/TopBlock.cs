using GrammerTest.Grammer.Scopes.Exceptions;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
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

        public override void Return(JObject result)
        {
            throw new InvalidGrammerException();
        }
    }
}
