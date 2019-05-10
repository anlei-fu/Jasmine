 namespace Jasmine.Interpreter.Tokenizers.Exceptions
{
    public class IncompletedOperatorException : TokenizerExcption
    {
        public IncompletedOperatorException(int line, int lineNumber, string msg) : base(line, lineNumber, msg)
        {
        }
    }
}
