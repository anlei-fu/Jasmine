using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;

namespace GrammerTest
{
    public class DoWhileScopeBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };
        public DoWhileScopeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public DoWhileBlock Build()
        {
            var doWhileBlock = new DoWhileBlock();

            throwErrorIfHasNoNextOrNext();

            if(_reader.CurrentToken.OperatorType!=OperatorType.LeftBrace)
            {
                _reader.Previous();

                doWhileBlock.Body.Children.Add(new ExpressionBuilder(_reader).Build());
            }
            else
            {
                doWhileBlock.Body = new BlockBuilder(_reader).Build();
            }

            throwErrorIfHasNoNextOrNext();

            throwIf(x => x.Value != Keywords.WHILE);

            throwErrorIfHasNoNextOrNext();

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);


            doWhileBlock.CheckExpression.Root = new AstNodeBuilder(_reader,_interceptChars).Build();


            return doWhileBlock;
        }
    }
}
