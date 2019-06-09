using Jasmine.Extensions;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;

namespace Jasmine.Orm.Implements
{
    public class DefaultQueryResultResolver : IQueryResultResolver
    {
        private DefaultQueryResultResolver()
        {

        }
        public static readonly IQueryResultResolver Instance = new DefaultQueryResultResolver();
        private ITableMetaDataProvider _tableProvider => DefaultTableMetaDataProvider.Instance;
        private ISqlTypeConvertor _baseTypeConvertor => DefaultBaseTypeConvertor.Instance;
        private IReflectionCache<TypeMetaData,Type> _refelctionProvider => JasmineReflectionCache.Instance;


        public IEnumerable<T> Resolve<T>(QueryResultContext context)
        {
            var ls = new List<T>();

            var table = _tableProvider.GetTable(typeof(T));

            var resolveItems = createResolveItems(context, table);

            while (context.Reader.Read())
            {
                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParent(item.Parent, instanceMap,item.PropertyType);

                    var rawValue = context.Reader.GetValue(item.ColumnIndex);

                    item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));

                }

                ls.Add((T)instance);
            }

            return ls;
        }

        private void createParent(string parent, Dictionary<string, object> map,Type propertyType)
        {
            var segs = parent.Splite1("_");

            var obj = map[""];

            var temp = string.Empty;

            foreach (var item in segs)
            {
                temp += item;

                if (!map.ContainsKey(temp))
                {
                    var instance =_tableProvider.GetTable(propertyType).Constructor.Invoke();

                    var property = _refelctionProvider.GetItem(obj.GetType()).Properties.GetItemByName(item);

                    property.Setter.Invoke(obj, instance);

                    map.Add(temp, instance);
                }

                obj = map[temp];
            }
        }
        private List<ResolveItem> createResolveItems(QueryResultContext context, TableMetaData table)
        {
            var ls = new List<ResolveItem>();
            // iterate all column in result table
            foreach (var item in context.ResultTable.Columns.Values)
            {
                var segs = item.Name.Splite1("_");
                //may be self column or join table 's column
                if (segs.Count == 1)
                {
                    //self
                    if (table.Columns.ContainsKey(item.Name))
                    {
                        ls.Add(new ResolveItem()
                        {
                            Parent = "",
                            PropertyName = table.Columns[item.Name].PorpertyName,
                            Setter = table.Columns[item.Name].Setter,
                            PropertyType = table.Columns[item.Name].RelatedType,
                            ColumnIndex = item.Index
                        });
                    }
                    //search join table
                    else
                    {
                        var result = resolveJoinTable(item, table, "");

                        if (result != null)
                            ls.Add(result);

                    }
                }
                else
                {
                    //search self join columns
                    var result = resolveJoinColumn(item, table, segs.ToArray(), "");

                    if (result != null)
                    {
                        ls.Add(result);

                        continue;
                    }

                    // search join table  join column
                    result = resolveJoinTableJoinColumn(item, table, segs.ToArray(), "");

                    if (ls != null)
                        ls.Add(result);
                }
            }

            return ls;

        }

        private ResolveItem resolveJoinTableJoinColumn(QuryResultColumnInfo column, TableMetaData table, string[] segs, string prefix)
        {
            foreach (var jointable in table.JoinTables.Values)
            {
                var result = resolveJoinColumn(column, jointable.Table, segs, jointable.PropertyName);

                if (result != null)
                    return result;

                result = resolveJoinTableJoinColumn(column, jointable.Table, segs, prefix + jointable.PropertyName);

                if (result != null)
                    return result;
            }

            return null;
        }
        private ResolveItem resolveJoinTable(QuryResultColumnInfo column, TableMetaData table, string prefix)
        {
            foreach (var item in table.JoinTables.Values)
            {
                if (item.Table.Columns.ContainsKey(column.Name))
                {
                    return new ResolveItem()
                    {
                        PropertyName = item.Table.Columns[column.Name].PorpertyName,
                        ColumnIndex = column.Index,
                        PropertyType = item.Table.Columns[column.Name].RelatedType,
                        Parent = prefix + item.PropertyName,
                        Setter = item.Table.Columns[column.Name].Setter
                    };
                }

                foreach (var joinTbale in item.Table.JoinTables.Values)
                {
                    var result = resolveJoinTable(column, joinTbale.Table, prefix + item.PropertyName);

                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        private ResolveItem resolveJoinColumn(QuryResultColumnInfo column, TableMetaData table, string[] segs, string prefix)
        {


            if (segs.Length == 0)
            {
                if (table.Columns.ContainsKey(segs[0]))
                {
                    return new ResolveItem()
                    {
                        PropertyName = segs[0],
                        PropertyType = table.Columns[segs[0]].RelatedType,
                        Parent = prefix,
                        ColumnIndex = column.Index,
                        Setter = table.Columns[segs[0]].Setter
                    };
                }
            }
            else
            {
                if (table.JoinColumns.ContainsKey(segs[0]))
                {
                    var array = new string[segs.Length - 1];
                    segs.CopyTo(array, 1);

                    var result = resolveJoinColumn(column, table.JoinColumns[segs[0]].Table, array, prefix + table.JoinColumns[segs[0]].PropertyName);

                    if (result != null)
                        return result;
                }
            }

            return null;

        }

        internal class ResolveItem
        {
            public string Parent { get; set; }
            public Action<object, object> Setter { get; set; }
            public Type PropertyType { get; set; }
            public int ColumnIndex { get; set; }
            public string PropertyName { get; set; }
        }

    }


}
