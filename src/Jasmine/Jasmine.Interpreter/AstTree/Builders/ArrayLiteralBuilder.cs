using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using static Jasmine.Interpreter.TypeSystem.Collections;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public  class ArrayLiteralBuilder:BuilderBase
    {
        public ArrayLiteralBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "ArrayLiteralBuilder";
        public Array Build()
        {
            return null;
        }
    }
}
