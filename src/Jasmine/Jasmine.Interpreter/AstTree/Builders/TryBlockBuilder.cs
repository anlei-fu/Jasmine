using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class TryBlockBuilder : BuilderBase
    {
        public TryBlockBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "TryBuilder";
        public TryBlock Build()
        {
            var tryBlock = new TryBlock(_block);

            tryBlock.Body = new OrderedBlockBuilder(_reader,"try",tryBlock).Build();

            return tryBlock;
        }
    }
}
