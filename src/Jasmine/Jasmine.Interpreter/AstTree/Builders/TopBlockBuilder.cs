using Jasmine.Interpreter.Scopes;
using Jasmine.Interpreter.Tokenizers;

namespace Jasmine.Interpreter.AstTree.Builders
{
    public class TopBlockBuilder : BuilderBase
    {
        public OrderdedBlock Block=new OrderdedBlock(null);
        public TopBlockBuilder(ISequenceReader<Token> reader, BreakableBlock block) : base(reader,block)
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
                            var block = new ForBlockBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.IF)
                        {
                            var block = new IfBlockBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.FOREACH)
                        {
                            var block = new ForeachBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.DO)
                        {
                            var block = new DoWhileScopeBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.WHILE)
                        {
                            var block = new WhileBlockBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
                        }
                        else if(_reader.Current().Value==Keywords.TRY)
                        {
                            var block = new TryCatchFinallyBlockBuilder(_reader,_block).Build();

                            Block.Children.Add(block);
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

                                var decExpression = new DeclareExpressionBuilder(_reader,_block).Build();

                                Block.Children.Add(decExpression);

                                break;
                              /*
                               * Function define expression is all right in this scope
                               * 
                               */ 

                            case OperatorType.Function:

                                var function = new FunctionBuilder(_reader,_block).Build();

                                var funcDefineExp = new FunctionDefineExpression(_block);

                                funcDefineExp.Root = new FunctionDefineNode(_block);

                                funcDefineExp.Root.Operands.Add(new OperandNode(function));

                                Block.Children.Add(funcDefineExp);

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

                                var crementExpression = new ExpressionBuilder(_reader,false,_block).Build();

                                Block.Children.Add(crementExpression);

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

                        var expression = new ExpressionBuilder(_reader,true,_block).Build();

                        Block.Children.Add(expression);

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
