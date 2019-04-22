using System.Collections.Generic;

namespace Jasmine.Orm
{
    public  interface ITemplateConvertor
    {
        string Convert(string template, object parameter);
        string Convert(string template, IDictionary<string, object> parameter);
        string Convert(string template, IEnumerable<IDictionary<string, object>> parameters);
        string Convert(string template, IEnumerable<object> parameters);
        string Convert(SqlTemplate template, SqlParameters parameters);
      
    }
}
