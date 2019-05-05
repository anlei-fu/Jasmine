using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class TopBlockBuilder : BuilderBase
    {
        public OrderdedBlock _scope=new OrderdedBlock();

        public TopBlockBuilder(ISequenceReader<Token> reader) : base(reader)
        {
        }

        public override string Name => "TopBuilder";

        public void Build()
        {
            while (_reader.HasNext())
            {
                _reader.Next();
                var token = _reader.Current();

                switch (token.TokenType)
                {
                    case TokenType.Keyword:

                        if (_reader.Current().Value==Keywords.FOR)
                        {
                            var block = new ForBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.IF)
                        {
                            var block = new IfBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.FOREACH)
                        {
                            var block = new ForeachBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.DO)
                        {
                            var block = new DoWhileScopeBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.WHILE)
                        {
                            var block = new WhileBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.TRY)
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

                        switch (_reader.Current().OperatorType)
                        {
                          
                            /*
                             *  variable declare 
                             */
                             
                            case OperatorType.Declare:

                                var decExpression = new DeclareExpressionBuilder(_reader).Build();

                                _scope.Children.Add(decExpression);

                                break;

                              /*
                               * Function define expression is all right in this scope
                               * 
                               */ 

                            case OperatorType.Function:

                                var function = new FunctionBuilder(_reader).Build();

                                var funcDefineExp = new FunctionDefineExpression();

                                funcDefineExp.Root = new FunctionDefineNode();

                                funcDefineExp.Root.Operands.Add(new OperandNode(function));

                                _scope.Children.Add(funcDefineExp);

                                break;

                            /*
                             *  New expression
                             */ 
                            case OperatorType.NewInstance:
                            /*
                             * Increament  expression
                             *
                             */ 
                            case OperatorType.Increment:
                            case OperatorType.Decrement:

                                _reader.Back();

                                var crementExpression = new ExpressionBuilder(_reader,false).Build();

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

                        _reader.Back();

                        var expression = new ExpressionBuilder(_reader,true).Build();

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
