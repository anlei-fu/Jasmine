using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ElseBlockBuilder : BuilderBase
    {
        public ElseBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public ElseBlock Build()
        {
            var elseBlock = new ElseBlock();

            elseBlock.Body = new BlockBuilder(_reader).Build();

            return elseBlock;
        }
    }
}
