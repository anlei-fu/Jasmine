using GrammerTest.Grammer;
using GrammerTest.Grammer.Exceptions;
using System;
using System.Collections.Generic;

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
        private bool _isExpressionStart;


        private OperandNode _currentLeftOperend;
        private OperandNode _currntRightOperand;



        private Token _lastToken;

        private void checkExpressionEnd()
        {
            if(_isExpressionStart)
            {
                throw new GrammerException();
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


                        }
                        else if (_currentToken.Value == "else")
                        {


                        }
                        else if (_currentToken.Value == "break")
                        {

                        }
                        else if (_currentToken.Value == "continue")
                        {

                        }
                        else
                        {
                            Console.WriteLine(" un surpported keyword!");
                        }

                        



                        break;

                    case TokenType.Operator:

                        requirePreviousIdentifier();

                        buildOperator(_currentToken.OperatorType);

                        break;


                    case TokenType.Identifier:
                        
                        if(_currentOperandNode==null)
                        {
                            checkIdentifierExists(_currentToken.Value);

                            _currentOperandNode = new OperandNode() { Name = _currentToken.Value };

                            _currentOperatorNode = new OperatorNode(OperatorType.QueryObJect);

                            rightBide();
                        }
                        else
                        {
                            chekGrammer();

                        }



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
