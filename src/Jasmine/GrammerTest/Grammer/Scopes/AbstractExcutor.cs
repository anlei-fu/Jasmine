using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public abstract class AbstractExcutor : IExcutor
    {
        public AbstractExcutor(BreakableBlock parent)
        {
            Parent = parent;
        }
        public virtual string Name => "AbstractExcutor";
        public abstract void Excute();
        public virtual BreakableBlock Parent { get; set; }
    }
}
