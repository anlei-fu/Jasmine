﻿using Jasmine.Common;
using Jasmine.Configuration.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Configuration
{
    public  class PropertyNodeParser
    {
       private CharSequenceReader _reader;
      

        public PropertyNode Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            _reader = new CharSequenceReader(input);

            return parse0();
        }

        public PropertyNode Parse(CharSequenceReader reader)
        {
            _reader = reader;

            return parse();
        }
        private  PropertyNode  parse0()
        {
            var parameterNode = new PropertyNode();

            parameterNode.IsProperty = true;

            parameterNode.Name = parsePropertyName();

            if(_reader.HasNext())
            {
                parameterNode.Paramters = parseParameter();
            }
            
            return parameterNode;
        }
        private PropertyNode parse()
        {
           

            var node = parse0();

            throwIfHasNoNextOrNext();

            throwIfNotMatch('}', "");

            throwIfHasNoNextOrNext();

            throwIfNotMatch('@', "");

            return node;
        }
        private Dictionary<string, PropertyNode> parseParameter()
        {

            var dic = new Dictionary<string, PropertyNode>();

            while (true)
            {
                var key = parseKey();

                throwIfHasNoNextOrNext();

                if (_reader.Current() == '@'&&_reader.HasNext()&&_reader.Forwards()=='{')
                {
                    _reader.Next();
                    dic.Add(key, parse());

                    throwIfHasNoNextOrNext();

                    if (_reader.Current() == ']')
                    {
                        return dic;
                    }
                    else
                    {
                        throwIfNotMatch(',', "require char ','");
                    }

                }
                else
                {
                    _reader.Back();

                    var parameterNode = new PropertyNode();

                    parameterNode.Value = parseSegment(',', ']');

                    dic.Add(key, parameterNode);

                    if (_reader.Current() == ']')
                    {
                        return dic;
                    }
                    else
                    {
                        throwIfNotMatch(',', "require char ','");
                    }

                }

            }

        }


        private string parsePropertyName()
        {
            var result= parseSegment('[','}');

            if (_reader.Current() == '}')
                _reader.Back();

            return result;
        }
        private string parseKey()
        {
            return parseSegment(':');
        }

        private string parseSegment(char end)
        {
            var builder = new StringBuilder();

            while (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == end)
                    break;

                builder.Append(_reader.Current());
            }

            return builder.ToString();
        }

        private string parseSegment(char end1, char end2)
        {
            var builder = new StringBuilder();
          

            while (_reader.HasNext())
            {
                _reader.Next();

                if (_reader.Current() == end1 || _reader.Current() == end2)
                    break;

                builder.Append(_reader.Current());
            }

            return builder.ToString();
        }

        private void throwIfHasNoNextOrNext()
        {
            if(!_reader.HasNext())
            {
                throw new ExpressionIncorrectException("incompleted expression");
            }

            _reader.Next();
        }


        private void throwIfNotMatch(char c,string msg)
        {
            if(_reader.Current()!=c)
            {
                throw new ExpressionIncorrectException($"unexcepted char '{_reader.Current()}',at line:{_reader.Line}, index:{_reader.LineNumber}");
            }
        }
    }
}
