using Jasmine.Common;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Implements;
using Jasmine.Reflection;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Jasmine.Extensions;

namespace Jasmine.Orm
{
    public class SqlTemplate : INameFearture
    {
        public string Name { get; set; }
        public SqlTemplateSegment[] Segments { get; set; }
        public string RawString { get; set; }
    }

    public struct SqlTemplateSegment
    {
        public SqlTemplateSegment(string value, bool isVarible)
        {
            Value = value;
            IsVarible = isVarible;
        }
        public string Value { get; }
        public bool IsVarible { get; }
    }

    public class SqlTemplateParser
    {
        public static SqlTemplate Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var reader = new CharSequenceReader(input);

            bool parsingParameter = false;

            var template = new SqlTemplate();

            template.RawString = input;

            var ls = new List<SqlTemplateSegment>();

            var segmentBuilder = new StringBuilder();

            while (reader.HasNext())
            {
                reader.Next();

                if (reader.Current() == '@' && !parsingParameter)
                {
                    if (segmentBuilder.Length != 0)
                    {
                        ls.Add(new SqlTemplateSegment(segmentBuilder.ToString(), false));

                        segmentBuilder.Clear();
                    }

                    parsingParameter = true;
                }
                else if (parsingParameter && (reader.Current() == ' ' || reader.Current() == '\r' || reader.Current() == '\n'))
                {
                    ls.Add(new SqlTemplateSegment(segmentBuilder.ToString(), true));

                    segmentBuilder.Clear();

                    parsingParameter = false;
                }
                else
                {
                    segmentBuilder.Append(reader.Current());
                }

            }

            if (segmentBuilder.Length != 0)
            {
                if (parsingParameter)
                {
                    ls.Add(new SqlTemplateSegment(segmentBuilder.ToString(), true));
                }
                else
                {
                    ls.Add(new SqlTemplateSegment(segmentBuilder.ToString(), false));
                }
            }

            template.Segments = ls.ToArray();

            return template;

        }
    }

    public class SqlTemplateProvider : IReadOnlyCollection<SqlTemplate>
    {
        private const string ORM_TEMPLATE_GROUP = "orm-template-group";
        private const string TEMPLATE = "template";
        private const string Name = "name";

        private ConcurrentDictionary<string, SqlTemplate> _map = new ConcurrentDictionary<string, SqlTemplate>();
        public int Count => _map.Count;


        public void Load(string xmlPath)
        {
            var xml = new XmlDocument();

            foreach (var item in xml.GetAll(x=>x.Name==ORM_TEMPLATE_GROUP))
            {
                var name = item.GetAttribute(Name);

                if (name == null)
                {
                    throw new RequiredAttributeNotFoundException($"required attribute {Name} of {ORM_TEMPLATE_GROUP} not found!");
                }

                foreach (var templateNode in item.GetAll(x => x.Name == TEMPLATE))
                {
                    var tptName = templateNode.GetAttribute(Name);

                    if (name == null)
                    {
                        throw new RequiredAttributeNotFoundException($"required attribute {Name} of {TEMPLATE} not found!");
                    }

                    var template = SqlTemplateParser.Parse(templateNode.InnerText);

                    template.Name = name + tptName;

                    Add(template);
                }

            }
        }

        public void Remove(string name)
        {
            _map.TryRemove(name, out var _);
        }
        public bool Contains(string name)
        {
            return _map.ContainsKey(name);
        }
        public SqlTemplate GetTemplate(string name)
        {
            return _map.TryGetValue(name, out var result) ? result : null;
        }

        public void Add(string name, string templateStr)
        {
            var template = SqlTemplateParser.Parse(templateStr);

            template.Name = name;

            Add(template);
        }
        public void Add(SqlTemplate template)
        {
            if (!_map.TryAdd(template.Name, template))
            {
                throw new TemplateAlreadyExistsException($"{template.Name} alrady exists!");
            }
        }

        public IEnumerator<SqlTemplate> GetEnumerator()
        {
            foreach (var item in _map.Values)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }

    public class SqltemplateConverter
    {
        private SqltemplateConverter()
        {

        }
        public static readonly SqltemplateConverter Instance = new SqltemplateConverter();

        
        public string Convert(SqlTemplate template, object parameters)
        {
            var map = genarateMap(string.Empty, parameters);


            var builder = new StringBuilder();

            foreach (var item in template.Segments)
            {
                if (item.IsVarible)
                {
                    if (map.ContainsKey(item.Value))
                    {
                        builder.Append(map[item.Value]);
                    }
                    else
                    {
                        throw new ParameterNotFoundException($"requierd parameter @{item.Value} can not be found by given parameter instance ({parameters}),{template.RawString} ");
                    }


                }
                else
                {
                    builder.Append(item.Value);
                }
            }

            return builder.ToString();
        }


        private Dictionary<string, string> genarateMap(string prefix, object obj)
        {
            var map = new Dictionary<string, string>();

            foreach (var item in JasmineReflectionCache.Instance.GetItem(obj.GetType()).Properties)
            {
                var value = item.Getter.Invoke(obj);
                var valueType = value.GetType();

                var name = prefix + item.Name.ToLower();

                if (BaseTypes.Base.Contains(valueType))
                {
                    map.Add(name, DefaultBaseTypeConvertor.Instance.ConvertToSqlString(valueType, value));
                }
                else if (valueType.IsClass)
                {
                    var result = genarateMap(name, value);

                    foreach (var pair in genarateMap(name, value))
                    {
                        map.Add(pair.Key, pair.Value);
                    }
                }

            }

            return map;
        }

    }
}
