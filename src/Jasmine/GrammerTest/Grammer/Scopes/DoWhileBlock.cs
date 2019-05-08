using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class DoWhileBlock : BodyBlock
    {
        public DoWhileBlock(BreakableBlock parent) : base(parent)
        {
            CheckExpression = new Expression(parent);
        }

        public override string Name => base.Name+".DoWhileBlock";
        public Expression CheckExpression { get; set; }

        private bool _break;
        public override void Break()
        {
            _break = true;
        }
        public override void Continue()
        {

        }
        public override void Excute()
        {
            Body.Excute();

            CheckExpression.Excute();

            var result = CheckExpression.Root.Output as JBool;

            while(result.Value)
            {
                if (_break)
                    break;

                Body.Excute();

                result = CheckExpression.Root.Output as JBool;
            }

            _break = false;
        }
        
    }
}
