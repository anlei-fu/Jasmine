using Jasmine.Common;
using Jasmine.Configuration.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Configuration
{
    public   class PropertyValueParser
    {
        private CharSequenceReader _reader;
        private StringBuilder _builder=new StringBuilder();
        public Template[] Parse(string input)
        {

            _reader = new CharSequenceReader(input);

            var ls = new List<Template>();

            while (_reader.HasNext())
            {
                _reader.Next();

                if(_reader.Current()=='@'&&_reader.HasNext()&&_reader.Forwards()=='{')
                {
                    _reader.Next();

                    if (_builder.Length!=0)
                    {
                        ls.Add(new Template() { Value = _builder.ToString() });
                        _builder.Clear();
                    }

                    var template = new Template();
                    template.PropertyNode = parseProperty();

                    ls.Add(template);
                }
                else if (_reader.Current() == '$' && _reader.HasNext() && _reader.Forwards() == '{')
                {

                    _reader.Next();

                    if (_builder.Length != 0)
                    {
                        ls.Add(new Template() { Value = _builder.ToString() });
                        _builder.Clear();
                    }

                    var template = new Template();
                    template.Value =parseVariable();
                    template.IsVariable = true;
                    ls.Add(template);
                }
                else
                {
                    _builder.Append(_reader.Current());
                }

            }

            if (_builder.Length != 0)
            {
                ls.Add(new Template() { Value = _builder.ToString() });
            }

            return ls.ToArray();
        }

        private string parseVariable()
        {
            var builder = new StringBuilder();

            while (_reader.HasNext())
            {
                _reader.Next();

                if(_reader.Current()=='}'&&_reader.HasNext()&&_reader.Forwards()=='$')
                {
                    _reader.Next();
                    return builder.ToString();
                }
                else
                {
                    builder.Append(_reader.Current());
                }

            }

            throw new ExpressionIncorrectException($"expression varible incompleted!");
        }

        private PropertyNode parseProperty()
        {
            return new PropertyNodeParser().Parse(_reader);
        }
    }
}
