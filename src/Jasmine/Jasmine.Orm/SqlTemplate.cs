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
       
        public string[] GetVaribleNames()
        {
            var result = new List<string>();

            foreach (var item in Segments)
            {
                if (item.IsVarible)
                    result.Add(item.Value);
            }

            return result.ToArray();
        }
        private string _rawString;
        public string RawString
        {
            get
            {
                if (_rawString == null)
                {
                    var builder = new StringBuilder();

                    foreach (var item in Segments)
                    {
                        if (item.IsVarible)
                            builder.Append("@");

                        builder.Append(item.Value);
                    }

                    _rawString = builder.ToString();
                }

                return _rawString;
            }
            set
            {
                _rawString = value;
            }
        }
    }

    public class SqlTemplateMaker
    {
        public static SqlTemplate MakeInsert(string table, params string[] columns)
        {
            var left = MakeInsertLeft(table, columns);
            var right = MakeInsertRight(columns);

            var ls = new List<SqlTemplateSegment>();
            ls.Add(new SqlTemplateSegment(left, false));
            ls.AddRange(right.Segments);

           return new SqlTemplate()
            {
                Segments = ls.ToArray()
            };
        }

        public static string MakeInsertLeft(string table, params string[] columns)
        {
            var builder = new StringBuilder();

            builder.Append($"Insert Into {table}(");

            foreach (var item in columns)
            {
                builder.Append(item.Replace(".","_")).Append(",");
            }

            builder.RemoveLastComa();

            builder.Append(") Values ");

            return builder.ToString();

        }
        public static SqlTemplate MakeInsertRight( params string[] columns)
        {
            var ls = new List<SqlTemplateSegment>();

            ls.Add(new SqlTemplateSegment("(", false));

            foreach (var item in columns)
            {
                ls.Add(new SqlTemplateSegment(item, true));

                ls.Add(new SqlTemplateSegment(",", false));
            }

            if (ls[ls.Count - 1].Value == ",")
                ls.RemoveAt(ls.Count - 1);

            ls.Add(new SqlTemplateSegment(")", false));

            return new SqlTemplate()
            {
                Segments = ls.ToArray()
            };

        }


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
        private SqlTemplateProvider()
        {

        }

        public static readonly SqlTemplateProvider Instance = new SqlTemplateProvider();

        private const string ORM_TEMPLATE_GROUP = "orm-template-group";
        private const string TEMPLATE = "template";
        private const string NAME = "name";

        private ConcurrentDictionary<string, SqlTemplate> _map = new ConcurrentDictionary<string, SqlTemplate>();
        public int Count => _map.Count;


        public void Load(string xmlPath)
        {
            var xml = new XmlDocument();

            xml.Load(xmlPath);

            foreach (var item in xml.GetAll(x => x.Name == ORM_TEMPLATE_GROUP))
            {
                var name = item.GetAttribute(NAME);

                if (name == null)
                {
                    throw new RequiredAttributeNotFoundException($"required attribute {NAME} of {ORM_TEMPLATE_GROUP} tag not found!");
                }

                foreach (var templateNode in item.GetAll(x => x.Name == TEMPLATE))
                {
                    var tptName = templateNode.GetAttribute(NAME);

                    if (tptName == null)
                    {
                        throw new RequiredAttributeNotFoundException($"required attribute {NAME} of {TEMPLATE} tag not found!");
                    }

                    var template = SqlTemplateParser.Parse(templateNode.InnerText);

                    template.Name = name + "." + tptName;

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
    /// <summary>
    /// still needs optimice
    /// </summary>
    public class SqltemplateConverter
    {
        private SqltemplateConverter()
        {

        }

        public static readonly SqltemplateConverter Instance = new SqltemplateConverter();


        public string Convert(SqlTemplate template, object parameters)
        {
            var builder = new StringBuilder();

            foreach (var item in template.Segments)
            {
                if (item.IsVarible)
                {
                    builder.Append(getParameterStringValue(item.Value, parameters));
                }
                else
                {
                    builder.Append(item.Value);
                }
            }

            return builder.ToString();
        }


        private string getParameterStringValue(string name, object obj)
        {
            var temp = obj;

            foreach (var item in name.Splite1("."))
            {
                var property = JasmineReflectionCache.Instance.GetItem(obj.GetType()).Properties.GetItemByName(item);

                if (property == null)
                    throw new ParameterNotFoundException($"parameter @{name} can not be found by given isntance {obj} ");

                temp = property.GetValue(temp);

            }

            return DefaultBaseTypeConvertor.Instance.ConvertToSqlString(temp.GetType(), temp);

        }

    }
}
