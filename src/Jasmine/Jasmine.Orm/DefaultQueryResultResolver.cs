using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Implements
{
    public class DefaultQueryResultResolver : IQueryResultResolver
    {
        private DefaultQueryResultResolver()
        {
        }

        public static readonly DefaultQueryResultResolver Instance = new DefaultQueryResultResolver();


        private ITableMetaDataProvider _tableProvider => DefaultTableMetaDataProvide.Instance;
        private ISqlBaseTypeConvertor _baseTypeConvertor => DefaultBaseTypeConvertor.Instance;

        public object Resolve(QueryResultContext context, Type type)
        {
            var table = _tableProvider.GetTable(type);

            var data = table.Constructor.Invoke();

            foreach (var item in table.Columns.Values)
            {
                var column = item.ColumnName;

                if (context.ResultTable.Columns.ContainsKey(column))
                {
                    var rawValue = context.Reader.GetValue(context.ResultTable.Columns[column].Index);

                    if (rawValue.GetType() == item.RelatedType)
                    {
                        item.Setter.Invoke(data, rawValue);
                    }
                    else
                    {
                        item.Setter.Invoke(data, _baseTypeConvertor.ExpliciteConvert(rawValue, item.RelatedType));
                    }
                }
            }

            return data;

        }

       
    }
}
