using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public  class ObjectLiteralBuilder:BuilderBase
    {
        public ObjectLiteralBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "ObjectLiteralBuilder";
        public JObject Build()
        {
            return null;
        }
    }
}
