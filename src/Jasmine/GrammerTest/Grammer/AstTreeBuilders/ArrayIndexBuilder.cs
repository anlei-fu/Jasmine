using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ArrayIndexBuilder : BuilderBase
    {
        private static readonly string[] _intercptChars = new string[] {"]" };

        public ArrayIndexBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "ArrayIndexBuilder";

        public OperatorNode Build()
        {
           return  new AstNodeBuilder(_reader,_intercptChars).Build();
        }
    }
}
