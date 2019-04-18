using System.Collections.Generic;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;

namespace Jasmine.Orm.Implements
{
    public class DefaultUnkowTypeConvertor : IUnknowTypeConvertor
    {

        private DefaultUnkowTypeConvertor()
        {

        }
        public static readonly IUnknowTypeConvertor Instance = new DefaultUnkowTypeConvertor();
        public IEnumerable<object> Convert(SqlResultContext context)
        {
            var result = new List<object>();

            foreach (var item in context.Columns.Values)
            {
                result.Add(DefaultBaseTypeConvertor.Instance.FromSql(context.Reader, item.Index, SqlTypeCovertor.ConvertSqlDataTypeToCsharpType(item.SqlType)));
            }

            return result;
        }
    }
}
