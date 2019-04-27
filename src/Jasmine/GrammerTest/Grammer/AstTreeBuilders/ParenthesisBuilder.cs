using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ParenthesisBuilder:BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };
        public ParenthesisBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public OperatorNode Build()
        {
            return new AstNodeBuilder(_reader,_interceptChars).Build();
        }
    }
}
