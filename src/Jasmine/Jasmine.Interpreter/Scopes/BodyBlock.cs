using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public abstract class BodyBlock:BreakableBlock
    {
        public BodyBlock(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => base.Name+".BodyBlock";
        public OrderdedBlock Body { get; set; }

        public override void Break()
        {
            Parent.Break();
        }

        public override void Catch(JError error)
        {
            Parent.Catch(error);
        }

        public override void Return(Any result)
        {
            Parent.Return(result);
        }

        public override void Continue()
        {
            Parent.Continue();
        }
        public override void Excute(ExcutingStack stack)
        {

            Body.Excute(stack);

        }
    }
}
