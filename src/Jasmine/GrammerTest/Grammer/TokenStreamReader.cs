using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public  class TokenStreamReader
    {
        private Token[] _tokens;

        public Token Next(int step=1)
        {
            CurrentIndex = CurrentIndex + step;

            return CurrentToken;
        }

        public Token PreviousToken(int step=1)
        {
            return _tokens[CurrentIndex - step];
        }
        public Token Previous(int step=1)
        {
            CurrentIndex = CurrentIndex -step;

            return CurrentToken;
        }
        public bool HasNext(int step=1)
        {
            return CurrentIndex + step < _tokens.Length;
        }
        public int CurrentIndex { get; private set; }
        public Token CurrentToken => _tokens[CurrentIndex];
    }
}
