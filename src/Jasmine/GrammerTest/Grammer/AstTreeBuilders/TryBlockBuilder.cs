using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class TryBlockBuilder : BuilderBase
    {
        public TryBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "TryBuilder";

        public TryBlock Build()
        {
            var tryBlock = new TryBlock();

            tryBlock.Body = new OrderedBlockBuilder(_reader,"try").Build();

            return tryBlock;
        }
    }
}
