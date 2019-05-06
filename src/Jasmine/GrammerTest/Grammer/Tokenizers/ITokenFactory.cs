using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.Tokenizers
{
    public interface ITokenFactory<T>
    {
        T Create(string value, TokenType type,int line,int lineNumber);
        T Create(OperatorType type, int line, int lineNumber);
    }
}
