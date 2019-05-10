using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class CatchBuilder : BuilderBase
    {
        public CatchBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }
        public override string Name => "CatchBlockBuilder";
        public CatchBlock Build()
        {
            var catchBlock = new CatchBlock(_block);

            throwErrorIfHasNoNextOrNext("incompleted catch block!");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);//(

            throwErrorIfHasNoNextOrNext("incompleted catch block!");

            if (_reader.Current().Value == "var")
            {
                throwErrorIfOperatorTypeNotMatch(OperatorType.Declare);//(var

                throwErrorIfHasNoNextOrNext("incompleted catch block!");//( var ex

                throwIf(x => !x.IsIdentifier());

                catchBlock.ErrorName = _reader.Current().Value;

                throwErrorIfHasNoNextOrNext("incompleted catch block!");

                throwErrorIfOperatorTypeNotMatch(OperatorType.RightParenthesis);//(var ex)
            }
            else if(_reader.Current().OperatorType!=OperatorType.RightParenthesis)
            {
                throwError("incorrect catch block");
            }

            catchBlock.Body = new OrderedBlockBuilder(_reader,"catch",catchBlock).Build();

            return catchBlock;

        }
    }
}
