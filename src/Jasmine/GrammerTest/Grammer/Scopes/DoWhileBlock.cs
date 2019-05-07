using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class DoWhileBlock : BodyBlock
    {
        public DoWhileBlock(Block parent) : base(parent)
        {
            CheckExpression = new Expression(parent);
        }

        public override string Name => base.Name+".DoWhileBlock";
        public Expression CheckExpression { get; set; }

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
            throw new System.NotImplementedException();
        }
        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
