using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
  public  class ArrayLiteralBuilder:BuilderBase
    {
        public ArrayLiteralBuilder(ISequenceReader<Token> reader, Block block) : base(reader, block)
        {
        }

        public override string Name => "ArrayLiteralBuilder";

        public JArray Build()
        {
            return null;
        }
    }
}
