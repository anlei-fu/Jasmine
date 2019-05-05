using GrammerTest.Grammer;
using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest
{
    public class DoWhileScopeBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public DoWhileScopeBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "DoWhileBuilder";

        public DoWhileBlock Build()
        {
            var doWhileBlock = new DoWhileBlock();

            if (!_reader.HasNext())
                throwError($"incompleted do-while block;");

            doWhileBlock.Body = new OrderedBlockBuilder(_reader,"do-while").Build();
           

            throwErrorIfHasNoNextAndNext($"incompleted do-while block;");

            throwIf(x => x.Value != Keywords.WHILE);

            throwErrorIfHasNoNextAndNext($"incompleted do-while block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);


            doWhileBlock.CheckExpression.Root = new AstNodeBuilder(_reader,_interceptChars).Build();


            return doWhileBlock;
        }
    }
}
