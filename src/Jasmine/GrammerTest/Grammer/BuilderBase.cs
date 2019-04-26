using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class BuilderBase
    {
        public  BuilderBase(TokenStreamReader reader)
        {
            _reader = reader;
        }
        protected TokenStreamReader _reader;

        protected OperatorNode _currentNode;
       

        protected void throwError(string msg)
        {

        }
    }
}
