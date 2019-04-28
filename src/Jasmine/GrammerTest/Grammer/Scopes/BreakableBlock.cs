using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.TypeSystem;

namespace Jasmine.Spider.Grammer
{
    public abstract class BreakableBlock : Block
    {
        public override string Name => base.Name+".BreakableBlock";
        public abstract void Catch(JError error);
        public abstract void Break();
        public abstract void Continue();
        public abstract void Return(JObject result);
    }
}
