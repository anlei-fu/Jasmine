using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.Scopes
{
    public abstract class BreakableBlock : Block
    {
        public BreakableBlock(BreakableBlock parent) : base(parent)
        {
        }

        public override string Name => base.Name+".BreakableBlock";
        public abstract void Catch(JError error);
        public abstract void Break();
        public abstract void Continue();
        public abstract void Return(Any result);
    }
}
