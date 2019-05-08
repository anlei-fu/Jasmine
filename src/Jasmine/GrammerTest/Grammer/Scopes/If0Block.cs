using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public  class If0Block :BodyBlock
    {
        public If0Block(IfBlock parent) : base(parent)
        {
            Parent = parent;
            CheckExpression = new Expression(parent);
        }
        public new IfBlock Parent { get; }
        public Expression CheckExpression { get; set; } 

        public override void Break()
        {
            ((BreakableBlock)Parent).Break();

        }

        public override void Catch(JError error)
        {
            throw new System.NotImplementedException();
        }

        public override void Continue()
        {
            ((BreakableBlock)Parent).Continue();
        }

        public override void Excute()
        {
            CheckExpression.Excute();

            if((bool)CheckExpression.Root.Output)
            {
                Parent.SetMatchFound();
                Body.Excute();
            }
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
