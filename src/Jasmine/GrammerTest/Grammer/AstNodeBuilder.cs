using Jasmine.Spider.Grammer;
using System;
using System.Diagnostics;

namespace GrammerTest.Grammer
{
    public  class AstNodeBuilder:BuilderBase
    {
        public AstNodeBuilder(TokenStreamReader reader,
                              bool hasParent,
                              Func<Token,bool> interceptor) : base(reader)

        {
        }

        /// <summary>
        ///  set for priority
        /// </summary>
        private bool _hasParent;
        /// <summary>
        /// current priority
        /// </summary>
        private int _currentPrority;
        /// <summary>
        /// higherlevel intercepter
        /// </summary>
        public Func<Token, bool> Interceptor { get; }
         

     
    

    
        private OperatorNode finishBuild()
        {
            if (_currentNode != null) //expression empty;
            {
                _currentNode.DoCheck();
            }

            if (_hasParent)
                _reader.Previous();



            return _currentNode;
        }
        

        private void pushNew(NewOperatorNode node)
        {
            Debug.Assert(_currentNode == null);

            _currentNode = node;
        }
       

        private void pushNot(NotOperatorNode node)
        {
            Debug.Assert(_currentNode == null);

            _currentNode = node;
        }


        private void pushOrderedNode(OperatorNode node)
        {
            Debug.Assert(_currentNode != null);

            _currentNode.DoCheck();

            node.Children.Add(_currentNode);

            _currentNode = node;
        }

       

      
        public OperatorNode Build()
        {

            while (_reader.HasNext())
            {

                if (Interceptor(reader.CurrentToken))
                {
                    return finishBuild();
                }

                switch (_reader.Next().TokenType)
                {
                    case TokenType.Keyword:

                        throw new Exception();

                    case TokenType.Operator:


                        /*
                         * check priority
                         *
                         * 1. if current operator is higher than current
                         * start a build,previouce and reset current operator 's right operand
                         * 2. if current operator's priority is lower than current and has parent builder ,previous one token and return 
                         * 3. main work
                         */
                        var priority = _reader.CurrentToken.OperatorType.GetPriority();

                        if(priority>_currentPrority)
                        {
                            _reader.Previous(2);

                            _currentNode.Children.RemoveAt(1);


                            //reset current operand
                        }
                        else if(priority<_currentPrority&&_hasParent)
                        {
                            _reader.Previous();

                            return _currentNode;

                        }
                      
                        switch (_reader.CurrentToken.OperatorType)
                        {
                            //can not post to here  
                            //those operator should be handled or intercepted by higher level builder
                            case OperatorType.Assignment:
                            case OperatorType.AddAsignment:
                            case OperatorType.ReduceAsignment:
                            case OperatorType.MutiplyAsignment:
                            case OperatorType.DevideAsignment:
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

                                pushOrderedNode(OperatorNodeFactory.CreateAnd());

                                break;
                                    
                            case OperatorType.Or:

                                pushOrderedNode(OperatorNodeFactory.CreateOr());

                                break;

                            case OperatorType.Not:

                                pushNot(OperatorNodeFactory.CreateNot());

                                break;

                            case OperatorType.Equel:

                                pushOrderedNode(OperatorNodeFactory.CreateCompareEquel());

                                break;

                            case OperatorType.NotEquel:

                                pushOrderedNode(OperatorNodeFactory.CreateCompareNotEquel());

                                break;

                            case OperatorType.Member:

                                pushOrderedNode(OperatorNodeFactory.CreateMemeber());
                            
                                break;

                            case OperatorType.LeftParenn:

                                /*
                                 *  maybe new heighest priority operator or method call
                                 *  
                                 *  method call intercepted by push identifier
                                 * 
                                 */ 


                                if (_reader.PreviouceToken().TokenType != TokenType.Operator)//"ssss"(5+8)
                                {

                                    ThrowError($" grammer error! ( must after a operator or be first token! ");
                                }
                                else
                                {
                                    //start a  new builder

                                    var node = new ParenthesisBuilder(_reader).Build();

                                }



                                break;

                           
                                /*
                                 *  can just be object literal
                                 */ 

                            case OperatorType.LeftBrace:

                                if(_currentNode!=null)
                                {
                                    ThrowError("{ must be start token of a expression!");
                                }
                                else
                                {
                                    var node = new ObjectLiteralBuilder(_reader).Build();
                                }


                                break;

                            /*
                             * array index call or array literal
                             * 
                             * array index call intercepted by pushidentifier
                             * 
                             */

                            case OperatorType.LeftSquare:


                                if (_currentNode!= null)
                                {
                                    ThrowError("[ must be start token of a expression!");
                                }
                                else
                                {
                                    var node = new ArrayLiteralBuilder(_reader).Build();
                                }
                         
                                break;

                       
                            case OperatorType.Add:

                                pushOrderedNode(OperatorNodeFactory.CreateAdd());

                                break;

                            case OperatorType.Reduce:

                                pushOrderedNode(OperatorNodeFactory.CreateReduce());

                                break;

                            case OperatorType.Mod:

                                pushOrderedNode(OperatorNodeFactory.CreateMod());

                                break;

                            case OperatorType.Mutiply:

                                pushOrderedNode(OperatorNodeFactory.CreateMutiply());

                                break;

                            case OperatorType.Devide:

                                pushOrderedNode(OperatorNodeFactory.CreateDevide());

                                break;

                            case OperatorType.Bigger:

                                pushOrderedNode(OperatorNodeFactory.CreateBigger());

                                break;

                            case OperatorType.BiggerEquel:

                                pushOrderedNode(OperatorNodeFactory.CreateBiggerEquelNode());

                                break;

                            case OperatorType.Less:

                                pushOrderedNode(OperatorNodeFactory.CreateLess());

                                break;


                            case OperatorType.LessEquel:

                                pushOrderedNode(OperatorNodeFactory.CreateLessEquel());

                                break;


                            // just suppoort left increment and decrement 
                            case OperatorType.Increment:

                                pushUnaryNumber(OperatorNodeFactory.CreateIncrement());
                                break;

                            case OperatorType.Decrement:

                                pushUnaryNumber(OperatorNodeFactory.CreateDecremnet());

                                break;


                            case OperatorType.New:

                                pushNew(OperatorNodeFactory.CreateNew());

                                break;
                        }




                        break;

                    case TokenType.Identifier:

                        pushIdentifier(OperatorNodeFactory.CreateOperrand(new JString(_reader.CurrentToken.Value)));

                        break;

                    case TokenType.String:

                        pushValue(OperatorNodeFactory.CreateOperrand(new JString(_reader.CurrentToken.Value)));

                        break;

                    case TokenType.Number:


                        pushValue(OperatorNodeFactory.CreateOperrand(new JNumber(_reader.CurrentToken.Value)));

                        break;

                    case TokenType.Bool:


                         pushValue(OperatorNodeFactory.CreateOperrand(new JBool(_reader.CurrentToken.Value)));

                        break;
                }
            }

            /*
             *this builder should finish by interceptor not by  finsh iteration
             *
             *
             */ 

            throw new Exception();
        }


