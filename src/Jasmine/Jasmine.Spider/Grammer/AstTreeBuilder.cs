using System.Collections.Generic;

namespace Jasmine.Spider.Grammer
{
    public  class AstTreeBuilder
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
      
        private bool hasNext()
        {
            return true;
        }

        private Token next()
        {
            return null;
        }
        private Token previous()
        {
            return null;
        }

        private bool _isInexpression;

        private Stack<Stack<OperatorNode>> _stacks;
        private Stack<OperatorNode> _currentStack;
        private List<Token> _tokens;
        private Token _currentToken;
        private Scope _currentStatement;
        private Expression _currentExpression;
        private OperatorNode _currentOperatorNode;


        public void build()
        {
            while (hasNext())
            {

                switch (next().TokenType)
                {
                    case TokenType.Keyword:

                        if(_isInexpression)
                        {

                        }
                        else
                        {
                            if(_currentToken.Value=="for")
                            {

                            }
                            else if(_currentToken.Value=="if")
                            {

                            }
                            else
                            {
                               

                            }


                        }

                        break;

                    case TokenType.Operator:


                        break;
                    case TokenType.Identifier:
                        break;
                    case TokenType.String:
                        break;
                    case TokenType.Numble:
                        break;
                    case TokenType.Bool:
                        break;
                    default:
                        break;
                }


            }


        }


       
        

       

      
       

    }
}
