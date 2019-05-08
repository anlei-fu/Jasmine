using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class FinalyBlockBuilder : BuilderBase
    {
        public FinalyBlockBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name =>"FinallyBuilder";

        public FinallyBlock Build()
        {
            var finallyBlock = new FinallyBlock(_block);

            finallyBlock.Body = new OrderedBlockBuilder(_reader, "finally",finallyBlock).Build();

            return finallyBlock;
        }
    }
}
