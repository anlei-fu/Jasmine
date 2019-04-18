using System.Collections.Generic;
using System.Text;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Model;
using Jasmine.Reflection;

namespace Jasmine.Orm.Implements
{
    public class DefaultTemplateConvertor : ITemplateConvertor
    {
        private DefaultTemplateConvertor()
        {

        }

        public static readonly ITemplateConvertor Instance = new DefaultTemplateConvertor();

        public string Convert(string template, object parameter)
        {
            var type = parameter.GetType();

            if (!DefaultReflectionCache.Instance.Contains(type))
                DefaultReflectionCache.Instance.Cache(type);

            var dictionary = new Dictionary<string, object>();

            foreach (var item in DefaultReflectionCache.Instance.GetItem(type).Properties)
            {
                dictionary.Add(item.Name, item.GetValue(parameter));
            }

            return Convert(template, dictionary);
                
        }

        public string Convert(string template, IDictionary<string, object> parameter)
        {
            return Convert(DefaultTemplateParser.Instance.Parse(template), parameter);
        }

        public string Convert(string template, IEnumerable<IDictionary<string, object>> parameters)
        {
            return Convert(DefaultTemplateParser.Instance.Parse(template), parameters);
        }

        public string Convert(string template, IEnumerable<object> parameters)
        {
            return Convert(DefaultTemplateParser.Instance.Parse(template), parameters);
        }

        public string Convert(IEnumerable<TemplateSegment> segments, object parameter)
        {
            var type = parameter.GetType();

            if (!DefaultReflectionCache.Instance.Contains(type))
                DefaultReflectionCache.Instance.Cache(type);

            var dictionary = new Dictionary<string, object>();

            foreach (var item in DefaultReflectionCache.Instance.GetItem(type).Properties)
            {
                dictionary.Add(item.Name, item.GetValue(parameter));
            }

            return Convert(segments, dictionary);

        }

        public string Convert(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters)
        {
            var sb = new StringBuilder();

            foreach (var item in parameters)
            {
                sb.Append(Convert(segments,item)+" ");
            }

            return sb.ToString();
        }

        public string Convert(IEnumerable<TemplateSegment> segments, IEnumerable<object> parameters)
        {

            var sb = new StringBuilder();

            foreach (var item in parameters)
            {
                sb.Append(Convert(segments, item) + " ");
            }

            return sb.ToString();
        }
        public string Convert(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameters)
        {
            var sb = new StringBuilder();

            foreach (var item in segments)
            {
                if (item.IsParamer)
                {
                    if (parameters.ContainsKey(item.Value))
                    {
                        if (item.IsStringParameter)
                        {
                            sb.Append(DefaultBaseTypeConvertor.Instance.ToSQL(parameters[item.Value].GetType(), parameters[item.Value]));
                        }
                        else
                        {
                            sb.Append(parameters[item.Value].ToString());
                        }


                    }
                    else
                    {
                        throw new ParameterNotFoundException(item.Value);
                    }
                }
                else
                {
                    sb.Append(item.Value);
                }
            }

            return sb.ToString();
        }
    }
}
