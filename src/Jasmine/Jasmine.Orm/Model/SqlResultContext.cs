using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace Jasmine.Orm.Model
{
    public   class SqlResultContext
    {
        public SqlResultContext(SqlDataReader reader)
        {
            Reader = reader;
            var t = 0;

            while (t++<reader.FieldCount)
            {
                var column = new ColumnMetaInfo()
                {
                    Name = reader.GetName(t-1),
                    SqlType = reader.GetProviderSpecificFieldType(t-1),
                    Index = t-1
                };

                Columns.TryAdd(column.Name, column);
            }
        }
      public SqlDataReader Reader { get;}
      public ConcurrentDictionary<string, ColumnMetaInfo> Columns { get;} = new ConcurrentDictionary<string, ColumnMetaInfo>();
    }

    public class ColumnMetaInfo
    {
        public string Name { get; set; }
        public Type SqlType { get; set; }
        public int Index { get; set; }
    }
}
