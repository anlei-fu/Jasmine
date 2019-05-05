using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Tokenizers
{
    public class TokenFactory : ITokenFactory<Token>
    {
        private TokenFactory()
        {

        }
        public static readonly TokenFactory Instance = new TokenFactory();
        public Token Create(string value, TokenType type, int line, int lineNumber)
        {
            return new Token(value, type, line, lineNumber);
        }

        public Token Create(OperatorType type, int line, int lineNumber)
        {
            return new Token(type, line, lineNumber);
        }
    }
}
