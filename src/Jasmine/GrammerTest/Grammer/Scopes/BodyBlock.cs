using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
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

        public override void Return(JObject result)
        {
            Parent.Return(result);
        }

        public override void Continue()
        {
            Parent.Continue();
        }
        public override void Excute()
        {
            Body.Excute();
        }
    }
}
