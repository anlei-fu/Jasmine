using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
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
        public override void Excute(ExcutingStack stack)
        {
            Body.Excute(stack);

            CheckExpression.Excute(stack);

            var result = (bool)stack.Get(CheckExpression.Root);

            while(result)
            {
                if (_break)
                    break;

                Body.Excute(stack);

                CheckExpression.Excute(stack);

                result = (bool)stack.Get(CheckExpression.Root);
            }

            _break = false;
        }
        
    }
}
