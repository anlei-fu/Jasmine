using GrammerTest.Grammer;
using GrammerTest.Grammer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Jasmine.Spider.Grammer
{
    public class AstTreeBuilder
    {
        private const string ATTRIBUTE = "";
        private const string ANY_ATTRIBUTE = "";
        private const string ALL_ATTRIBUTE = "";
        //functions
        /// <summary>
        ///  e.attr.containsAny(name="div",);
        /// </summary>
        private const string CONTAINS_ANY = "";
        private const string CONTAINS_ALL = "";
        private const string SELECTALL = "";
        private const string SELECTDIRECT = "";
        private const string MAP = "";
        private const string ARRAY = "";
        private const string LENTH = "";
        private const string GETITEM = "";
        private const string ADD = "";
        public const string REMOVE = "";

        //members
        private const string ATTR = "";
        private const string NAME = "";
        private const string PARRENT = "";
        private const string PREVIOUS = "";
        private const string NEXT = "";

        private int _currentIndex = -1;
        private bool hasNext()
        {
            return _currentIndex + 1 < _tokens.Count;
        }

        private Token next()
        {
            return null;
        }
        private Token previous()
        {
            return null;
        }

        private bool _isInExpression;

        private Stack<Stack<OperatorNode>> _stacks;
        private Stack<OperatorNode> _currentStack;
        private List<Token> _tokens;
        private Token _currentToken;
        private Scope _currentStatement;
        private Expression _currentExpression;
        private OperatorNode _currentOperatorNode;
        private OperandNode _currentOperandNode;
        private Scope _currentScope;
        private bool _isExpressionStarted;


        private OperandNode _currentLeftOperend;
        private OperandNode _currntRightOperand;



        private Token _lastToken;


        private void pushIntoScopeStack(Scope scope)
        {

        }

        private void checkExpressionEnd()
        {
            if(_isExpressionStarted)
            {
                throw new GrammerException();
            }

            _currentScope

        }

        private void checkOperator()
        {

        }
        private void checkKeyword()
        {
            if(_isExpressionStarted)
            {
                
            }

        }
   


        private void checkExpressionOperatorStart()
        {

        }

        private Operand _expressionStartOperand;
       
        private void buildExpression()
        {
            var expression = new Expression()
            {
                Parent = _currentScope,
            };

            _currentScope = expression;

            pushIntoScopeStack(expression);

            previous();

            while (hasNext())
            {
                switch (next().TokenType)
                {
                    case TokenType.Keyword:
                        //throw error
                        break;
                    case TokenType.Operator:

                        if (!_isExpressionStarted)
                            checkExpressionOperatorStart();

                        switch (_currentToken.OperatorType)
                        {
                            case OperatorType.Assignment:
                                break;
                            case OperatorType.And:
                                break;
                            case OperatorType.Or:
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
                                break;
                            case OperatorType.RightParenn:
                                break;
                            case OperatorType.LeftBrace:
                                break;
                            case OperatorType.RightBrace:
                                break;
                            case OperatorType.LeftSquare:
                                break;
                            case OperatorType.RightSquare:
                                break;
                            case OperatorType.Add:
                                break;
                            case OperatorType.Reduce:
                                break;
                            case OperatorType.Mode:
                                break;
                            case OperatorType.Mutiply:
                                break;
                            case OperatorType.Devide:
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
                                break;
                            case OperatorType.BiggerEquel:
                                break;
                            case OperatorType.Less:
                                break;
                            case OperatorType.LessEquel:
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

          

        }

        public void build(List<Token> tokens)
        {
            _tokens = tokens;

            while (hasNext())
            {

                next();

                chekGrammer();

                switch (_currentToken.TokenType)
                {
                    case TokenType.Keyword:

                        checkExpressionEnd();

                        if (_currentToken.Value == "for")
                        {
                            buildFor();
                        }
                        else if (_currentToken.Value == "if")
                        {
                            buildIf();
                        }
                        else if (_currentToken.Value == "elif")
                        {
                            buildElif();

                        }
                        else if (_currentToken.Value == "else")
                        {
                            buildElse();

                        }
                        else
                        {
                            Console.WriteLine(" un surpported keyword!");
                        }

                        break;


                    case TokenType.Operator:
                    case TokenType.Identifier:
                        buildExpression();
                        break;

                    case TokenType.String:
                    case TokenType.Number:
                    case TokenType.Bool:

                        break;
                }

                _lastToken = _currentToken;
            }


        }



        private void checkOperatorFinished()
        {
           if(_currentOperatorNode==null)
            {

            }
           else
            {

            }

        }

        private void checkOprandNotNull(OperatorConstraint constraint)
        {
            //requir operand not null
            if(_currentOperandNode==null)
            {
                throw new GrammerException();
            }
            else
            {
                //operand object is match
                if(!constraint.Equles(_currentOperandNode.Object.Type))
                {

                }
            }
        }

        private void checkInputConstraint(OperatorConstraint constraint)
        {
            if(_lastToken!=null)
            {

            }
            else
            {
                // expression first operator
                // means left operand should have been set
                // if not 
                //

                checkOprandNotNull(constraint);
            }
        }

        /// <summary>
        /// ..
        /// 
        /// 
        /// </summary>
        private void requirePreviousIdentifier()
        {

        }

        private void buildOperator(OperatorType op)
        {

            //create temp operater node
            var node = new OperatorNode(op);

            switch (op)
            {
                case OperatorType.Assignment:
                case OperatorType.Equel:
                case OperatorType.NotEquel:
                    checkInputConstraint(OperatorConstraint.Varible);
                    break;

                case OperatorType.And:
                case OperatorType.Or:
                case OperatorType.Not:
                    checkInputConstraint(OperatorConstraint.Bool);
                    break;
               
                
               
             

                case OperatorType.Add:
                case OperatorType.Reduce:
                case OperatorType.Mode:
                case OperatorType.Mutiply:
                case OperatorType.Devide:
                case OperatorType.LeftIncrement:
                case OperatorType.RightIncrement:
                case OperatorType.LeftDecrement:
                case OperatorType.RightDecrement:
                case OperatorType.Bigger:
                case OperatorType.BiggerEquel:
                case OperatorType.Less:
                case OperatorType.LessEquel:
                    checkInputConstraint(OperatorConstraint.Number);
                    break;
                case OperatorType.Coma:
                    break;
                case OperatorType.ExpressionEnd:
                    break;
                
               
                case OperatorType.Var:
                    reqireExpressionNotStarted();
                    checkInputConstraint(OperatorConstraint.String);
                    break;
                case OperatorType.QueryObJect:
                case OperatorType.New:
                case OperatorType.Member:
                    checkInputConstraint(OperatorConstraint.String);
                    break;

                case OperatorType.Call:

                case OperatorType.LeftParenn:


                case OperatorType.RightParenn:
                case OperatorType.LeftBrace:
                case OperatorType.RightBrace:
                case OperatorType.LeftSquare:
                case OperatorType.RightSquare:
                case OperatorType.Break:
                case OperatorType.Continue:
                    break;
            }


            buildOperatorChain(node);

        }


        private void buildOperatorChain(OperatorNode NewNode)
        {

        }

        private void reqireExpressionNotStarted()
        {

        }
        private void buildElse()
        {

        }
        private  void buildElif()
        {

        }

        private void chekGrammer()
        {
            switch (_currentToken.TokenType)
            {
                case TokenType.Keyword:
                    break;
                case TokenType.Operator:

                    //two operator
                    if(_lastToken.TokenType==TokenType.Operator)
                    {
                        throw new GrammerException();
                    }


                    break;
                case TokenType.Identifier:

                    if (_lastToken.TokenType == TokenType.Identifier)
                    {
                        throw new GrammerException();
                    }

                    break;
                case TokenType.String:

                    if (_lastToken.TokenType == TokenType.Identifier)
                    {
                        throw new GrammerException();
                    }

                    break;
                case TokenType.Number:
                    break;
                case TokenType.Bool:
                    break;
                default:
                    break;
            }

            


        }
        private void rightBide()
        {

        }

        private void checkIdentifierExists(string identifer)
        {

        }
        private void buildFor()
        {

        }

        private void buildIf()
        {

        }










    }
}