        private void checkOperator(OperatorType type)
        {
            if(_reader.PreviouceToken().IsOperator())
            {
                if(type==OperatorType.Not)
                {

                }
                else if(type==OperatorType.Increment||type==OperatorType.Decrement)
                {
                    
                }
                else
                {
                    ThrowError("");
                }
              
            }
          
        }

        /// <summary>
        /// tow identifier one by one is a syntax error 
        /// </summary>
        private void checkIdentifer()
        {
            if (!_reader.PreviouceToken().IsOperator())
                ThrowError($" syntax error, two identifer repeat");
        }

      
        

        private void pushUnaryNumber(OperatorNode node)
        {
           if(_currentNode==null)
            {
                _currentNode = node;
            }
           else
            {
                pushOrderedNode(node);
            }

        }



      

        private bool ComparePriority(OperatorType type)
        {
            return true;
        }

      
        private void pushIdentifier(OperandNode node)
        {
            checkIdentifer();

            //query object first identifier that means this is a variable

            if(_currentNode==null)
            {
                _currentNode = node;
            }
            else
            {
                _currentNode.Children.Add(node);
            }
        }


        private void pushValue(OperandNode node)
        {
            checkIdentifer();

            if (_currentNode == null)
            {
                _currentNode = node;
            }
            else
            {
                _currentNode.Children.Add(node);
            }
        }

       

        private void ThrowError(string msg)
        {

        }
    }
}
