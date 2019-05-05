using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class ForBlockBuilder : BuilderBase
    {

        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public ForBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name =>"ForBuilder";

        public ForBlock Build()
        {
            var forBlock = new ForBlock();

            if(_reader.HasNext())
            {
                _reader.Next();
                var token = _reader.Current();

                if (token.OperatorType != OperatorType.LeftParenthesis)
                {
                    throwUnexceptedError();
                }
                else
                {
                    throwErrorIfHasNoNextAndNext("incompleted for block;");

                    throwErrorIfOperatorTypeNotMatch(OperatorType.Declare);

                    forBlock.DeclareExpression = new DeclareExpressionBuilder(_reader).Build();

                    forBlock.CheckExpression = new ExpressionBuilder(_reader, false).Build();

                    if (!forBlock.CheckExpression.Root.OutputType.IsBool())
                        throwError("second expression should be bool expression ,but it's not;");

                    forBlock.OperateExpression.Root = new AstNodeBuilder(_reader, _interceptChars).Build();

                    if (_reader.HasNext())
                    {

                        forBlock.Block = new OrderedBlockBuilder(_reader, "for").Build();

                    }
                    else
                    {
                        throwError("incompleted for block;");
                    }
                }
            }
            else
            {
                throwError("incompleted for block;");
            }

            return null;
        }
    }
}
