﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Jasmine.Orm
{
    public class QueryResultContext
    {
        public QueryResultContext(ISqlExcuter excutor,DbDataReader reader,DbConnection connection, IDbConnectionProvider provider)
        {
            Reader = reader;
            ResultTable = QueryResultTableMetaData.Create(reader);
            ConnectionProvider = provider;
            Connection = connection;
            Excutor = excutor;
        }
        public DbConnection Connection { get;  }
        public IDbConnectionProvider ConnectionProvider { get; }
        public DbDataReader Reader { get; }
        public QueryResultTableMetaData ResultTable { get; }
        public ISqlExcuter Excutor { get; }
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
