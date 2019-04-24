using Jasmine.Spider.Grammer;
using System;

namespace GrammerTest.Grammer
{
    public  class AstNodeBuilder
    {
        private TokenStreamReader _reader;

        private OperatorNode _currentNode;
        private Scope _scope;
        private Operand _currentOperand;

        /// <summary>
        /// is the current token canbe first token
        /// ++,
        /// --,
        /// !,
        /// </summary>
        private void checkStartWithOperator()
        {

        }

        private void checkOutputType(OperatorNode node, OutputType type)
        {

        }

        private OperatorNode buildArrayIndex()
        {

        }
        private OperatorNode buildFunctionCall()
        {
            return null;
        }
        private void checkPreviceIsOperator()
        {

        }

        public OperatorNode Build(TokenStreamReader reader,Scope scope)
        {

            while (_reader.HasNext())
            {
                switch (_reader.Next().TokenType)
                {
                    case TokenType.Keyword:
                        throw new Exception();

                    case TokenType.Operator:

                        if (_currentOperand == null)
                            checkStartWithOperator();


                        switch (_reader.CurrentToken.OperatorType)
                        {
                            case OperatorType.Assignment:

                                checkOutputType(_currentNode, OutputType.Variable);

                                break;

                            case OperatorType.And:
                            case OperatorType.Or:
                            case OperatorType.Not:

                                checkOutputType(_currentNode, OutputType.Bool);


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

                                if(_reader.PreviouceToken().IsIdentifier())
                                {
                                    buildFunctionCall();
                                }
                                else
                                {

                                }


                                break;

                            case OperatorType.RightParenn:
                            case OperatorType.LeftBrace:
                            case OperatorType.RightBrace:
                            case OperatorType.RightSquare:
                                throw new Exception();

                            case OperatorType.LeftSquare:

                                if(_reader.PreviouceToken().IsIdentifier())
                                {
                                    buildArrayIndex();
                                }
                                else
                                {

                                }
                         
                                break;

                            case OperatorType.Add:
                            case OperatorType.Reduce:
                            case OperatorType.Mod:
                            case OperatorType.Mutiply:
                            case OperatorType.Devide:
                                checkOutputType(_currentNode, OutputType.Number);

                                break;
                            case OperatorType.LeftIncrement:
                                break;
                            case OperatorType.RightIncrement:
                                break;
                            case OperatorType.LeftDecrement:
                                break;
                            case OperatorType.RightDecrement:
                                break;
                            case OperatorType.Semicolon:
                                break;
                            case OperatorType.Coma:
                                break;
                            case OperatorType.ExpressionEnd:
                                break;

                            case OperatorType.Bigger:
                            case OperatorType.BiggerEquel:
                            case OperatorType.Less:
                            case OperatorType.LessEquel:
                                checkOutputType(_currentNode, OutputType.Number);
                                break;
                            case OperatorType.QueryObJect:
                                break;
                            case OperatorType.New:
                                break;
                            case OperatorType.Var:
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
                    case TokenType.String:
                    case TokenType.Number:
                    case TokenType.Bool:

                        checkPreviceIsOperator();

                        break;
                    default:
                        break;
                }
            }



            return null;
        }
    }
}
