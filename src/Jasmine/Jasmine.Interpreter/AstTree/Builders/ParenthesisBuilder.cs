using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class ParenthesisBuilder:BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public ParenthesisBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "ParenthesisBuilder";
        public OperatorNode Build()
        {
            return new AstNodeBuilder(_reader,_block,_interceptChars).Build();
        }
    }
}
