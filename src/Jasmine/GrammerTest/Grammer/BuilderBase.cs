using Jasmine.Spider.Grammer;
using System;

namespace GrammerTest.Grammer
{
    public class BuilderBase
    {
        protected TokenStreamReader _reader;
        protected OperatorNode _currentOperatorNode;
        protected Operand _firstOperand;
        protected Operand _currentOperand;
        protected Func<Token, bool> _interceptor;

        public OperatorNode Build()
        {
            while (_reader.HasNext())
            {

                if(_interceptor(_reader.Next()))
                {

                }

                switch (_reader.CurrentToken.TokenType)
                {
                    case TokenType.Keyword:
                        ThrowGrammerException();
                        break;
                    case TokenType.Operator:
                        switch (_reader.CurrentToken.OperatorType)
                        {
                            case OperatorType.Assignment:
                                break;

                            case OperatorType.And:
                            case OperatorType.Or:
                                checkRequiredOutput(OutputType.Bool);
                                break;

                            case OperatorType.Not:
                                break;
                            case OperatorType.Equel:
                                break;
                            case OperatorType.Member:
                                break;
                            case OperatorType.NotEquel:
                                break;
                            case OperatorType.Call:
                                break;
                            case OperatorType.LeftParenn:

                                if(_reader.Previous().TokenType==TokenType.Identifier)
                                {

                                }
                                else
                                {

                                }

                                break;
                            
                            case OperatorType.LeftBrace:
                                if(GetHashCode()>0)
                                {

                                }
                                else
                                {

                                }

                                break;
                           
                            case OperatorType.LeftSquare:



                                break;
                           



                            case OperatorType.Bigger:
                            case OperatorType.BiggerEquel:
                            case OperatorType.Less:
                            case OperatorType.LessEquel:
                            case OperatorType.Add:
                            case OperatorType.Reduce:
                            case OperatorType.Mod:
                            case OperatorType.Mutiply:
                            case OperatorType.Devide:
                                checkRequiredOutput(OutputType.Number);
                                break;



                            case OperatorType.Increment:
                            case OperatorType.Decrement:
                                break;
                            case OperatorType.Semicolon:
                                break;
                            case OperatorType.RightBrace:
                            case OperatorType.RightSquare:
                            case OperatorType.RightParenn:
                            case OperatorType.Var:
                            case OperatorType.Coma:
                            case OperatorType.ExpressionEnd:
                                break;
                          
                            case OperatorType.QueryObJect:
                                break;
                            case OperatorType.New:
                                break;
                         
                            case OperatorType.Break:
                                break;
                            case OperatorType.Continue:
                                break;
                            default:
                                break;
                        }

                        break;
                    case TokenType.Identifier:
                        break;
                    case TokenType.String:
                        break;
                    case TokenType.Number:
                        break;
                    case TokenType.Bool:
                        break;
                    default:
                        break;
                }

            }

            return null;
        }

        private void checkRequiredOutput(OutputType type)
        {

        }
        protected void ThrowGrammerException()
        {

        }
    }
}
