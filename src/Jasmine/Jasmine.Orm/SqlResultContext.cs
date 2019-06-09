using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Jasmine.Orm
{
    public class QueryResultContext
    {
        public QueryResultContext(DbDataReader reader)
        {
            Reader = reader;
            ResultTable = QueryResultTableMetaData.Create(reader);
        }

        public DbDataReader Reader { get; }
        public QueryResultTableMetaData ResultTable { get; }

    }


    public class QueryResultTableMetaData
    {
        public static QueryResultTableMetaData Create(IDataReader reader)
        {
            var result = new QueryResultTableMetaData();

            var index = 0;

            while (index < reader.FieldCount)
            {
                var column = new QuryResultColumnInfo()
                {
                    Name = reader.GetName(index),
                    SqlType = reader.GetFieldType(index),
                    Index = index,
                };

                result.Columns.Add(column.Name, column);
                ++index;
            }

            return result;
        }
        public IDictionary<string, QuryResultColumnInfo> Columns { get; } = new Dictionary<string, QuryResultColumnInfo>();
    }

    public struct QuryResultColumnInfo
    {
        public string Name { get; set; }
        public Type SqlType { get; set; }
        public int Index { get; set; }
       
    }
}
