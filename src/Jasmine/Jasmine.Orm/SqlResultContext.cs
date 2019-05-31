using System;
using System.Collections.Concurrent;
using System.Data;

namespace Jasmine.Orm.Model
{
    public class QueryResultContext
    {
        public QueryResultContext(IDataReader reader)
        {
            Reader = reader;
            ResultTable = QueryResultTableMetaData.Create(reader);
        }
        public IDataReader Reader { get; }

        public QueryResultTableMetaData ResultTable { get; }

    }


    public class QueryResultTableMetaData
    {
        public static QueryResultTableMetaData Create(IDataReader reader)
        {
            var result = new QueryResultTableMetaData();

            var t = 0;

            while (t++ < reader.FieldCount)
            {
                var column = new QuryResultColumnInfo()
                {
                    Name = reader.GetName(t - 1),
                    SqlType = reader.GetFieldType(t - 1),
                    Index = t - 1
                };

                result.Columns.TryAdd(column.Name, column);
            }

            return result;
        }
        public ConcurrentDictionary<string, QuryResultColumnInfo> Columns { get; } = new ConcurrentDictionary<string, QuryResultColumnInfo>();
    }

    public class QuryResultColumnInfo
    {
        public string Name { get; set; }
        public Type SqlType { get; set; }
        public int Index { get; set; }
    }
}
