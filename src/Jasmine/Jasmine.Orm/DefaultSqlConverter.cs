using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;
using System.Text;

namespace Jasmine.Orm.Implements
{
    public class DefaultSqlConvertor : IOrmConvertor
    {
        private DefaultSqlConvertor()
        {
        }

        public static readonly DefaultSqlConvertor Instance = new DefaultSqlConvertor();

        private const string COMA = ",";

        private ITableMetaDataProvider _tableProvider => DefaultTableMetaDataProvide.Instance;
        private ISqlBaseTypeConvertor _baseTypeConvertor => DefaultBaseTypeConvertor.Instance;

        public object FromResult(SqlResultContext context, Type type)
        {
            var table = _tableProvider.GetTable(type);

            var data = table.Constructor.Invoke();

            foreach (var item in table.Columns)
            {
                var column = item.Value.ColumnName;

                if (context.TempTable.Columns.ContainsKey(column))
                    item.Value.Setter.Invoke(data, _baseTypeConvertor.FromSql(context.Reader, context.TempTable.Columns[column].Index, item.Value.RelatedType));
            }

            return data;

        }

        public string ToSql(object data)
        {
            var sb = new StringBuilder();

            foreach (var item in _tableProvider.GetTable(data.GetType()).Columns.Values)
            {
                sb.Append(_baseTypeConvertor.ToSQL(item.RelatedType, item.Getter.Invoke(data), item.Nullable))
                  .Append(COMA);
            }


            if (sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 1, 1);


            return sb.ToString();

        }
    }
}
