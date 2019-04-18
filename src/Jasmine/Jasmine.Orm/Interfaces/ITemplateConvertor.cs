using Jasmine.Orm.Model;
using System.Collections.Generic;

namespace Jasmine.Orm.Interfaces
{
    public  interface ITemplateConvertor
    {
        string Convert(string template, object parameter);
        string Convert(string template, IDictionary<string, object> parameter);
        string Convert(string template, IEnumerable<IDictionary<string, object>> parameters);
        string Convert(string template, IEnumerable<object> parameters);
        string Convert(IEnumerable<TemplateSegment> segments, object parameter);
        string Convert(IEnumerable<TemplateSegment> segments, IDictionary<string, object> parameter);
        string Convert(IEnumerable<TemplateSegment> segments, IEnumerable<IDictionary<string, object>> parameters);
        string Convert(IEnumerable<TemplateSegment> segments, IEnumerable<object> parameters);
    }
}
