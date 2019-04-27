using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class If0BlockBuilder : BuilderBase
    {

        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };
        public If0BlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public If0Block Build()
        {
            var if0Block = new If0Block();

            if (_reader.HasNext())
            {
                var token = _reader.Next();

                if (token.OperatorType != OperatorType.LeftParenthesis)
                    throwError("");

                var node = new AstNodeBuilder(_reader, _interceptChars).Build();

                var expression = new Expression();

                expression.Root = node;

                if0Block.CheckExpression = expression;

                if0Block.Block = new BlockBuilder(_reader).Build();

                return if0Block;

            }
            else
            {
                throwError("");
            }

            return if0Block;
        }
    }
}
