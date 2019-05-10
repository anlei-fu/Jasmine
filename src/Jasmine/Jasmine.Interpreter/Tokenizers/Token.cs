using GrammerTest.Grammer;

namespace Jasmine.Interpreter.Tokenizers
{
    public class Token
    {
        internal Token(string value,TokenType type,int line,int lineNumber)
        {
            Value = value;
            TokenType = type;
            Line = line;
            LineNumber = lineNumber;
        }
        internal Token(OperatorType _operator, int line, int lineNumber)
        {
            TokenType = TokenType.Operator;

            OperatorType = _operator;
            Value = _operator.Tostring0();
            Line = line;
            LineNumber = lineNumber;
        }

        public int Line { get; internal set; }
        public int LineNumber { get;internal set; }

        public TokenType TokenType { get;internal set; }
        public OperatorType OperatorType { get; internal set; }
        public string Value { get; internal set; }

        public bool IsIdentifier() => TokenType == TokenType.Identifier;

        public bool IsOperator() => TokenType == TokenType.Operator;

        public bool IsNumber() => TokenType == TokenType.Number;
        public bool IsBool() => TokenType == TokenType.Bool;
        public bool IsString() => TokenType == TokenType.String;
        public bool IsNull() => TokenType == TokenType.Null;
    }
}
