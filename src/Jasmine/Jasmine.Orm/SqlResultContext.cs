using System;
using System.Collections.Concurrent;
using System.Data.Common;

namespace Jasmine.Orm.Model
{
    public class SqlResultContext
    {
        public SqlResultContext(DbDataReader reader)
        {
            Reader = reader;
            TempTable = QueryResultTableMetaData.Create(reader);
        }
        public DbDataReader Reader { get; }

        public QueryResultTableMetaData TempTable { get; }

    }


    public class QueryResultTableMetaData
    {
        public static QueryResultTableMetaData Create(DbDataReader reader)
        {
            var result = new QueryResultTableMetaData();

            var t = 0;

            while (t++ < reader.FieldCount)
            {
                var column = new QuryResultColumnMetaInfo()
                {
                    Name = reader.GetName(t - 1),
                    SqlType = reader.GetProviderSpecificFieldType(t - 1),
                    Index = t - 1
                };

                result.Columns.TryAdd(column.Name, column);
            }

            return result;
        }
        public ConcurrentDictionary<string, QuryResultColumnMetaInfo> Columns { get; } = new ConcurrentDictionary<string, QuryResultColumnMetaInfo>();
    }

    public class QuryResultColumnMetaInfo
    {
        public string Name { get; set; }
        public Type SqlType { get; set; }
        public int Index { get; set; }
    }
}
