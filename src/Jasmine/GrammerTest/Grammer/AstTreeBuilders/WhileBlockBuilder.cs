using GrammerTest.Grammer.AstTree;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class WhileBlockBuilder : BuilderBase
    {
        private static readonly string[] _intercepChars = new string[]
        {
            ")"
        };

        public WhileBlockBuilder(ISequenceReader<Token> reader,Block block) : base(reader,block)
        {
        }

        public override string Name => "WhileBuilder";

        public WhileBlock Build()
        {
            var whileBlock = new WhileBlock(_block);

            throwErrorIfHasNoNextAndNext("incompleted while block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);


            whileBlock.CheckExpression.Root = new AstNodeBuilder(_reader,_block,_intercepChars).Build();

            if (!whileBlock.CheckExpression.Root.OutputType.IsBool())
                throwError("inter check-expression requires a bool result,but it's not;");

            if (!_reader.HasNext())
                throwError("incompleted while block;");

            whileBlock.Body = new OrderedBlockBuilder(_reader,"while",whileBlock).Build();
          


            return whileBlock;
        }
    }
}
