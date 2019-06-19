using Jasmine.Extensions;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Interfaces;
using Jasmine.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Jasmine.Orm.Implements
{
    public class DefaultCursor : ICursor
    {
        internal DefaultCursor(QueryResultContext context)
        {
            _context = context;
            

        }

        private readonly object _locker = new object();

        private ISqlExcuter _excutor => _context.Excutor;
        private DbConnection _connection => _context.Connection;
        private QueryResultContext _context;
        private IDbConnectionProvider _provider => _context.ConnectionProvider;
        private ITableMetaDataProvider _tableProvider => DefaultTableMetaDataProvider.Instance;
        private ITypeCache _reflectionProvider => JasmineReflectionCache.Instance;
        private ISqlTypeConvertor _baseTypeConvertor => DefaultBaseTypeConvertor.Instance;

        public bool Closed { get; private set; }

        public virtual void Close()
        {
            lock (_locker)
            {
                if (!Closed)
                {
                    Closed = true;
                    _context.Reader.Close();
                    _provider.Recycle(_connection);
                }
            }
        }

        public T ReadOne<T>(bool withAssociate = false)
        {
            return ReadOne(typeof(T), withAssociate);
        }

        public async Task<T> ReadOneAsync<T>(bool doAssociateQuery = false)
        {
            return await ReadOneAsync(typeof(T), doAssociateQuery);
        }

        public IEnumerable<T> Read<T>(int count, bool doAssociateQuery = false)
        {
            var ls = new List<T>();

            var table = _tableProvider.GetTable(typeof(T));

            var resolveItems = createResolveItems(_context, table);

            var t = 0;

            while (t++ < count && _context.Reader.Read())
            {
                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }

                ls.Add((T)instance);
            }

            return ls;
        }

        public async Task<IEnumerable<T>> ReadAsync<T>(int count, bool doAssociateQuery = false)
        {
            var ls = new List<T>();

            var table = _tableProvider.GetTable(typeof(T));

            var resolveItems = createResolveItems(_context, table);

            var t = 0;

            while (t++<count&&await _context.Reader.ReadAsync())
            {
                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }

                ls.Add((T)instance);
            }

            return ls;
        }

        public IEnumerable<T> ReadToEnd<T>(bool doAssociateQuery = false)
        {
            var ls = new List<T>();

            var table = _tableProvider.GetTable(typeof(T));

            var resolveItems = createResolveItems(_context, table);

            while (_context.Reader.Read())
            {
                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }

                ls.Add((T)instance);
            }

            return ls;
        }

        public async Task<IEnumerable<T>> ReadToEndAsync<T>(bool doAssocaiteQuery = false)
        {
            var ls = new List<T>();

            var table = _tableProvider.GetTable(typeof(T));

            var resolveItems = createResolveItems(_context, table);

            while (await _context.Reader.ReadAsync())
            {
                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }

                ls.Add((T)instance);
            }

            return ls;
        }

        public void Dispose()
        {
            Close();
        }

        /// <summary>
        ///  spend  much time  when  cast   List[dynamic] to List[T],so  use verbose code to instead call this method
        /// </summary>
        /// <param name="type"></param>
        /// <param name="doAssociateQuery"></param>
        /// <returns></returns>
        public dynamic ReadOne(Type type, bool doAssociateQuery = false)
        {
            if (_context.Reader.Read())
            {
                var table = _tableProvider.GetTable(type);

                var resolveItems = createResolveItems(_context, table);

                var instance = resolveOneRow(resolveItems, table);

                if (doAssociateQuery)
                {
                }

                return instance;

            }
            else
            {
                return null;
            }
        }

        public async Task<dynamic> ReadOneAsync(Type type, bool doAssociateQuery = false)
        {
            if (await _context.Reader.ReadAsync())
            {
                var table = _tableProvider.GetTable(type);

                var resolveItems = createResolveItems(_context, table);

                var instance = resolveOneRow(resolveItems, table);

                if (doAssociateQuery)
                {
                }

                return instance;

            }
            else
            {
                return null;
            }
        }

        public IEnumerable<dynamic> Read(Type type, int count, bool doAssociateQuery = false)
        {
            var ls = new List<dynamic>();

            var table = _tableProvider.GetTable(type);

            var resolveItems = createResolveItems(_context, table);

            var t = 0;

            while (_context.Reader.Read() && t++ < count)
            {
                if (_context.Reader.Read())
                {
                    var instance = resolveOneRow(resolveItems, table);

                    if (doAssociateQuery)
                    {
                    }

                    ls.Add(instance);
                }
            }


            return ls;
        }

        public async Task<IEnumerable<dynamic>> ReadAsync(Type type, int count, bool doAssociateQuery = false)
        {
            var ls = new List<dynamic>();

            var table = _tableProvider.GetTable(type);

            var resolveItems = createResolveItems(_context, table);

            var t = 0;

            while (await _context.Reader.ReadAsync() && t++ < count)
            {
                if (_context.Reader.Read())
                {
                    var instanceMap = new Dictionary<string, object>();

                    var instance = table.Constructor.Invoke();

                    instanceMap.Add("", instance);

                    foreach (var item in resolveItems)
                    {
                        if (!instanceMap.ContainsKey(item.Parent))
                            createParentInstance(item.Parent, instanceMap);

                        var result = _context.Reader.IsDBNull(item.ColumnIndex);

                        if (!result)
                        {
                            var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                            item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                        }
                    }


                    if (doAssociateQuery)
                    {
                    }

                    ls.Add(instance);
                }
            }


            return ls;
        }

        public IEnumerable<dynamic> ReadToEnd(Type type, bool doAssociateQuery = false)
        {
            var ls = new List<dynamic>();

            var table = _tableProvider.GetTable(type);

            var resolveItems = createResolveItems(_context, table);


            while (_context.Reader.Read())
            {

                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }


                if (doAssociateQuery)
                {
                }

                ls.Add(instance);
            }


            return ls;
        }

        public async Task<IEnumerable<dynamic>> ReadToEndAsync(Type type, bool doAssociateQuery = false)
        {
            var ls = new List<dynamic>();

            var table = _tableProvider.GetTable(type);

            var resolveItems = createResolveItems(_context, table);

            while (await _context.Reader.ReadAsync())
            {

                var instanceMap = new Dictionary<string, object>();

                var instance = table.Constructor.Invoke();

                instanceMap.Add("", instance);

                foreach (var item in resolveItems)
                {
                    if (!instanceMap.ContainsKey(item.Parent))
                        createParentInstance(item.Parent, instanceMap);

                    var result = _context.Reader.IsDBNull(item.ColumnIndex);

                    if (!result)
                    {
                        var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                        item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                    }
                }


                if (doAssociateQuery)
                {
                }

                ls.Add(instance);
            }

            return ls;
        }
        /// <summary>
        /// can not be inline ,call this method ,if loop count is big ,will get the speed slower
        /// </summary>
        /// <param name="resolveItems"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private dynamic resolveOneRow(List<ResolveItem> resolveItems, TableMetaData table)
        {

            var instanceMap = new Dictionary<string, object>();

            var instance = table.Constructor.Invoke();

            instanceMap.Add("", instance);

            foreach (var item in resolveItems)
            {
                if (!instanceMap.ContainsKey(item.Parent))
                    createParentInstance(item.Parent, instanceMap);

                var result = _context.Reader.IsDBNull(item.ColumnIndex);

                if (!result)
                {
                    var rawValue = _context.Reader.GetValue(item.ColumnIndex);

                    item.Setter.Invoke(instanceMap[item.Parent], _baseTypeConvertor.FromSqlFiledValue(rawValue, item.PropertyType));
                }
            }

            return instance;
        }

        private void createParentInstance(string parent, Dictionary<string, object> map)
        {
            var segs = parent.Splite1("_");

            var obj = map[""];

            var temp = string.Empty;

            foreach (var item in segs)
            {
                temp = temp == string.Empty ? item : temp + "_" + item;

                if (!map.ContainsKey(temp))
                {
                    var type = _reflectionProvider.GetItem(obj.GetType()).Properties.GetItemByName(item)?.PropertyType;

                    if (type == null)
                    {
                        throw new PropertyNotExistsException("");
                    }

                    var instance = _reflectionProvider.GetItem(type).Constructors.GetDefaultConstructor()?.DefaultInvoker?.Invoke();

                    if (instance == null)
                    {
                        throw new InstanceCanNotBeCreatedException($"{type} can not be created ,require no parameter constructor!");
                    }


                    var property = _reflectionProvider.GetItem(obj.GetType()).Properties.GetItemByName(item);

                    property.Setter.Invoke(obj, instance);

                    map.Add(temp, instance);
                }

                obj = map[temp];
            }
        }
        private bool checkCanDoAssociateQuery(AssociateTable table)
        {
            return true;
        }

        private string makeAssociateQuerySql(AssociateTable table)
        {
            return string.Empty;
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

                // iterate recurserve
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


            if (segs.Length == 1)
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

                    Array.Copy(segs, 1, array, 0, array.Length);

                    prefix = prefix == string.Empty ? table.JoinColumns[segs[0]].PropertyName
                                               : prefix + "_" + table.JoinColumns[segs[0]].PropertyName;

                    var result = resolveJoinColumn(column, table.JoinColumns[segs[0]].Table, array, prefix);

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
