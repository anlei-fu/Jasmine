using System.Collections.Generic;
using System.Text;

namespace Jasmine.Spider.Grammer
{
    public  class TokenStreamFactory
    {
        private const string AND = "&&";
        private const string OR = "||";
        private const string NOTEQUEL = "!=";
        private const string EQUEL = "==";



        private const string FOR = "";
        private const string IN = "";
        private const string IF = "";
        private const string ELSEIF = "";
        private const string ELSE = "";
        private const string BREAK = "";
        private const string NEW = "";
        private const string VAR = "";
        private const string CONTINUE = "";


        private StringBuilder _stringBuilder;
        private StringBuilder _identifierBuilder;
        private string _pattern;
        private int _currentIndex;
        private char _currentChar;
        private bool hasNext()
        {
            return true;
        }
        private void previous()
        {

        }
        private char next()
        {
            return _pattern[++_currentIndex];
        }

        private List<Token> _tokens;
        private void parseOperator(char end, OperatorType type)
        {
            if (hasNext())
            {
                if (next() == '&')
                {
                    _tokens.Add(new Token(type));
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

        private void pushOperator(OperatorType type)
        {
            _tokens.Add(new Token(type));
        }
        private void throwExpressionInCompletedException()
        {

        }
        private void parseMayBeDouble(char c,OperatorType d,OperatorType s)
        {
            if(hasNext())
            {
                if(next()==c)
                {
                    pushOperator(d);
                }
                else
                {
                    previous();
                    pushOperator(s);
                }

            }
            else
            {
                pushOperator(s);
            }
        }

        public IList<Token> GetTokenStream()
        {
            while (hasNext())
            {
                switch (_currentChar)
                {
                    case '+':

                        break;

                    case '-':

                        break;

                    case '%':

                        break;

                    case '/':

                        break;

                    case ':':

                        break;

                    case '*':

                        break;


                    case '&':

                        parseOperator('&', OperatorType.And);

                        break;

                    case '|':

                        parseOperator('|', OperatorType.Or);

                        break;

                    case '!':

                        parseMayBeDouble('=', OperatorType.NotEquel, OperatorType.Not);

                        break;

                    case '(':

                        pushOperator(OperatorType.LeftBrace);

                        break;

                    case ')':

                        pushOperator(OperatorType.RightBrace);

                        break;

                    case '=':

                        parseMayBeDouble('=', OperatorType.Equel, OperatorType.Assignment);

                        break;

                    case ',':

                        pushOperator(OperatorType.Coma);

                        break;

                    case '<':

                        parseMayBeDouble('=', OperatorType.LessEquel, OperatorType.Less);

                        break;

                    case '>':

                        parseMayBeDouble('=', OperatorType.BiggerEquel, OperatorType.Bigger);

                        break;

                    case '[':

                        pushOperator(OperatorType.LeftSquare);

                        break;

                    case ']':

                        pushOperator(OperatorType.RightSquare);

                        break;

                    case '.':

                        pushOperator(OperatorType.Member);

                        break;

                    case '"':

                        parseString();

                        break;

                    case ';':

                        pushOperator(OperatorType.ExpressionEnd);

                        break;
                    case '{':

                        pushOperator(OperatorType.LeftBrace);

                        break;

                    case '}':

                        pushOperator(OperatorType.RightBrace);

                        break;

                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':

                        skipWhiteSpice();

                        break;

                    default:

                        parseIdentifierOrKeyword();

                        break;
                }
            }

            return _tokens;
        }

        private void skipWhiteSpice()
        {
            while (hasNext())
            {
                switch (next())
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        break;
                    default:
                        previous();
                        return;
                }
            }
        }

        private void pushIdentifier(string identifier)
        {
            _tokens.Add(new Token(identifier, TokenType.Identifier));
        }
        private void pushKeyWord(string keywords)
        {
            _tokens.Add(new Token(keywords, TokenType.Keyword));
        }

        private HashSet<string> _keyWords = new HashSet<string>()
        {
            FOR,
            IN,
            VAR,
            IF,
            ELSE,
            ELSEIF,
            BREAK,
            CONTINUE

        };

        private HashSet<char> _operatorChars = new HashSet<char>()
        {
            '=','!','&','|','.','(',')','*','/','<','>',';','"','\'',',','%','+','-','%'

        };
        private HashSet<char> _whiteSpice = new HashSet<char>();

        private void parseIdentifierOrKeyword()
        {
            _identifierBuilder.Append(_currentChar);

            while (hasNext())
            {
                next();

                if ((_currentChar >= 'a' && _currentChar <= 'z') || (_currentChar >= 'A' && _currentChar <= 'Z') || _currentChar == '_' || (_currentChar > '0' && _currentChar < '9'))
                    _identifierBuilder.Append(_currentChar);
                else
                {
                    if(!_operatorChars.Contains(_currentChar)||_whiteSpice.Contains(_currentChar))
                    {

                    }

                    if(_keyWords.Contains(_identifierBuilder.ToString()))
                    {
                        pushKeyWord(_identifierBuilder.ToString());
                    }
                    else
                    {
                        pushIdentifier(_identifierBuilder.ToString());
                    }

                    _identifierBuilder.Clear();
                }

            }
        }

        private void pushString(string str)
        {
            _tokens.Add(new Token(str, TokenType.String));
        }

       
        private void parseString()
        {
            while(hasNext())
            {
                if(next()=='"'&&_pattern[_currentIndex-1]!='\\')
                {
                    pushString(_stringBuilder.ToString());
                }
                else
                {
                    _stringBuilder.Append(_currentChar);
                }
            }

            _stringBuilder.Clear();

        }
    }
}
