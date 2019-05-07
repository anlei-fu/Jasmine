using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public  class If0Block :BodyBlock
    {
        public If0Block(Block parent) : base(parent)
        {
            CheckExpression = new Expression(parent);
        }

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

        public bool IsMatch()
        {
            CheckExpression.Excute();

            return (bool)CheckExpression.Root.Output;
        }

        public override void Return(JObject result)
        {
            throw new System.NotImplementedException();
        }
    }
}
