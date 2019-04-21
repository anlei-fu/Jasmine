using System.Collections.Generic;
using System.Text;
using static Jasmine.Spider.Grammer.AstTree;

namespace Jasmine.Spider.Grammer
{
    public  class AstTreeParser
    {
        private const string ATTRIBUTE = "";
        private const string ANY_ATTRIBUTE = "";
        private const string ALL_ATTRIBUTE = "";
        private const string NAME = "";
        private const string AND= "";
        private const string OR = "";
        private const string NOTEQUEL = "";
        private const string EQUEL = "";

        private Stack<Stack<OperatorNode>> _stacks;
        private Stack<OperandNode> _currentStack;
        private string _pattern;

        private int _currentIndex;
        private char _currentChar;
        private bool hasNext()
        {
            return true;
        }

        private char next()
        {
            return _pattern[++_currentIndex];
        }

        private List<Segment> _segements;
           

            

        private string _operator;
        private void previous()
        {

        }

        private void parseOperator(char end,OperatorType type)
        {
            if (hasNext())
            {
                if (next() == '&')
                {
                    _segements.Add(new Segment(null, true, type));
                }
                else
                {
                    throw new AstGrammerException($"not supported operator (${_currentChar})");
                }
            }
            else
            {
                throw new AstGrammerException($"not completed expression!");
            }
        }

        private void throwExpressionInCompletedException()
        {

        }

        private void addOperatorSegment(OperatorType type)
        {
            _segements.Add(new Segment(null, true, type));
        }

        public AstTree.OperatorNode Parse(string pattern)
        {
            parseSegments();

           return creaateTree();
        }

        private void parseSegments()
        {
            while (hasNext())
            {
                switch (_currentChar)
                {
                    case '&':

                        parseOperator('&', OperatorType.And);

                        break;

                    case '|':

                        parseOperator('|', OperatorType.Or);

                        break;

                    case '!':

                        if (hasNext())
                        {
                            if (next() == '=')
                            {
                                addOperatorSegment(OperatorType.NotEquel);
                            }
                            else
                            {
                                previous();

                                addOperatorSegment(OperatorType.Not);
                            }
                        }
                        else
                        {
                            throwExpressionInCompletedException();
                        }

                        break;

                    case '(':

                        addOperatorSegment(OperatorType.LeftBrace);

                        break;

                    case ')':

                        addOperatorSegment(OperatorType.RightBrace);

                        break;

                    case '=':

                        parseOperator('=', OperatorType.Equel);

                        break;

                    case '.':

                        addOperatorSegment(OperatorType.Member);

                        break;

                    case '"':

                        parseVariable();

                        break;

                    default:

                        parseFunction();

                        break;
                }
            }
        }


        private AstTree.OperatorNode creaateTree()
        {
            return null;
        }

        private StringBuilder _parameterBuilder;

        private void parseVariable()
        {
            _parameterBuilder.Append(_currentChar);

            while (hasNext())
            {
                next();

                if(_currentChar=='"'&&_pattern[_currentIndex-1]!='\\')
                {
                    
                }
            }
        }

        private void parseFunction()
        {

        }

       struct Segment
        {
            public Segment(string value,bool isOperator,OperatorType type)
            {
                OperatorType = type;
                IsOperator = isOperator;
                Value = value;
            }
            public OperatorType OperatorType { get; set; }
            public bool IsOperator { get; set; }
            public string Value { get; set; }
        }
        enum OperatorType
        {
            And,
            Or,
            Not,
            Equel,
            Member,
            NotEquel,
            Call,
            LeftBrace,
            RightBrace,
        }

        static class OperatorTypeExtension
        {
            public static int Priority( OperatorType type)
            {
                switch (type)
                {
                    case OperatorType.And:
                    case OperatorType.Or:
                        return 4;
                    case OperatorType.Not:
                        return 3;
                    case OperatorType.Equel:
                    case OperatorType.NotEquel:
                        return 2;
                    case OperatorType.Member:
                    case OperatorType.Call:
                    case OperatorType.LeftBrace:
                    case OperatorType.RightBrace:
                        return 1;
                    default:
                        return 0;
                }
            }
        }

    }
}
