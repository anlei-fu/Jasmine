using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public class ElseBlock : BodyBlock
    {
        public ElseBlock(Block parent) : base(parent)
        {
        }

        public override void Break()
        {
            ((BreakableBlock)Parent).Break();

        }

        public override void Catch(JError error)
        {
            ((BreakableBlock)Parent).Continue();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            Body.Excute();
        }

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
