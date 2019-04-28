using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public abstract class BodyBlock:BreakableBlock
    {
        public override string Name => base.Name+".BodyBlock";
        public OrderdedBlock Body { get; set; }
    }
}
