using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ParenthesisBuilder:BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public ParenthesisBuilder(ISequenceReader<Token> reader, Block block) : base(reader, block)
        {
        }

        public override string Name => "ParenthesisBuilder";

        public OperatorNode Build()
        {
            return new AstNodeBuilder(_reader,_block,_interceptChars).Build();
        }
    }
}
