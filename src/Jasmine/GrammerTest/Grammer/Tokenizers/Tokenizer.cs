using System.Collections.Generic;
using System.Text;

namespace Jasmine.Spider.Grammer
{
    public  class TokenStreamGenerator
    {
        private const string AND = "&&";
        private const string OR = "||";
        private const string NOTEQUEL = "!=";
        private const string EQUEL = "==";


    
        private const string FOR = "for";
        private const string IN = "in";
        private const string IF = "if";
        private const string ELSEIF = "elif";
        private const string ELSE = "else";
        private const string DO = "";
        private const string WHILE = "";
        private const string FOREACH = "";
        private const string CASE = "";
        private const string SWITCH = "";
        private const string OUT = "";
        private const string DEFAULT = "";
        private const string TRY = "";
        private const string CATCH = "";
        private const string FINALLY = "";
        private const string THROW = "";





        private const string FUNCTION = "";
        private const string BREAK = "break";
        private const string NEW = "new";
        private const string VAR = "var";
        private const string CONTINUE = "continue";
        private const string TRUE = "true";
        private const string FALSE = "false";
        private const string NULL = "null";

        private StringBuilder _numberBuilder = new StringBuilder();
        private StringBuilder _stringBuilder=new StringBuilder();
        private StringBuilder _identifierBuilder=new StringBuilder();
        private string _input;
        private int _currentIndex=-1;
        private char _currentChar=>_input[_currentIndex];
        private bool hasNext()
        {
            return _currentIndex+1<_input.Length;
        }
        private char previous()
        {
            return _input[--_currentIndex];
        }

        private int _line = 1;
        private int _lineNumber;
        private char next()
        {
            if (_input[++_currentIndex] == '\n')
            {
                _line++;
                _lineNumber = 1;
            }
            else
            {
                _lineNumber++;
            }
            return _currentChar;
        }

        private List<Token> _tokens=new List<Token>();
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

        private void parseAssignment(OperatorType single,OperatorType bi)
        {
            if(hasNext())
            {
                if(next()=='=')
                {
                    pushOperator(bi);
                }
                else
                {
                    previous();
                    pushOperator(single);
                }
            }
            else
            {
                pushOperator(single);
            }
        }

        private void parseIncrement(char c,OperatorType single,OperatorType increment ,OperatorType assign)
        {
            if(hasNext())
            {
                var d = next();

                if (d == c)
                {
                    pushOperator(increment);
                }
                else if (d == '=')
                    pushOperator(assign);
                else
                {
                    previous();

                    pushOperator(single);
                }

            }
            else
            {
                pushOperator(single);
            }
        }

        public IList<Token> GetTokenStream(string input)
        {
            _input = input;

            while (hasNext())
            {
                next();

                switch (_currentChar)
                {
                    case '+':

                        parseIncrement('+', OperatorType.Add, OperatorType.Increment, OperatorType.AddAsignment);

                        break;

                    case '-':

                        parseIncrement('+', OperatorType.Reduce, OperatorType.Decrement, OperatorType.ReduceAsignment);

                        break;

                    case '%':

                        parseAssignment(OperatorType.Mod, OperatorType.ModAsignment);

                        break;

                    case '/':

                        if(hasNext())
                        {
                            next();

                            if (_currentChar == '/')
                                skipSingleAnnotation();
                            else if (_currentChar == '*')
                                skipMutipleAnnotation();
                            else if (_currentChar == '=')
                                pushOperator(OperatorType.DevideAsignment);
                            else
                            {
                                previous();
                                pushOperator(OperatorType.Reduce);
                            }
                        }
                        else
                        {
                            pushOperator(OperatorType.Reduce);
                        }


                        break;

                    case ':':

                        pushOperator(OperatorType.Semicolon);

                        break;

                    case '*':

                        parseAssignment(OperatorType.Mutiply,OperatorType.MutiplyAsignment);

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
        private void pushNull()
        {

        }
        private void pushBool(string value)
        {

        }

        private HashSet<string> _keyWords = new HashSet<string>()
        {
            FOR,
            IN,
            IF,
            ELSE,
            ELSEIF,
        };
        private Dictionary<string, OperatorType> _operators = new Dictionary<string, OperatorType>()
        {
            {NEW,OperatorType.New },
            {VAR,OperatorType.Var },
            {BREAK,OperatorType.Break},
            {CONTINUE,OperatorType.Continue},
        };

        private HashSet<char> _operatorChars = new HashSet<char>()
        {
            '=','!','&','|','.','(',')','*','/','<','>',';','"','\'',',','%','+','-','%'

        };
        private HashSet<char> _whiteSpice = new HashSet<char>()
        {
            ' ','\n','\r','\t'

        };

        private void pushNumberToken(string value)
        {
            _tokens.Add(new Token(value, TokenType.Number));
        }


        private void skipSingleAnnotation()
        {
            while(hasNext())
            {
                if (next() == '\n')
                    return;
            }
        }
        private void skipMutipleAnnotation()
        {
            while(hasNext())
            {
                if (next() == '/' && _input[_currentIndex - 1] == '*')
                    return;
            }
        }



        private void parseIdentifierOrKeyword()
        {

            if (_currentChar>='0'&&_currentChar<='9')
            {
                bool isDotFound = false;
                _numberBuilder.Append(_currentChar);



                while (hasNext())
                {
                    next();

                    if(_currentChar=='.')
                    {
                        if (!isDotFound)
                        {
                            _numberBuilder.Append(_currentChar);
                            isDotFound = true;
                            
                        }
                        else
                        {
                            previous();
                            pushNumberToken(_numberBuilder.ToString());

                            _numberBuilder.Clear();
                            return;
                        }
                    }
                    else if(_currentChar >= '0' && _currentChar <= '9')
                    {
                        _numberBuilder.Append(_currentChar);
                    }
                    else
                    {
                        pushNumberToken(_numberBuilder.ToString());
                        _numberBuilder.Clear();
                        previous();

                        return;
                    }

                }

            }
            else
            {
                _identifierBuilder.Append(_currentChar);

                while (hasNext())
                {
                    next();

                    if ((_currentChar >= 'a' && _currentChar <= 'z') || (_currentChar >= 'A' && _currentChar <= 'Z') || _currentChar == '_' || (_currentChar > '0' && _currentChar < '9'))
                        _identifierBuilder.Append(_currentChar);
                    else
                    {
                        if (!_operatorChars.Contains(_currentChar) && !_whiteSpice.Contains(_currentChar))
                        {
                            throw new System.Exception();
                        }

                        var identifier = _identifierBuilder.ToString();

                        if(identifier==NULL)
                        {
                            pushNull();
                        }
                        else if(identifier==TRUE)
                        {
                            pushBool(TRUE);
                        }
                        else if(identifier==FALSE)
                        {
                            pushBool(FALSE);
                        }
                        else if (_keyWords.Contains(identifier))//keyword
                        {
                            pushKeyWord(identifier);
                        }
                        else if (_operators.ContainsKey(identifier))//operator 
                        {
                            pushOperator(_operators[identifier]);
                        }
                        else
                        {
                            pushIdentifier(_identifierBuilder.ToString());
                        }

                        previous();

                        _identifierBuilder.Clear();

                        return;
                    }

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
                if(next()=='"'&&_input[_currentIndex-1]!='\\')
                {
                    pushString(_stringBuilder.ToString());
                    _stringBuilder.Clear();
                    return;
                }
                else
                {
                    _stringBuilder.Append(_currentChar);
                }
            }

         

        }
    }
}
