namespace GrammerTest.Grammer.Tokenizers.Exceptions
{
    public class NotSurpportedOperatorException : TokenizerExcption
    {
        public NotSurpportedOperatorException(int line, int lineNumber, string msg) : base(line, lineNumber, msg)
        {
        }
    }
}
