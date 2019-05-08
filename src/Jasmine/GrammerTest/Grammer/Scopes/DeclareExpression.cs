using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public class DeclareExpression : Expression
    {
        public DeclareExpression(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => base.Name+".DeclareExpression";
    }
}
