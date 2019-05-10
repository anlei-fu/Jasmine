namespace Jasmine.Interpreter.Scopes
{
    public abstract class AbstractExcutor : IExcutor
    {
        public AbstractExcutor(BreakableBlock parent)
        {
            Parent = parent;
        }
        public virtual string Name => "AbstractExcutor";
        public abstract void Excute(ExcutingStack stack);
        public virtual BreakableBlock Parent { get; set; }
    }
}
