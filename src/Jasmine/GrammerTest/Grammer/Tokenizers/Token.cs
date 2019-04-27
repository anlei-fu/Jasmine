namespace Jasmine.Spider.Grammer
{
    public class Token
    {
        public Token(string value,TokenType type)
        {
            Value = value;
            TokenType = type;
        }
        public Token(OperatorType _operator)
        {
            TokenType = TokenType.Operator;

            OperatorType = _operator;
        }

        public int Line { get; set; }
        public int LineNumber { get; set; }

        public TokenType TokenType { get; set; }
        public OperatorType OperatorType { get; set; }
        public string Value { get; set; }

        public bool IsIdentifier() => TokenType == TokenType.Identifier;

        public bool IsOperator() => TokenType == TokenType.Operator;

        public bool IsNumber() => TokenType == TokenType.Number;
        public bool IsBool() => TokenType == TokenType.Bool;
        public bool IsString() => TokenType == TokenType.String;
        public bool IsNull() => TokenType == TokenType.Null;
    }
}
