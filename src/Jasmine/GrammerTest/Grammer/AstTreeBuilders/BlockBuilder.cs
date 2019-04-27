using GrammerTest.Grammer.Scopes;
using Jasmine.Spider.Grammer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrammerTest.Grammer.AstTreeBuilders
{
    public class BlockBuilder : BuilderBase
    {
        public BlockBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public Block Build()
        {
            Scope _scope = null;

            while (_reader.HasNext())
            {
                switch (_reader.Next().TokenType)
                {
                    case Jasmine.Spider.Grammer.TokenType.Keyword:

                        if (_reader.CurrentToken.Value == Keywords.FOR)
                        {
                            var block = new ForBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if (_reader.CurrentToken.Value == Keywords.IF)
                        {
                            var block = new IfBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if (_reader.CurrentToken.Value == Keywords.FOREACH)
                        {
                            var block = new ForeachBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if (_reader.CurrentToken.Value == Keywords.DO)
                        {
                            var block = new DoWhileScopeBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if (_reader.CurrentToken.Value == Keywords.WHILE)
                        {
                            var block = new WhileBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else if (_reader.CurrentToken.Value == Keywords.TRY)
                        {
                            var block = new TryCatchFinallyBlockBuilder(_reader).Build();

                            _scope.Children.Add(block);
                        }
                        else
                        {
                            throwError("");
                        }

                        break;

                    case Jasmine.Spider.Grammer.TokenType.Operator:

                        switch (_reader.CurrentToken.OperatorType)
                        {

                            case Jasmine.Spider.Grammer.OperatorType.Var:

                                var decExpression = new DeclareExpressionBuilder(_reader).Build();

                                _scope.Children.Add(decExpression);

                                break;

                            case OperatorType.RightBrace:
                                return _scope;

                            case OperatorType.Break:
                                break;

                            case OperatorType.Continue:
                                break;

                            case Jasmine.Spider.Grammer.OperatorType.Increment:
                            case Jasmine.Spider.Grammer.OperatorType.Decrement:
                                _reader.Previous();

                                break;

                            default:

                                throwError("");

                                break;
                        }

                        break;


                    case Jasmine.Spider.Grammer.TokenType.Identifier:

                        _reader.Previous();

                        var expression = new ExpressionBuilder(_reader).Build();

                        _scope.Children.Add(expression);

                        break;

                    case Jasmine.Spider.Grammer.TokenType.String:
                    case Jasmine.Spider.Grammer.TokenType.Number:
                    case Jasmine.Spider.Grammer.TokenType.Bool:
                    case Jasmine.Spider.Grammer.TokenType.Null:
                    default:

                        throwError("");

                        break;
                }
            }


            throwError("blcok unfinishedn exception");

            return _scope;
        }
    }
}
