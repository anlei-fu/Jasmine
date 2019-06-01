using Jasmine.Common;
using Jasmine.Orm.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Orm
{
    public class TableMetaData:IReadOnlyCollection<ColumnMetaData>,ITypeFearture
    {
        public DataSourceType DataSource { get; set; }
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public IDictionary<string, ColumnMetaData> Columns { get; set; } = new Dictionary<string, ColumnMetaData>();
        public Func<object> Constructor { get; set; }
        public int Count =>Columns.Count;


        public IEnumerator<ColumnMetaData> GetEnumerator()
        {
            foreach (var item in Columns.Values)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Columns.Values)
            {
                yield return item;
            }
        }
    }
}
