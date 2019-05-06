using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class BlockBuilder : BuilderBase
    {
        public BlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public OrderdedBlock Build()
        {
           OrderdedBlock orderingBlock = new OrderdedBlock();

            throwErrorIfHasNoNextOrNext();

            /*
             * to check ,the block is single expression or mutiple expression 
             * 
             */
             
            if(_reader.CurrentToken.OperatorType==OperatorType.LeftBrace)
            {
                while (_reader.HasNext())
                {
                    switch (_reader.Next().TokenType)
                    {
                        case TokenType.Keyword:

                            if (_reader.CurrentToken.Value == Keywords.FOR)
                            {
                                var block = new ForBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.CurrentToken.Value == Keywords.IF)
                            {
                                var block = new IfBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.CurrentToken.Value == Keywords.FOREACH)
                            {
                                var block = new ForeachBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.CurrentToken.Value == Keywords.DO)
                            {
                                var block = new DoWhileScopeBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.CurrentToken.Value == Keywords.WHILE)
                            {
                                var block = new WhileBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.CurrentToken.Value == Keywords.TRY)
                            {
                                var block = new TryCatchFinallyBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else
                            {
                                throwError("");
                            }

                            break;

                            /*
                             *  the operators that  a expression  can start with
                             *  new;
                             *  break;
                             *  continue;
                             *  throw;
                             *  ++;
                             *  --;
                             *  new;
                             *  var;
                             *  identifier;
                             *  
                             *  function operator is not allowed ,it's just allowed in topiest block
                             */ 

                        case TokenType.Operator:

                            switch (_reader.CurrentToken.OperatorType)
                            {

                                case OperatorType.Var:

                                    var decExpression = new DeclareExpressionBuilder(_reader).Build();

                                    orderingBlock.Children.Add(decExpression);

                                    break;
                                   
                                    //block finishd
                                case OperatorType.RightBrace:

                                    return orderingBlock;

                                case OperatorType.Break:

                                    break;

                                case OperatorType.Continue:

                                    break;

                                case OperatorType.Increment:
                                case OperatorType.Decrement:

                                    _reader.Previous();

                                    var expression0 = new ExpressionBuilder(_reader).Build();
                                    orderingBlock.Children.Add(expression0);

                                    break;

                                default:

                                    throwError("");

                                    break;
                            }

                            break;


                        case TokenType.Identifier:

                            _reader.Previous();

                            var expression = new ExpressionBuilder(_reader).Build();

                            orderingBlock.Children.Add(expression);

                            break;

                        case TokenType.String:
                        case TokenType.Number:
                        case TokenType.Bool:
                        case TokenType.Null:
                        default:

                            throwError("");

                            break;
                    }
                }
            }
            else
            {
                _reader.Previous();
            }


          


            throwError("blcok unfinishedn exception");

            return orderingBlock;
        }
    }
}
