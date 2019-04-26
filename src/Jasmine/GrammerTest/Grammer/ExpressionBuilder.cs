using Jasmine.Spider.Grammer;

namespace GrammerTest.Grammer
{
    public class ExpressionBuilder:BuilderBase
    {
        public ExpressionBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        public  Scope Build()
        {
            var expression = new Expression()
            {

            };

            while (_reader.HasNext())
            {
                switch (_reader.Next().TokenType)
                {
                    case TokenType.Keyword:

                        if(_currentNode!=null)
                        {
                            throwError("");
                        }
                        else if(_reader.CurrentToken.Value=="for")
                        {
                            return new ForScopeBuilder(_reader).Build();
                        }
                        else if(_reader.CurrentToken.Value=="foreach")
                        {
                            return new ForeachBuilder(_reader).Build();
                        }
                        else if(_reader.CurrentToken.Value=="do")
                        {
                            return new DoWhileScopeBuilder(_reader).Build();
                        }
                        else if(_reader.CurrentToken.Value=="while")
                        {
                            return new WhileScopeBuilder(_reader).Build();
                        }
                        else if(_reader.CurrentToken.Value=="if")
                        {
                            return new IfScopeBuilder(_reader).Build();
                        }
                        else
                        {
                            throwError("");
                        }

                        break;

                    case TokenType.Operator:

                        switch (_reader.CurrentToken.OperatorType)
                        {
                            case OperatorType.Var:
                                break;
                            case OperatorType.Function:
                                break;
                            case OperatorType.Break:
                                break;
                            case OperatorType.Continue:
                                break;
                            
                            default:
                                throwError("");
                                break;
                        }

                        break;

                    case TokenType.Identifier:

                        break;

                    case TokenType.String:
                    case TokenType.Number:
                    case TokenType.Bool:
                    case TokenType.Null:

                        throwError("");

                        break;

                    default:
                        break;
                }

            }



            return expression;

        }

      


    }
}
