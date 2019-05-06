using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ForeachBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ")"
        };
        public ForeachBuilder(TokenStreamReader reader) : base(reader)
        {
        }


        public ForeachBlock Build()
        {
            var foreachBlock = new ForeachBlock();


            /*
             *  resolve header
             * 
             */
             
            throwErrorIfHasNoNextOrNext();

            throwErrorIfOperatorTypeNotMatch(OperatorType.LeftParenthesis);//(

            throwErrorIfHasNoNextOrNext();

            throwErrorIfOperatorTypeNotMatch(OperatorType.Var);//(var

            throwErrorIfHasNoNextOrNext();

            throwIf(x => x.IsIdentifier(), " sytanx error");//(var item

            var name = _reader.CurrentToken.Value;

            throwErrorIfHasNoNextOrNext();

            throwIf(x => x.Value != Keywords.IN);//(var item in

            foreachBlock.DeclareExpression = new DeclareExpression()
            {
                Root = new DeclareOperator()
            };

            foreachBlock.DeclareExpression.Root.Operands.Add(new OperandNode(new JString(name)));//(var item in 


            foreachBlock.GetCollectionExpression = new Expression()
            {
                Root = new AstNodeBuilder(_reader, _interceptChars).Build()
            };

            throwErrorIfHasNoNextOrNext();

            var token = _reader.Next();


            /*
             * Resolve body
             * 
             */ 

            if (token.OperatorType == OperatorType.LeftBrace)
            {
                foreachBlock.Body = new BlockBuilder(_reader).Build();
            }
            else
            {
                _reader.Previous();

                foreachBlock.Body = new OrderdedBlock();

                var exp = new ExpressionBuilder(_reader).Build();
                foreachBlock.Body.Children.Add(exp);
            }


            return foreachBlock;


        }
    }
}
