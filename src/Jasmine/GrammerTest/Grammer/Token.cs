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

        public TokenType TokenType { get; set; }
        public OperatorType OperatorType { get; set; }
        public string Value { get; set; }
    }
}
