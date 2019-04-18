using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;
using System.Text;

namespace Jasmine.Orm.Implements
{
    public class DefaultSqlConvertor : ISqlConvertor
    {
        private DefaultSqlConvertor()
        {
            _tableCache = DefaultTableInfoCache.Instance;
            _baseTypeConvertor = DefaultBaseTypeConvertor.Instance;
        }

        public static readonly DefaultSqlConvertor Instance = new DefaultSqlConvertor();

        private const string _coma = ",";
        private ITableInfoCache _tableCache;
        private ISqlBaseTypeConvertor _baseTypeConvertor;
        public object FromResult(SqlResultContext context, Type type)
        {
            var table = _tableCache.GetTable(type);
            var data = table.Constructor.Invoke();

            foreach (var item in table.Columns)
            {
                if (context.Columns.ContainsKey(item.Value.SqlName))
                    item.Value.Setter.Invoke(data, _baseTypeConvertor.FromSql(context.Reader, context.Columns[item.Value.SqlName].Index, item.Value.RelatedType));
            }

            return data;

        }

        public string ToSql(object data)
        {
            var sb = new StringBuilder();

            foreach (var item in _tableCache.GetTable(data.GetType()).Columns.Values)
            {
                sb.Append(_baseTypeConvertor.ToSQL(item.RelatedType, item.Getter.Invoke(data), item.Nullable))
                  .Append(_coma);
            }

            return sb.Remove(sb.Length - 1, 1).ToString();

        }
    }
}
