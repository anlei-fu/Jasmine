using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class DeclareExpressionBuilder : BuilderBase
    {
        private static readonly string[] _intercptChars = new string[]
        {
            ";"
        };

        public DeclareExpressionBuilder(ISequenceReader<Token> reader,Block block) : base(reader,block)
        {
        }

        public override string Name => "DeclareExpressionBuilder";

       

        private void requirePreviousIsIdentifier()
        {
            if (_reader.Last().TokenType != TokenType.Identifier)
                throwError("");
        }

        public DeclareExpression Build()
        {
            var expression = new DeclareExpression(_block);

            var node = new DeclareOperator(_block);

            while (_reader.HasNext())
            {
                _reader.Next();

                var token = _reader.Current();

                /*
                 *  In this loop,tokens type  can only be ',', 'identifer',';','='
                 *  ',',';','=', requires previous token must be an  Identifier
                 * 
                 */ 

                if (token.TokenType == TokenType.Identifier)
                {
                    node.Operands.Add(new OperandNode(new JString(token.Value)));
                }
                else if (token.OperatorType == OperatorType.Coma)//mutiple  variable declaration
                {
                    requirePreviousIsIdentifier();

                    continue;
                }
                else if (token.OperatorType == OperatorType.ExpressionEnd)//declaration ends
                {
                    requirePreviousIsIdentifier();

                    node.DoCheck();

                    expression.Root = node;

                    return expression;
                }
                else if (token.OperatorType == OperatorType.Assignment)// this expression is a  declare-assignment expression
                {

                    requirePreviousIsIdentifier();

                    var assign = new DeclareAsignmentNode(_block);

                    var astNode = new AstNodeBuilder(_reader,_block ,_intercptChars).Build();

                    node.DoCheck();

                    assign.Operands = node.Operands;

                    assign.Operands.Add(astNode);

                    expression.Root = assign;

                    return expression;


                }
                else
                {
                    //invalid token type

                    throwError("");
                }


            }

            // expression incomplete
            throwError("");

            return null;
            
        }
    }
}
