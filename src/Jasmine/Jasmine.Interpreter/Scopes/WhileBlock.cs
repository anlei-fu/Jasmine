using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
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

            CheckExpression.Excute(ExcutingStack);

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
