using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ElseBlockBuilder : BuilderBase
    {
        public ElseBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name =>"ElseBuilder";

        public ElseBlock Build()
        {
            var elseBlock = new ElseBlock();

            elseBlock.Body = new OrderedBlockBuilder(_reader,"else").Build();

            return elseBlock;
        }
    }
}
