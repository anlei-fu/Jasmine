namespace Jasmine.Interpreter.Scopes
{
    public class DeclareExpression : Expression
    {
        public DeclareExpression(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => base.Name+".DeclareExpression";
    }
}
