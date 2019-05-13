using Jasmine.Interpreter.AstTree;

namespace Jasmine.Interpreter.Scopes
{
    public class Expression:AbstractExcutor
    {
        public Expression(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => base.Name+"Expression";
        public OperatorNode Root { get; set; }
        public override void Excute(ExcutingStack stack)
        {
            Root.Excute(stack);
        }
    }
}
