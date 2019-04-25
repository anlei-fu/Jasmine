using Jasmine.Spider.Grammer;
using System;

namespace GrammerTest.Grammer
{
    public  class AstNodeBuilder:BuilderBase
    {

        private Func<Token, bool> _interceptor;

        public AstNodeBuilder(TokenStreamReader reader) : base(reader)
        {
        }

        /// <summary>
        /// is the current token canbe first token
        /// ++,
        /// --,
        /// !,
        /// </summary>
        private void checkStartWithOperator()
        {
            switch (_reader.CurrentToken.OperatorType)
            {
               
                case OperatorType.Not:
                case OperatorType.Increment:
                case OperatorType.Decrement:
                case OperatorType.Break:
                case OperatorType.Continue:
                    break;
                default:

                    break;
            }

        }

        private void checkOutputType( OutputType type)
        {

        }

        private OperatorNode buildArrayIndex()
        {
            return null;
        }
        private OperatorNode buildFunctionCall()
        {
            return null;
        }
        private void checkPreviceIsOperator()
        {

        }

    
        private OperatorNode finishBuild()
        {
            return null;
        }
        
        private void pushOperator(OperatorType op)
        {

        }

        public OperatorNode Build(TokenStreamReader reader,Scope scope)
        {

          

            while (_reader.HasNext())
            {

                if (_interceptor(reader.CurrentToken))
                {

                    return finishBuild();
                }

                switch (_reader.Next().TokenType)
                {
                    case TokenType.Keyword:
                        throw new Exception();

                    case TokenType.Operator:

                        checkOperator(_reader.CurrentToken.OperatorType);

                        switch (_reader.CurrentToken.OperatorType)
                        {
                            //can not post to here  
                            //those operator should be handled or intercepted by higher level builder
                            case OperatorType.Assignment:




                            case OperatorType.Var:
                            case OperatorType.Semicolon:
                            case OperatorType.Question:
                            case OperatorType.Coma:
                            case OperatorType.ExpressionEnd:
                            case OperatorType.Break:
                            case OperatorType.Continue:
                            case OperatorType.RightParenn:
                            case OperatorType.RightBrace:
                            case OperatorType.RightSquare:

                                ThrowError($" there's a bug ,this token should be intercepted by higher layer,should not be post here ");

                             break;


                            //logic check
                            case OperatorType.And:
                                pushLogicOperator(OperatorNodeFactory.CreateAnd());
                                break;
                                    
                            case OperatorType.Or:
                                pushLogicOperator(OperatorNodeFactory.CreateOr());
                                break;

                            case OperatorType.Not:
                                checkOutputType( OutputType.Bool);


                                break;

                            case OperatorType.Equel:

                            case OperatorType.Member:
                            case OperatorType.NotEquel:
                                break;

                            case OperatorType.LeftParenn:

                                if (_reader.PreviouceToken().TokenType != TokenType.Operator)
                                {
                                    ThrowError($" grammer error! ( must after a operator or be first token! ");
                                }
                                else
                                {
                                    var node = new ParenthesisBuilder(_reader).Build();
                                }



                                break;

                            //object literal
                            case OperatorType.LeftBrace:

                                if(_firstOperand!=null)
                                {
                                    ThrowError("{ must be start token of a expression!");
                                }
                                else
                                {
                                    var node = new ObjectLiteralBuilder(_reader).Build();
                                }


                                break;

                            //array index call or array literal
                            case OperatorType.LeftSquare:


                                if (_firstOperand != null)
                                {
                                    ThrowError("[ must be start token of a expression!");
                                }
                                else
                                {
                                    var node = new ArrayLiteralBuilder(_reader).Build();
                                }
                         
                                break;

                            //number check
                            case OperatorType.Add:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateAdd());

                                break;

                            case OperatorType.Reduce:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateReduce());

                                break;

                            case OperatorType.Mod:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateMod());

                                break;

                            case OperatorType.Mutiply:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateMutiply());

                                break;

                            case OperatorType.Devide:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateDevide());

                                break;

                            case OperatorType.Bigger:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateBigger());

                                break;

                            case OperatorType.BiggerEquel:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateBiggerEquelNode());

                                break;

                            case OperatorType.Less:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateLess());

                                break;


                            case OperatorType.LessEquel:

                                pushAlgrothmOperator(OperatorNodeFactory.CreateLessEquel());

                                break;


                            case OperatorType.Increment:
                            case OperatorType.Decrement:
                                if (_firstOperand != null && _reader.PreviouceToken().TokenType != TokenType.Operator)
                                    throw new Exception();

                                break;

                            case OperatorType.New:
                            
                                break;
                        }




                        break;

                    case TokenType.Identifier:

                        checkIdentifer();

                        pushIdentifier(OperatorNodeFactory.CreateOperrand(new JString(_reader.CurrentToken.Value)));

                        break;
                    case TokenType.String:

                        checkIdentifer();

                        pushString(OperatorNodeFactory.CreateOperrand(new JString(_reader.CurrentToken.Value)));

                        break;

                    case TokenType.Number:

                        checkIdentifer();

                        pushNumber(OperatorNodeFactory.CreateOperrand(new JNumber(_reader.CurrentToken.Value)));

                        break;

                    case TokenType.Bool:

                        checkIdentifer();

                        pushBool(OperatorNodeFactory.CreateOperrand(new JBool(_reader.CurrentToken.Value)));

                        break;
                }
            }



            throw new Exception();
        }


        private void checkOperator(OperatorType type)
        {
            if(_reader.PreviouceToken().IsOperator())
            {
                if (!(_reader.CurrentToken.OperatorType == OperatorType.Increment ||
                    _reader.CurrentToken.OperatorType == OperatorType.Decrement ||
                    _reader.CurrentToken.OperatorType == OperatorType.Not)) 
                {
                    ThrowError("two operator repeat ");
                }
              
            }
            else
            {

            }
        }

        private void checkIdentifer()
        {
            if (!_reader.PreviouceToken().IsOperator())
                ThrowError($" syntax error, two identifer repeat");
        }

        private void pushCompare()
        {

        }
        

        private void pushUnaryNumber(OperatorNode node)
        {
           if(_currentNode==null)
            {
                _currentNode = node;
            }
           else
            {
              
            }

        }



        private void pushAlgrothmOperator(OperatorNode node)
        {
            checkOutputType(OutputType.Number);

            if (_currentNode.IsOperator)
            {
              
            }
            else
            {

            }
        }

        private bool ComparePriority(OperatorType type)
        {
            return true;
        }

        private void pushLogicOperator(LogicOperatorNode node)
        {
            checkOutputType(OutputType.Bool);

            if(_currentNode.IsOperator)
            {
                if(ComparePriority(node.OperatorType))
                {

                }
                else
                {

                }

            }
            else
            {

            }

        }
        private void pushIdentifier(OperandNode node)
        {

        }

        private void pushNumber(OperandNode node)
        {

        }
        private void pushString(OperandNode node)
        {

        }

        private void pushBool(OperandNode node)
        {

        }

        private void ThrowError(string msg)
        {

        }
    }
}
