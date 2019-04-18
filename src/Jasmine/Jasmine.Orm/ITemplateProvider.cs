using Jasmine.Orm.Model;

namespace Jasmine.Orm.Interfaces
{
    public  interface ITemplateProvider
    {
        Template GetTemplate(string templateName);
        void SaveTemplate(Template template);
        void SaveTemplate(string template, string name);
    }
}
