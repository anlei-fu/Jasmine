using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.Scopes
{
    public  class ForBlock:BodyBlock
    {
        public ForBlock(BreakableBlock parent) : base(parent)
        {
            OperateExpression = new Expression(this);
        }

  
      
        public DeclareExpression DeclareExpression { get; set; }
        public Expression CheckExpression { get; set; }
        public Expression OperateExpression { get; set; }

        private  bool _break;
       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Break()
        {
            _break = true;
        }


        public override void Excute(ExcutingStack stack)
        {
            DeclareExpression.Excute(stack);

         
            while(true)
            {
                if (_break)
                    break;


                CheckExpression.Excute(stack);

                

                if (!(bool)stack.Get(CheckExpression.Root))
                {
                    break;
                }

                Body.Excute(stack);

           
                OperateExpression.Excute(stack);

            }

             UnsetAll();
            _break = false;
        }

      
    }
}
