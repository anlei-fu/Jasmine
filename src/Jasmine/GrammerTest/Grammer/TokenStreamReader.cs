using Jasmine.Spider.Grammer;
using System.Collections.Generic;

namespace GrammerTest.Grammer
{
    public class TokenStreamReader
    {

        public TokenStreamReader(List<Token> tokens)
        {
            _tokens = tokens;
        }

        private List<Token> _tokens;
        public int CurrentIndex { get; private set; } = -1;
        public bool HasNext(int step=1)
        {
            return CurrentIndex+step<_tokens.Count;
        }

        public Token PreviouceToken(int step=1)
        {
            return _tokens[CurrentIndex - step];
        }


        public Token Next(int step=1)
        {
            CurrentIndex += step;
            return  _tokens[CurrentIndex];
        }

        public Token Previous(int step=1)
        {
            CurrentIndex -= step;
            return _tokens[CurrentIndex];
        }


        public Token CurrentToken => _tokens[CurrentIndex];
    }
}
