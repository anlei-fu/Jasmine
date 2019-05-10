using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class DoWhileScopeBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public DoWhileScopeBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name => "DoWhileBuilder";

        public DoWhileBlock Build()
        {
            var doWhileBlock = new DoWhileBlock(_block);

            if (!_reader.HasNext())
                throwError($"incompleted do-while block;");

            doWhileBlock.Body = new OrderedBlockBuilder(_reader,"do-while",_block).Build();
           
            throwErrorIfHasNoNextOrNext($"incompleted do-while block;");

            throwIf(x => x.Value != Keywords.WHILE);

            throwErrorIfHasNoNextOrNext($"incompleted do-while block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);

            doWhileBlock.CheckExpression.Root = new AstNodeBuilder(_reader,_block,_interceptChars).Build();

            return doWhileBlock;
        }
    }
}
