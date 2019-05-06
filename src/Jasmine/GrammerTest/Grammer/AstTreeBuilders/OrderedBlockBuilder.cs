using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using System.Runtime.CompilerServices;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class OrderedBlockBuilder : BuilderBase
    {
        private static readonly string[] _interceptChars = new string[]
        {
            ";"
        };

        private string _parentName;
        public OrderedBlockBuilder(ISequenceReader<Token> reader, string parentName) : base(reader)
        {
            _parentName = parentName;
        }

        public override string Name => "OderingBlockBuilder";



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Expression buildContinueOrBreakExpression(bool isContinue)
        {
            throwErrorIfHasNoNextAndNext("incompleted expression;");

            throwErrorIfOperatorTypeNotMatch(OperatorType.ExpressionEnd);

            var exp = new Expression();

            exp.Root = isContinue ? OperatorNodeFactory.CreateContinue()
                                    : OperatorNodeFactory.CreateBreak();

            return exp;
        }

        private Expression buildReturnExpression()
        {
            var expNode = new AstNodeBuilder(_reader, _interceptChars).Build();

            var retNode = OperatorNodeFactory.CreateReturn();

            retNode.Operands.Add(expNode);

            var exp = new Expression();

            exp.Root = retNode;

            return exp;
        }

        private void throwIfBlockNotFinish()
        {
            if (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current().OperatorType != OperatorType.RightBrace)
                    throwError("block should be finished but it still has token;");
            }
            else
            {
                throwError("");
            }

        }

        public OrderdedBlock Build()
        {
            OrderdedBlock orderingBlock = new OrderdedBlock();

            throwErrorIfHasNoNextAndNext($"incompleted {_parentName} block;");

            /*
             * to check ,the block is single expression or mutiple expression 
             * 
             */

            if (_reader.Current().OperatorType == OperatorType.LeftBrace)
            {
                while (_reader.HasNext())
                {
                    _reader.Next();

                    var token = _reader.Current();

                    switch (token.TokenType)
                    {
                        case TokenType.Keyword:

                            if (_reader.Current().Value == Keywords.FOR)
                            {
                                var block = new ForBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.Current().Value == Keywords.IF)
                            {
                                var block = new IfBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.Current().Value == Keywords.FOREACH)
                            {
                                var block = new ForeachBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.Current().Value == Keywords.DO)
                            {
                                var block = new DoWhileScopeBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.Current().Value == Keywords.WHILE)
                            {
                                var block = new WhileBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else if (_reader.Current().Value == Keywords.TRY)
                            {
                                var block = new TryCatchFinallyBlockBuilder(_reader).Build();

                                orderingBlock.Children.Add(block);
                            }
                            else
                            {
                                throwUnexceptedError();
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

                            switch (_reader.Current().OperatorType)
                            {

                                case OperatorType.Break:

                                    orderingBlock.Children.Add(buildContinueOrBreakExpression(false));

                                    throwIfBlockNotFinish();

                                    return orderingBlock;

                                case OperatorType.Continue:

                                    orderingBlock.Children.Add(buildContinueOrBreakExpression(true));

                                    throwIfBlockNotFinish();

                                    return orderingBlock;

                                case OperatorType.Declare:

                                    var decExpression = new DeclareExpressionBuilder(_reader).Build();

                                    orderingBlock.Children.Add(decExpression);

                                    break;

                                case OperatorType.Return:

                                    orderingBlock.Children.Add(buildReturnExpression());

                                    throwIfBlockNotFinish();

                                    return orderingBlock;

                                //block finishd
                                case OperatorType.RightBrace:

                                    return orderingBlock;

                                case OperatorType.Increment:
                                case OperatorType.Decrement:

                                    _reader.Back();

                                    var expression0 = new ExpressionBuilder(_reader, true).Build();
                                    orderingBlock.Children.Add(expression0);

                                    break;

                                default:

                                    throwUnexceptedError();

                                    break;
                            }

                            break;

                        case TokenType.Identifier:

                            _reader.Back();

                            var expression = new ExpressionBuilder(_reader, true).Build();

                            orderingBlock.Children.Add(expression);

                            break;

                        case TokenType.String:
                        case TokenType.Number:
                        case TokenType.Bool:
                        case TokenType.Null:
                        default:

                            throwUnexceptedError();

                            break;
                    }
                }
            }
            else
            {
                if (_reader.Current().OperatorType == OperatorType.Continue)
                {
                    orderingBlock.Children.Add(buildContinueOrBreakExpression(true));

                    return orderingBlock;

                }
                else if (_reader.Current().OperatorType == OperatorType.Break)
                {
                    orderingBlock.Children.Add(buildContinueOrBreakExpression(false));

                    return orderingBlock;
                }
                else if (_reader.Current().OperatorType == OperatorType.Return)
                {
                    orderingBlock.Children.Add(buildReturnExpression());

                    return orderingBlock;
                }
                else
                {
                    _reader.Back();

                    var expression = new ExpressionBuilder(_reader, true).Build();

                    orderingBlock.Children.Add(expression);

                    return orderingBlock;
                }

            }

            throwError($"{_parentName} block is incompleted;");

            return orderingBlock;
        }
    }
}
