using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;
using Jasmine.Interpreter.TypeSystem;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class ForeachBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };

        public ForeachBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader, block)
        {
        }

        public override string Name => "ForeachBuilder";

        public ForeachBlock Build()
        {
            var foreachBlock = new ForeachBlock(_block);

            /*
             *  resolve header
             * 
             */
             
            throwErrorIfHasNoNextOrNext("incompleted foreach block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);//(

            throwErrorIfHasNoNextOrNext("incompleted foreach block;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.Declare);//(var

            throwErrorIfHasNoNextOrNext("incompleted foreach block;");

            throwIf(x =>!x.IsIdentifier());//(var item

            var name = _reader.Current().Value;

            throwErrorIfHasNoNextOrNext("incompleted foreach block;");

            throwIf(x => x.Value != Keywords.IN);//(var item in

            foreachBlock.DeclareExpression = new DeclareExpression(_block)
            {
                Root = new DeclareOperator(_block)
            };

            foreachBlock.DeclareExpression.Root.Operands.Add(new OperandNode(new JString(name)));//(var item in 

            foreachBlock.GetCollectionExpression = new Expression(_block);

            foreachBlock.GetCollectionExpression.Root = new AstNodeBuilder(_reader,_block ,_interceptChars).Build();

            /*
             * Resolve body
             * 
             */

            if (!_reader.HasNext())
                throwErrorIfHasNoNextOrNext("incompleted foreach block;");

            foreachBlock.Body = new OrderedBlockBuilder(_reader,"foreach",foreachBlock).Build();

            return foreachBlock;

        }
    }
}
