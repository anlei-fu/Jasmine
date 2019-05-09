using GrammerTest.Grammer.AstTreeBuilders;
using GrammerTest.Grammer.Scopes;
using GrammerTest.Grammer.Tokenizers;
using Jasmine.Spider.Grammer;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GrammerTest.Grammer
{
    public  class AstNodeBuilder:BuilderBase
    {
        public AstNodeBuilder(ISequenceReader<Token> reader,
                              BreakableBlock block,
                              string[] interceptChars,
                              bool hasparent=false) : base(reader,block)

        {
            _interceptChars = interceptChars;
            _hasParent = hasparent;
        }

        public AstNodeBuilder(ISequenceReader<Token> reader,
                               BreakableBlock block,
                               string[] interceptChars,
                               OperatorNode node):base(reader,block)
        {
            _interceptChars = interceptChars;
            _hasParent = true;
            _currentNode = node;
        }

        /// <summary>
        ///  set for priority
        /// </summary>
        private bool _hasParent;
        /// <summary>
        /// current priority,default value -2
        /// </summary>
        private int _currentPrority=-2;
        /// <summary>
        /// higher level intercepters
        /// </summary>
        public string[] _interceptChars { get; }

        public override string Name => "AstNodeBuilder";
     

        public OperatorNode Build()
        {
            while (_reader.HasNext())
            {
                _reader.Next();

                if (_interceptChars.Contains(_reader.Current().Value))
                {
                    return finishBuild();
                }

                switch (_reader.Current().TokenType)
                {

                    /*
                     * in this builder keywords are not allowed 
                     */
                    case TokenType.Keyword:

                        throw new Exception();

                    case TokenType.Operator:


                        /*
                         * check priority
                         *
                         * 1. if current operator is higher than current
                         * start a build,previouce and reset current operator 's right operand
                         * 2. if current operator's priority is lower than current and has parent builder ,Back one token and return 
                         * 3. main work
                         */
                        var priority = _reader.Current().OperatorType.GetPriority();

                        /*
                         * 
                         *  first set priority
                         */
                        if (_currentPrority == -2)
                        {
                            _currentPrority = priority;
                        }
                        else
                        {
                            /*
                             *  start a new priority builder 
                             *
                             */ 

                            if (priority > _currentPrority)
                            {
                                buildNewPriority();
                                continue;
                            }
                            else if (priority < _currentPrority && _hasParent&&_reader.Current().OperatorType!=OperatorType.LeftParenthesis)
                            {
                                return finishBuild();
                            }
                        }

                        switch (_reader.Current().OperatorType)
                        {
                            //can not post to here  
                            //those operator should be handled or intercepted by higher level builder
                            case OperatorType.Binary:
                            case OperatorType.Function:
                            case OperatorType.Coma:
                            case OperatorType.Declare:
                            case OperatorType.ExpressionEnd:
                            case OperatorType.Break:
                            case OperatorType.Continue:
                            case OperatorType.RightParenthesis:
                            case OperatorType.RightBrace:
                            case OperatorType.RightSquare:

                                throwUnexceptedError();

                                break;

                            case OperatorType.Assignment:

                                pushOrderedNode(OperatorNodeFactory.CreateAssigment(_block));

                                break;

                            case OperatorType.AddAsignment:

                                pushOrderedNode(OperatorNodeFactory.CreateAddAssignment(_block));

                                break;

                            case OperatorType.SubtractAsignment:

                                pushOrderedNode(OperatorNodeFactory.CreateSubtractAssignment(_block));

                                break;

                            case OperatorType.MutiplyAsignment:

                                pushOrderedNode(OperatorNodeFactory.CreateMultiplyAssignment(_block));

                                break;

                            case OperatorType.DevideAsignment:

                                pushOrderedNode(OperatorNodeFactory.CreateDevideAsignmentOperatorNode(_block));

                                break;


                            case OperatorType.Ternary:

                                var ternaryNode = new TerbaryBuilder(_reader,_block).Build();

                                ternaryNode.Operands.Insert(0, _currentNode);

                                /*
                                 * back one token ,because ';' has been intercepted by ternary builder;
                                 * 
                                 */
                              
                               //  _reader.Back();

                                return ternaryNode;

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

                            case OperatorType.MemberAccess:

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
                                    /*
                                     *  expressiomn starts with "("
                                     */ 
                                    _currentNode = new ParenthesisBuilder(_reader,_block).Build();
                                    /*
                                     *  _current priority has been set by "(",we should ignore  that operator
                                     */ 
                                    _currentPrority = -2;

                                }
                                else
                                {

                                    if (_reader.Last().TokenType == TokenType.Identifier)
                                    {
                                        /*
                                         * call at epression start
                                         */ 
                                       if(_currentPrority==-1)
                                        {
                                            var node = new CallBuilder(_reader,_block).Build();
                                            node.Operands.Insert(0, _currentNode);
                                            _currentNode = node;
                                            _currentPrority = 8;
                                        }
                                       /*
                                        * member call
                                        */ 
                                       else if(_currentPrority==8)
                                        {
                                            var node = new CallBuilder(_reader,_block).Build();

                                            node.Operands.Insert(0, _currentNode);
                                            _currentNode = node;
                                        }
                                       /*
                                        * start a new priority
                                        */
                                       else
                                        {
                                            buildNewPriority();
                                        }
                                    }
                                    else if (_reader.Last().TokenType != TokenType.Operator)//"ssss"(5+8)
                                    {

                                        throwUnexceptedError();
                                    }
                                    else
                                    {
                                        //start a  new builder
                                        var node = new ParenthesisBuilder(_reader,_block).Build();
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
                                    throwUnexceptedError();
                                }
                                else
                                {
                                    _currentNode = new OperandNode( new ObjectLiteralBuilder(_reader,_block).Build());
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

                                    if(_reader.Last().TokenType!=TokenType.Identifier)
                                    {
                                        throwUnexceptedError();
                                    }
                                    else
                                    {
                                        /*
                                        * call at epression start
                                        */
                                        if (_currentPrority == -1)
                                        {
                                            var node = new ArrayIndexBuilder(_reader,_block).Build();
                                            node.Operands.Insert(0, _currentNode);
                                            _currentNode = node;
                                            _currentPrority = 8;
                                        }
                                        /*
                                         * member call
                                         */
                                        else if (_currentPrority == 8)
                                        {
                                            var node = new ArrayIndexBuilder(_reader,_block).Build();

                                            node.Operands.Insert(0, _currentNode);
                                            _currentNode = node;
                                        }
                                        /*
                                         * start a new priority
                                         */
                                        else
                                        {
                                            buildNewPriority();
                                        }
                                    }
                                }
                                else
                                {
                                    _currentNode = new OperandNode(new JMappingObject(new ArrayLiteralBuilder(_reader,_block).Build()));
                                }
                         
                                break;
                       
                            case OperatorType.Add:

                                pushOrderedNode(OperatorNodeFactory.CreateAdd());

                                break;

                            case OperatorType.Subtract:

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

                                pushUnaryOperator(OperatorNodeFactory.CreateIncrement(_block));

                                break;

                            case OperatorType.Decrement:

                                pushUnaryOperator(OperatorNodeFactory.CreateDecremnet(_block));

                                break;

                            case OperatorType.NewInstance:

                                pushNew(OperatorNodeFactory.CreateNew());

                                break;
                        }

                        break;

                    case TokenType.Identifier:

                        pushIdentifier(OperatorNodeFactory.CreateOperrand(new JString(_reader.Current().Value)));

                        break;

                    case TokenType.String:

                        pushValue(OperatorNodeFactory.CreateOperrand(new JString(_reader.Current().Value)));

                        break;

                    case TokenType.Number:


                        pushValue(OperatorNodeFactory.CreateOperrand(new JNumber(_reader.Current().Value)));

                        break;

                    case TokenType.Bool:


                         pushValue(OperatorNodeFactory.CreateOperrand(new JBool(_reader.Current().Value)));

                        break;
                }
            }

            /*
             *this builder should finish by interceptor not by  finsh iteration
             *
             *
             */

            throwError("expression incomplete;");

           return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void buildNewPriority()
        {
            OperatorNode lastOperand = null;

            if (!_reader.Current().OperatorType.IsUnaryOperator())
            {
                /*
                 *  current node is  binary operator
                 *
                 */
                if (_currentNode.Operands.Count > 1)
                {
                    lastOperand = (OperatorNode)_currentNode.Operands[1];
                    _currentNode.Operands.RemoveAt(1);

                }
                /*
                 *  _current node is unary operator
                 */
                else
                {
                    lastOperand = (OperatorNode)_currentNode.Operands[0];
                    _currentNode.Operands.RemoveAt(0);
                }
            }

            _reader.Back();//back one token

            var node = new AstNodeBuilder(_reader,_block, _interceptChars, lastOperand).Build();

            _currentNode.Operands.Add(node);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OperatorNode finishBuild()
        {
            if (_currentNode != null) //is expression empty;
                _currentNode.DoCheck();//check current operator's operands is ok

            if (_hasParent)
                _reader.Back();

            return _currentNode;
        }
        /// <summary>
        /// tow identifier one by one is a syntax error 
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void checkIdentifer()
        {
            /*
             *  if the identifier  is not  first token of expression ,it should follow an operator or a keyword "in" in foreach block
             */
            if (_reader.HasPrevious())
            {
                if(!(Keywords.KeyWordsUsing.Contains(_reader.Last().Value)||_reader.Last().IsOperator()))

                throwUnexceptedError();
            }
        }

        private void pushNew(NewOperatorNode node)
        {
            /*
             * must be first operator node
             */

            // Debug.Assert(_currentNode == null);

            // _currentNode = node;
            //ignore at current
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushNot(NotOperatorNode node)
        {
            /*
            * must be first operator node
            * cause its priority is higher than && || ,it's always a new stack 
            */
            throwUnexceptedError();

            _currentNode = node;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushOrderedNode(OperatorNode node)
        {
            /*
             * _current node must be existed ,an operator or operand
             */
            if (_currentNode == null)
                throwUnexceptedError();

            /*
             *  check previous operator is completed
             */ 
            _currentNode.DoCheck();

            node.Operands.Add(_currentNode);

            _currentNode = node;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushUnaryOperator(OperatorNode node)
        {
            /*
             *  start with increment or decrement operator
             */ 
           if(_currentNode==null)
            {
                _currentNode = node;
            }
           else
            {
                pushOrderedNode(node);
            }

        }

       [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushIdentifier(OperandNode node)
        {
            checkIdentifer();

            //query object first identifier that means this is a variable
            if(_currentNode==null)
            {
                _currentNode = new QueryScopeOperatorNode((string)node.Output,_block);
            }
            else
            {
                _currentNode.Operands.Add(new QueryScopeOperatorNode((string)node.Output,_block));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    }
}
