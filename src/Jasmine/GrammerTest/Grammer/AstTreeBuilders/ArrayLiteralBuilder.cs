using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using static GrammerTest.Grammer.TypeSystem.Collections;

namespace GrammerTest.Grammer
{
    public  class ArrayLiteralBuilder:BuilderBase
    {
        public ArrayLiteralBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name => "ArrayLiteralBuilder";

        public Array Build()
        {
            return null;
        }
    }
}
