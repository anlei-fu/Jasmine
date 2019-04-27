using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class TopScopeBuilder : BuilderBase
    {
        public TopScope _scope;
        public TopScopeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public void Build()
        {
            while (_reader.HasNext())
            {
                switch (_reader.Next().TokenType)
                {
                    case TokenType.Keyword:

                        if (_reader.CurrentToken.Value==Keywords.FOR)
                        {
                            var block = new ForBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.CurrentToken.Value==Keywords.IF)
                        {
                            var block = new IfBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.CurrentToken.Value==Keywords.FOREACH)
                        {
                            var block = new ForeachBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.CurrentToken.Value==Keywords.DO)
                        {
                            var block = new DoWhileScopeBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.CurrentToken.Value==Keywords.WHILE)
                        {
                            var block = new WhileBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.CurrentToken.Value==Keywords.TRY)
                        {
                            var block = new TryCatchFinallyBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else
                        {
                            throwError("");
                        }

                        break;

                    case TokenType.Operator:

                        switch (_reader.CurrentToken.OperatorType)
                        {
                          
                            /*
                             *  variable declare 
                             */
                             
                            case OperatorType.Var:

                                var decExpression = new DeclareExpressionBuilder(_reader).Build();

                                _scope.Children.Add(decExpression);

                                break;

                              /*
                               * Function define expression is all right in this scope
                               * 
                               */ 

                            case OperatorType.Function:

                                var function = new FunctionScopeBuilder(_reader).Build();

                                var funcDefineExp = new FunctionDefineExpression();

                                funcDefineExp.Root = new FunctionDefineNode();

                                funcDefineExp.Root.Operands.Add(new OperandNode(function));

                                _scope.Children.Add(funcDefineExp);

                                break;

                            /*
                             *  New expression
                             */ 
                            case OperatorType.New:
                            /*
                             * Increament  expression
                             *
                             */ 
                            case OperatorType.Increment:
                            case OperatorType.Decrement:

                                _reader.Previous();

                                var crementExpression = new ExpressionBuilder(_reader).Build();

                                _scope.Children.Add(crementExpression);

                                break;

                          
                            /*
                             * Expression can not start with operator ,excepts  increment(++) ,decrement(--) ,var ,new ,function
                             * 
                             */ 

                            default:

                                throwError("");

                                break;
                        }

                        break;
                    /*
                     * Back one token ,to build excuting expression 
                     *
                     */ 
                      
                    case TokenType.Identifier:

                        _reader.Previous();

                        var expression = new ExpressionBuilder(_reader).Build();

                        _scope.Children.Add(expression);

                        break;

                    /*
                     * Expression can  not start with those kinds of token
                     * number,null,bool,string
                     * 
                     */

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


    }
}
