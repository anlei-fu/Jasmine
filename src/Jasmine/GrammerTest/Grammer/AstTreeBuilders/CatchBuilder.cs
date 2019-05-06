using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class CatchBuilder : BuilderBase
    {
        public CatchBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "CatchBlockBuilder";

        public CatchBlock Build()
        {
            var catchBlock = new CatchBlock();

            throwErrorIfHasNoNextAndNext("incompleted catch block!");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);//(

            throwErrorIfHasNoNextAndNext("incompleted catch block!");

            if (_reader.Current().Value == "var")
            {


                throwErrorIfOperatorTypeNotMatch(OperatorType.Declare);//(var

                throwErrorIfHasNoNextAndNext("incompleted catch block!");//( var ex

                throwIf(x => !x.IsIdentifier());

                catchBlock.ErrorName = _reader.Current().Value;

                throwErrorIfHasNoNextAndNext("incompleted catch block!");

                throwErrorIfOperatorTypeNotMatch(OperatorType.RightParenthesis);//(var ex)
            }
            else if(_reader.Current().OperatorType!=OperatorType.RightParenthesis)
            {
                throwError("incorrect catch block");
            }


            catchBlock.Body = new OrderedBlockBuilder(_reader,"catch").Build();

            return catchBlock;

        }
    }
}
