using Jasmine.Common;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class SqlTemplate : INameFearture
    {
        public string Name { get; set; }
        public SqlTemplateSegment[] Segments { get; set; }
    }

    public struct SqlTemplateSegment
    {
        public string[] Value  { get; }
        public bool IsVarible { get; }
    }

    public class SqlTemplateParser
    {
        public SqlTemplate Parse(string input)
        {
            return null;
        }
    }

    public  class SqlTemplateProvider:IReadOnlyCollection<SqlTemplate>
    {
        private ConcurrentDictionary<string, SqlTemplate> _map = new ConcurrentDictionary<string, SqlTemplate>();
        public int Count => _map.Count;


        public void Load(string xmlPath)
        {

        }
        public SqlTemplate GetTemplate(string name)
        {
            return _map.TryGetValue(name, out var result) ? result : null;
        }

        
        public void Add(SqlTemplate template)
        {
           if(!_map.TryAdd(template.Name, template))
            {
                //should throw something
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
        public string Convert(SqlTemplate template,object parameters)
        {
            return null;
        }
    }
}
