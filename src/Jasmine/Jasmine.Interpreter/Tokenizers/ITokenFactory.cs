namespace Jasmine.Interpreter.Tokenizers
{
    public interface ITokenFactory<T>
    {
        T Create(string value, TokenType type,int line,int lineNumber);
        T Create(OperatorType type, int line, int lineNumber);
    }
}
