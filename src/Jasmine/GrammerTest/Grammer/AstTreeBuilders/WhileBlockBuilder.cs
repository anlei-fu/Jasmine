using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class WhileBlockBuilder : BuilderBase
    {
        private static readonly string[] _intercepChars = new string[]
        {
            ")"
        };
        public WhileBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public WhileBlock Build()
        {
            var whileBlock = new WhileBlock();

            throwErrorIfHasNoNextAndNext();

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);


            whileBlock.CheckExpression.Root = new AstNodeBuilder(_reader,_intercepChars).Build();

            if (!whileBlock.CheckExpression.Root.OutputType.IsBool())
                throwError("");

            throwErrorIfHasNoNextAndNext();

            if(_reader.CurrentToken.OperatorType==OperatorType.LeftBrace)
            {
                whileBlock.Body = new BlockBuilder(_reader).Build();
            }
            else
            {
                _reader.Previous();

                whileBlock.Body = new OrderdedBlock();

                whileBlock.Body.Children.Add(new ExpressionBuilder(_reader).Build());
            }


            return whileBlock;
        }
    }
}
