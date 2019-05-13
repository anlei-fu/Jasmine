using System.Runtime.CompilerServices;

namespace Jasmine.Interpreter.Scopes
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute(ExcutingStack stack)
        {
            CheckExpression.Excute(stack);

            var match = (bool)stack.Get(CheckExpression.Root);

            if (match)
            {
                Parent.SetMatchFound();
                Body.Excute(stack);
            }
        }
       
    }
}
