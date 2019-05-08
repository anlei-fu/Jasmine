using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class WhileBlock : BodyBlock
    {
        public WhileBlock(BreakableBlock parent) : base(parent)
        {
            CheckExpression = new Expression(parent);
        }

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

            CheckExpression.Excute();

            var result = CheckExpression.Root.Output as JBool;
            
            while(result.Value)
            {
                if (_break)
                    break;

                Body.Excute();

                CheckExpression.Excute();

                 result = CheckExpression.Root.Output as JBool;
            }

            _break = false;
        }

     
    }
}
