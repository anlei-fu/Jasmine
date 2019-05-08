using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ElseBlockBuilder : BuilderBase
    {
        public ElseBlockBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name =>"ElseBuilder";

        public ElseBlock Build()
        {
            var elseBlock = new ElseBlock(_block);

            elseBlock.Body = new OrderedBlockBuilder(_reader,"else",elseBlock).Build();

            return elseBlock;
        }
    }
}
