using Jasmine.Spider.Grammer;
using System;
using System.Diagnostics;

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
            Debug.Assert(false);
        }

        protected void throwErrorIfHasNoNextOrNext(string msg="")
        {
            if (!_reader.HasNext())
                Debug.Assert(false);

            _reader.Next();
        }
        protected void throwErrorIfOperatorTypeNotMatch(OperatorType type, string msg="")
        {

            if (_reader.CurrentToken.OperatorType != type)
                Debug.Assert(false);
        }
        protected void throwIf(Func<Token,bool> predict,string msg="")
        {
            if (predict(_reader.CurrentToken))
                Debug.Assert(false);
        }
    }
}
