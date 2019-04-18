using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Model;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public class DefaultTemplateProvider : ITemplateProvider
    {
        private DefaultTemplateProvider()
        {

        }
        public static ITemplateProvider Instance = new DefaultTemplateProvider();
        public ConcurrentDictionary<string, Template> _templates = new ConcurrentDictionary<string, Template>();
        public Template GetTemplate(string templateName)
        {
            return _templates.TryGetValue(templateName, out var result) ? result
                                                                      : throw new TemplateException($" template ({templateName}) is not found! ");     
        }

        public void SaveTemplate(Template template)
        {
            if (!_templates.TryAdd(template.Name, template))
                throw new TemplateException($" template(template) already exists!");
        }

        public void SaveTemplate(string template, string name)
        {
            if (string.IsNullOrEmpty(template) || string.IsNullOrEmpty(name))
                throw new ArgumentNullException($" parametr {nameof(template)} or {nameof(name)} is null!");

            var temp = new Template();
            temp.Segments = DefaultTemplateParser.Instance.Parse(template);
            temp.Name = name;

            SaveTemplate(temp);
        }
    }
}
