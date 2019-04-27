using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class DoWhileBlock : BreakableBlock
    {
        public Expression CheckExpression { get; set; }
        public Block Block { get; set; }
        public override void Break()
        {
            base.Break();
        }
        public override void Continue()
        {
            base.Continue();
        }


    }
}
