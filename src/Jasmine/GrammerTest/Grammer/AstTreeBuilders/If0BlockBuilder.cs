using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class If0BlockBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public If0BlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "If0Builder";

        public If0Block Build()
        {
            var if0Block = new If0Block();

            throwErrorIfHasNoNextAndNext("incompleted if block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);

            if0Block.CheckExpression.Root = new AstNodeBuilder(_reader, _interceptChars).Build();

            //check is out bool or object
            if (!if0Block.CheckExpression.Root.OutputType.IsBool())
                throwError("'if' inner check-expression requires a bool result,but it's not;");

            if0Block.Body = new OrderedBlockBuilder(_reader,"if").Build();

            return if0Block;


        }
    }
}
