using System;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Implements;
using Jasmine.Orm.Model;

namespace Jasmine.Orm
{
    public class JasmineOrmConfig : ITemplateProvider,ISqlConvertorProvider
    {
        public void AddConvertor(Type type, ISqlConvertor convertor)
        {
            DefaultSqlConvertorProvider.Instance.AddConvertor(type, convertor);
        }

        public void AddConvertor<T>(ISqlConvertor convertor)
        {
            DefaultSqlConvertorProvider.Instance.AddConvertor<T>(convertor);
        }

        public Template GetTemplate(string templateName)
        {
            return DefaultTemplateProvider.Instance.GetTemplate(templateName);
        }

        public ISqlConvertor GetConvertor<T>()
        {
            return DefaultSqlConvertorProvider.Instance.GetConvertor<T>();
        }

        public ISqlConvertor GetConvertor(Type type)
        {
            return DefaultSqlConvertorProvider.Instance.GetConvertor(type);
        }

        public void RemoveConvertor(Type type)
        {
             DefaultSqlConvertorProvider.Instance.RemoveConvertor(type);
        }

        public void RemoveConvertor<T>()
        {
            DefaultSqlConvertorProvider.Instance.RemoveConvertor<T>();
        }

        public void SaveTemplate(Template template)
        {
            DefaultTemplateProvider.Instance.SaveTemplate(template);
        }

        public void SaveTemplate(string template, string name)
        {
            DefaultTemplateProvider.Instance.SaveTemplate(template, name);
        }
    }
}
