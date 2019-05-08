using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class FunctionBlock : BodyBlock
    {
        public const string RETURN = "__RETURN__";
        public FunctionBlock(BreakableBlock parent) : base(parent)
        {
        }

        public override void Break()
        {
            throw new System.NotImplementedException();
        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            throw new System.NotImplementedException();
        }

        public override void Excute()
        {
            Declare(RETURN, new JObject());

            Body.Excute();
        }

        public override void Return(JObject result)
        {
            Reset(RETURN, result);
        }
    }
}
