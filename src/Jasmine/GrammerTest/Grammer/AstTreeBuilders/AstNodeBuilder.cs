using GrammerTest.Grammer.AstTreeBuilders;
using Jasmine.Spider.Grammer;
using System;
using System.Diagnostics;
using System.Linq;

namespace GrammerTest.Grammer
{
    public  class AstNodeBuilder:BuilderBase
    {
        public AstNodeBuilder(TokenStreamReader reader,
                              string[] intercepChars,
                              bool hasparent=false) : base(reader)

        {
            _interceptChars = intercepChars;
            _hasParent = hasparent;
        }

       



        /// <summary>
        ///  set for priority
        /// </summary>
        private bool _hasParent;
        /// <summary>
        /// current priority
        /// </summary>
        private int _currentPrority=-2;
        /// <summary>
        /// higherlevel intercepter
        /// </summary>
        public string[] _interceptChars { get; }
         

     
    

    
        private OperatorNode finishBuild()
        {
            if (_currentNode != null) //is expression empty;
            {
                _currentNode.DoCheck();//check current operator's operands is ok
            }

            if (_hasParent)
                _reader.Previous();

            return _currentNode;
        }
        

        private void pushNew(NewOperatorNode node)
        {
            /*
             * must be first operator node
             */ 

            Debug.Assert(_currentNode == null);

            _currentNode = node;
        }
       

        private void pushNot(NotOperatorNode node)
        {
            /*
            * must be first operator node
            * cause its priority is higher than && || ,it's always a new stack 
            */
            Debug.Assert(_currentNode == null);

            _currentNode = node;
        }


        private void pushOrderedNode(OperatorNode node)
        {
           /*
            * must be first operator node
            */
            Debug.Assert(_currentNode != null);

            _currentNode.DoCheck();

            node.Operands.Add(_currentNode);

            _currentNode = node;
        }

       

      
        public OperatorNode Build()
        {

            while (_reader.HasNext())
            {
                _reader.Next();

                if (_interceptChars.Contains(_reader.CurrentToken.Value))
                {
                    return finishBuild();
                }

                switch (_reader.CurrentToken.TokenType)
                {

                    /*
                     * in this builder keyword is not allowed 
                     */
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

                        if (_currentPrority == -2)
                        {
                            _currentPrority = priority;
                        }
                        else
                        {

                            if (priority > _currentPrority)
                            {
                                _reader.Previous(2);

                                _currentNode.Operands.RemoveAt(1);

                                var node = new AstNodeBuilder(_reader, _interceptChars, true).Build();

                                _currentNode.Operands.Add(node);

                                //reset current operand
                            }
                            else if (priority < _currentPrority && _hasParent)
                            {
                                return finishBuild();

                            }
                        }


                        switch (_reader.CurrentToken.OperatorType)
                        {
                          
                            case OperatorType.Assignment:
                            case OperatorType.AddAsignment:
                            case OperatorType.ReduceAsignment:
                            case OperatorType.MutiplyAsignment:
                            case OperatorType.DevideAsignment:
                            case OperatorType.Semicolon:
                            case OperatorType.Question:

                            //can not post to here  
                            //those operator should be handled or intercepted by higher level builder
                            case OperatorType.Coma:
                            case OperatorType.Var:
                            case OperatorType.ExpressionEnd:
                            case OperatorType.Break:
                            case OperatorType.Continue:
                            case OperatorType.RightParenthesis:
                            case OperatorType.RightBrace:
                            case OperatorType.RightSquare:

                                ThrowError($" there's a bug ,this token should be intercepted by higher layer,should not be post here ");

                             break;

                            case OperatorType.Minus:
                                break;

                            
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

                            case OperatorType.LeftParenthesis:

                                /*
                                 *  maybe new heighest priority operator or method call
                                 *  
                                 *  method call intercepted by push identifier
                                 * 
                                 */
                                if (_currentNode == null)
                                {
                                    _currentNode = new ParenthesisBuilder(_reader).Build();

                                }
                                else
                                {

                                    if (_reader.PreviouceToken().TokenType == TokenType.Identifier)
                                    {
                                        _reader.Previous(2);

                                        var node = new CallBuilder(_reader).Build();

                                        node.Operands.Insert(0,_currentNode);
                                        _currentNode = node;

                                    }
                                    else if (_reader.PreviouceToken().TokenType != TokenType.Operator)//"ssss"(5+8)
                                    {

                                        ThrowError($" grammer error! ( must after a operator or be first token! ");
                                    }
                                    else
                                    {
                                        //start a  new builder

                                        var node = new ParenthesisBuilder(_reader).Build();

                                        _currentNode.Operands.Add(node);

                                    }
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
                                    _currentNode = new OperandNode( new ObjectLiteralBuilder(_reader).Build());
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
                                    if(_reader.PreviouceToken().TokenType!=TokenType.Identifier)
                                    {
                                        ThrowError("");
                                    }
                                    else
                                    {
                                        var node = new ArrayIndexBuilder(_reader).Build();

                                        node.Operands.Add(_currentNode);

                                        _currentNode = node;
                                    }
                                }
                                else
                                {
                                    _currentNode = new OperandNode( new ArrayLiteralBuilder(_reader).Build());
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

            ThrowError("");

           return null;
        }


        

        /// <summary>
        /// tow identifier one by one is a syntax error 
        /// </summary>
        private void checkIdentifer()
        {
            if (_reader.HasPrevious()&&!_reader.PreviouceToken().IsOperator())
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
                _currentNode = new QueryScopeOperatorNode(null);
            }
            else
            {
                _currentNode.Operands.Add(node);
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
                _currentNode.Operands.Add(node);
            }
        }

       

        private void ThrowError(string msg)
        {

        }
    }
}
