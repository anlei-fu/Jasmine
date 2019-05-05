using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class ObjectLiteralBuilder:BuilderBase
    {
        public ObjectLiteralBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "ObjectLiteralBuilder";

        public JObject Build()
        {
            return null;
        }
    }
}
