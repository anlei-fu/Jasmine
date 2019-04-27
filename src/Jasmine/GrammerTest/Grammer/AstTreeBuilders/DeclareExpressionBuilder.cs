using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class DeclareExpressionBuilder : BuilderBase
    {
        private static readonly string[] _intercptChars = new string[]
        {
            ";"
        };
        public DeclareExpressionBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public DeclareExpression Build()
        {
            var expression = new DeclareExpression();

            var node = new DeclareOperator();

            while (_reader.HasNext())
            {
                _reader.Next();

                var token = _reader.CurrentToken;

                if (token.TokenType == TokenType.Identifier)
                {
                    node.Operands.Add(new OperandNode(new JString(token.Value)));
                }

               else if (token.OperatorType == OperatorType.Coma)
                    continue;
               else if(token.OperatorType==OperatorType.ExpressionEnd)
                {
                    if (_reader.PreviouceToken().OperatorType == OperatorType.Coma)
                        throwError("");

                    node.DoCheck();

                    expression.Root = node;

                    return expression;
                }
                else if(token.OperatorType==OperatorType.Assignment)
                {

                    if (_reader.PreviouceToken().OperatorType == OperatorType.Coma)
                        throwError("");


                    var assign = new DeclareAsignmentNode();

                    var astNode = new AstNodeBuilder(_reader, _intercptChars).Build();

                    node.DoCheck();

                    assign.Operands = node.Operands;

                    assign.Operands.Add(astNode);

                    expression.Root = assign;

                    return expression;


                }
                else
                {
                    throwError("");
                }


            }


            throwError("");

            return null;
            
        }
    }
}
