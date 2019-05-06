using GrammerTest.Grammer.Tokenizers;
using GrammerTest.Grammer.Tokenizers.Exceptions;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Jasmine.Spider.Grammer
{
    public class Tokenizer : ITokenizer<Token>
    {
        /*
         * Keywords
         */
        private const string FOR = "for";
        private const string IN = "in";
        private const string IF = "if";
        private const string ELSEIF = "elif";
        private const string ELSE = "else";
        private const string DO = "do";
        private const string WHILE = "while";
        private const string FOREACH = "foreach";
        private const string TRY = "try";
        private const string CATCH = "catch";
        private const string FINALLY = "finally";
        private const string THROW = "throw";
        private const string RETURN = "return";
        /*
         *  Literal Operators
         */
        private const string FUNCTION = "function";
        private const string BREAK = "break";
        private const string NEW = "new";
        private const string VAR = "var";
        private const string CONTINUE = "continue";
        private const string TRUE = "true";
        private const string FALSE = "false";
        private const string NULL = "null";

        private readonly ITokenFactory<Token> _tokenFactory = TokenFactory.Instance;
        private readonly CharSequenceReader _reader = new CharSequenceReader();

        private readonly StringBuilder _numberBuilder = new StringBuilder();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly StringBuilder _identifierBuilder = new StringBuilder();

        private readonly Dictionary<string, OperatorType> _literalOperatorsMap = new Dictionary<string, OperatorType>()
        {
            {NEW,OperatorType.NewInstance },
            {VAR,OperatorType.Declare },
            {BREAK,OperatorType.Break},
            {CONTINUE,OperatorType.Continue},
            {FUNCTION,OperatorType.Function },
            {RETURN,OperatorType.Return }
        };

        private readonly HashSet<char> _operatorCharsSet = new HashSet<char>()
        {
            '=','!','&','|','.','(',')','*','/','<','>',';','"','\'',',','%','+','-','[',']','?',':'
        };

        private readonly HashSet<char> _whiteSpiceSet = new HashSet<char>()
        {
            ' ','\n','\r','\t'

        };

        private readonly HashSet<string> _keyWordSet = new HashSet<string>()
        {
            FOR,
            IN,
            IF,
            ELSE,
            ELSEIF,
            WHILE,
            DO,
            FOREACH,
            TRY,
            CATCH,
            FINALLY,
            THROW,
        };


        private readonly List<Token> _output = new List<Token>();


        public List<Token> Tokenize(string input)
        {

            _output.Clear();
            _reader.Reset(input);

            while (_reader.HasNext())
            {
                _reader.Next();

                switch (_reader.Current())
                {
                    case '+':

                        parseIncrementOperator('+', OperatorType.Add, OperatorType.Increment, OperatorType.AddAsignment);

                        break;

                    case '-':

                        parseIncrementOperator('-', OperatorType.Subtract, OperatorType.Decrement, OperatorType.SubtractAsignment);

                        break;

                    case '%':

                        parseAssignmentOrSingleOperator(OperatorType.Mod, OperatorType.ModAsignment);

                        break;

                    case '/':

                        if (_reader.HasNext())
                        {
                            _reader.Next();

                            if (_reader.Current() == '/')// single line annotation
                            {
                                skipSingleAnnotation();
                            }
                            else if (_reader.Current() == '*')// mutiple line annotation
                            {
                                skipMutipleLineAnnotation();
                            }
                            else if (_reader.Current() == '=')
                            {
                                pushOperator(OperatorType.DevideAsignment);
                            }
                            else
                            {
                                _reader.Back();
                                pushOperator(OperatorType.Subtract);
                            }
                        }
                        else
                        {
                            pushOperator(OperatorType.Subtract);
                        }


                        break;

                    case '?':

                        pushOperator(OperatorType.Ternary);

                        break;

                    case ':':

                        pushOperator(OperatorType.Binary);

                        break;

                    case '*':

                        parseAssignmentOrSingleOperator(OperatorType.Mutiply, OperatorType.MutiplyAsignment);

                        break;


                    case '&':

                        parseAndOrOperator('&', OperatorType.And);

                        break;

                    case '|':

                        parseAndOrOperator('|', OperatorType.Or);

                        break;

                    case '!':

                        parseSingleOrBiOperatorEndWithEquel('=', OperatorType.NotEquel, OperatorType.Not);

                        break;

                    case '(':

                        pushOperator(OperatorType.LeftParenthesis);

                        break;

                    case ')':

                        pushOperator(OperatorType.RightParenthesis);

                        break;

                    case '=':

                        parseSingleOrBiOperatorEndWithEquel('=', OperatorType.Equel, OperatorType.Assignment);

                        break;

                    case ',':

                        pushOperator(OperatorType.Coma);

                        break;

                    case '<':

                        parseSingleOrBiOperatorEndWithEquel('=', OperatorType.LessEquel, OperatorType.Less);

                        break;

                    case '>':

                        parseSingleOrBiOperatorEndWithEquel('=', OperatorType.BiggerEquel, OperatorType.Bigger);

                        break;

                    case '[':

                        pushOperator(OperatorType.LeftSquare);

                        break;

                    case ']':

                        pushOperator(OperatorType.RightSquare);

                        break;

                    case '.':

                        pushOperator(OperatorType.MemberAccess);

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

                        parseIdentifier();

                        break;
                }
            }


            return _output;
        }


        private void skipWhiteSpice()
        {
            while (_reader.HasNext())
            {
                _reader.Next();

                switch (_reader.Current())
                {
                    case '\r':
                    case '\n':
                    case '\t':
                    case ' ':
                        break;
                    default:

                        _reader.Back();

                        return;
                }
            }
        }
        private void skipSingleAnnotation()
        {
            while (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == '\n')
                    return;
            }
        }
        private void skipMutipleLineAnnotation()
        {
            while (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == '/' && _reader.Last() == '*')
                    return;
            }
        }
        private void parseString()
        {
            while (_reader.HasNext())
            {
                _reader.Next();


                if (_reader.Current() == '"' && _reader.Last() != '\\')
                {
                    pushString(_stringBuilder.ToString());
                    _stringBuilder.Clear();

                    return;
                }
                else
                {
                    _stringBuilder.Append(_reader.Current());
                }
            }
        }

        private void parseNumber()
        {
            bool isDotFound = false;

            _numberBuilder.Append(_reader.Current());

            while (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == '.')
                {
                    if (!isDotFound)
                    {
                        _numberBuilder.Append(_reader.Current());

                        isDotFound = true;

                    }
                    else
                    {
                        _reader.Back();

                        pushNumberToken(_numberBuilder.ToString());

                        _numberBuilder.Clear();

                        return;
                    }
                }
                else if (_reader.Current() >= '0' && _reader.Current() <= '9')
                {
                    _numberBuilder.Append(_reader.Current());
                }
                else
                {
                    pushNumberToken(_numberBuilder.ToString());
                    _numberBuilder.Clear();
                    _reader.Back();

                    return;
                }

            }
        }

        private void parseAndOrOperator(char end, OperatorType type)
        {
            if (_reader.HasNext())
            {
                if (_reader.Current() == end)
                {
                    _output.Add(_tokenFactory.Create(type, _reader.Line, _reader.LineNumber));
                }
                else
                {
                    throw new NotSurpportedOperatorException(_reader.Line, _reader.LineNumber, $" not supported operator (${_reader.Current()})");
                }
            }
            else
            {
                throw new IncompletedOperatorException(_reader.Line, _reader.LineNumber, $" not completed Operator!");
            }
        }

        private void parseSingleOrBiOperatorEndWithEquel(char c, OperatorType d, OperatorType s)
        {
            if (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == c)
                {
                    pushOperator(d);
                }
                else
                {
                    _reader.Back();
                    pushOperator(s);
                }

            }
            else
            {
                pushOperator(s);
            }
        }

        private void parseAssignmentOrSingleOperator(OperatorType single, OperatorType bi)
        {
            if (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == '=')
                {
                    pushOperator(bi);
                }
                else
                {
                    _reader.Back();
                    pushOperator(single);
                }
            }
            else
            {
                pushOperator(single);
            }
        }

        private void parseIncrementOperator(char c, OperatorType single, OperatorType increment, OperatorType assign)
        {
            if (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == c)
                {
                    pushOperator(increment);
                }
                else if (_reader.Current() == '=')
                    pushOperator(assign);
                else
                {
                    _reader.Back();

                    pushOperator(single);
                }

            }
            else
            {
                pushOperator(single);
            }
        }

        private void parseIdentifier()
        {

            if (_reader.Current() >= '0' && _reader.Current() <= '9')
            {
                parseNumber();
            }
            else
            {
                _identifierBuilder.Append(_reader.Current());

                while (_reader.HasNext())
                {
                    _reader.Next();

                    /*
                     *  identifier  required char set [a-zA-z0-9_]
                     */
                    if (checkIsValidIdentifierChar())
                    {
                        _identifierBuilder.Append(_reader.Current());
                    }
                    else
                    {
                        if (!_operatorCharsSet.Contains(_reader.Current()) && !_whiteSpiceSet.Contains(_reader.Current()))
                        {
                            throw new InvalidIdentifierCharException(_reader.Line, _reader.LineNumber, $"{_reader.Current()} can not use to build a identifier name");
                        }

                        var identifier = _identifierBuilder.ToString();

                        /*
                         * to check is really a identifier or literal value
                         * 
                         */
                        if (identifier == NULL)
                        {
                            pushNull();
                        }
                        else if (identifier == TRUE)
                        {
                            pushBool(TRUE);
                        }
                        else if (identifier == FALSE)
                        {
                            pushBool(FALSE);
                        }
                        else if (_keyWordSet.Contains(identifier))//keyword
                        {
                            pushKeyWord(identifier);
                        }
                        else if (_literalOperatorsMap.ContainsKey(identifier))//operator 
                        {
                            pushOperator(_literalOperatorsMap[identifier]);
                        }
                        else
                        {
                            pushIdentifier(_identifierBuilder.ToString());
                        }

                        _reader.Back();

                        _identifierBuilder.Clear();

                        return;
                    }

                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool checkIsValidIdentifierChar()
        {
            return (_reader.Current() >= 'a' && _reader.Current() <= 'z') ||
                   (_reader.Current() >= 'A' && _reader.Current() <= 'Z') ||
                   _reader.Current() == '_' ||
                   (_reader.Current() > '0' && _reader.Current() < '9');
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushIdentifier(string identifier)
        {
            _output.Add(_tokenFactory.Create(identifier, TokenType.Identifier, _reader.Line, _reader.LineNumber));

        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushKeyWord(string keyword)
        {
            _output.Add(_tokenFactory.Create(keyword, TokenType.Keyword, _reader.Line, _reader.LineNumber));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushNull()
        {
            _output.Add(_tokenFactory.Create("null", TokenType.Null, _reader.Line, _reader.LineNumber));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushBool(string value)
        {
            _output.Add(_tokenFactory.Create(value, TokenType.Bool, _reader.Line, _reader.LineNumber));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushNumberToken(string value)
        {
            _output.Add(_tokenFactory.Create(value, TokenType.Number, _reader.Line, _reader.LineNumber));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushString(string str)
        {
            _output.Add(_tokenFactory.Create(str, TokenType.String, _reader.Line, _reader.LineNumber));
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void pushOperator(OperatorType type)
        {
            _output.Add(_tokenFactory.Create(type, _reader.Line, _reader.LineNumber));
        }

    }
}
