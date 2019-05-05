using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
  public  class ArrayLiteralBuilder:BuilderBase
    {
        public ArrayLiteralBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "ArrayLiteralBuilder";

        public JArray Build()
        {
            return null;
        }
    }
}
