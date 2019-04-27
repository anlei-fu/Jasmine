using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class PriorityBuilder : BuilderBase
    {
        private int _level;
        public PriorityBuilder(TokenStreamReader reader) : base(reader)
        {
        }

       public AstNode Build()
        {
            return null;
        }
    }
}
