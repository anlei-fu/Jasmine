using GrammerTest.Grammer.TypeSystem;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Scopes
{
    public class CatchBlock : BodyBlock
    {
        public CatchBlock(BreakableBlock parent) : base(parent)
        {
        }

        public string ErrorName { get; internal set; }
       

       

       
    }
}
