using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ArrayIndexBuilder : BuilderBase
    {
        private static readonly string[] _intercptChars = new string[] {"]" };
        public ArrayIndexBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public OperatorNode Build()
        {
           return  new AstNodeBuilder(_reader,_intercptChars).Build();
        }
    }
}
