using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class FinalyBlockBuilder : BuilderBase
    {
        public FinalyBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name =>"FinallyBuilder";

        public FinallyBlock Build()
        {
            var finallyBlock = new FinallyBlock();

            finallyBlock.Body = new OrderedBlockBuilder(_reader, "finally").Build();

            return finallyBlock;
        }
    }
}
