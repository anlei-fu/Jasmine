namespace Jasmine.Interpreter.Tokenizers.Exceptions
{
    public class InvalidIdentifierCharException : TokenizerExcption
    {
        public InvalidIdentifierCharException(int line, int lineNumber, string msg) : base(line, lineNumber, msg)
        {
        }
    }
}
