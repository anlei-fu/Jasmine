using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;
using System.Runtime.CompilerServices;

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void Excute()
        {
            CheckExpression.Excute();

            var match = (CheckExpression.Root.Output as JBool).Value;

            if (match)
            {
                Parent.SetMatchFound();
                Body.Excute();
            }
        }
       
    }
}
