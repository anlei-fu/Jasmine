using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class ForBlockBuilder : BuilderBase
    {
        public ForBlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public ForBlock Build()
        {
            var forBlock = new ForBlock();

            if(_reader.HasNext())
            {
                var token = _reader.Next();

                if (token.OperatorType != OperatorType.LeftParenthesis)
                    throwError("");
                else
                {
                    throwErrorIfHasNoNextOrNext();

                    throwErrorIfOperatorTypeNotMatch(OperatorType.Var);

                    forBlock.DeclareExpression = new DeclareExpressionBuilder(_reader).Build();

                    forBlock.CheckExpression = new ExpressionBuilder(_reader).Build();

                    forBlock.OperateExpression = new ExpressionBuilder(_reader).Build();

                    if (_reader.HasNext())
                    {
                        token = _reader.Next();

                        /*
                         *  check () finished
                         */
                        if (token.OperatorType != OperatorType.RightParenthesis)
                            throwError("");

                        forBlock.Block = new BlockBuilder(_reader).Build();

                    }
                    else
                    {
                        throwError("");
                    }

                }
            }
            else
            {
                throwError("incompletd block");
            }

            return null;
        }
    }
}
