using Jasmine.Orm.Attributes;
using Jasmine.Orm.Interfaces;
using Jasmine.Orm.Model;
using Jasmine.Reflection;
using Jasmine.Reflection.Interfaces;
using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm.Implements
{
    public class DefaultTableInfoCache : ITableInfoCache
    {
        private DefaultTableInfoCache()
        {

        }

        private readonly ConcurrentDictionary<Type, Table> _tables = new ConcurrentDictionary<Type, Table>();
        private readonly ITypeCache _reflection =DefaultReflectionCache.Instance;

        public static readonly ITableInfoCache Instance = new DefaultTableInfoCache();
        public void Cache(Type type)
        {
            if (_tables.ContainsKey(type))
                return;

            if (!_reflection.Contains(type))
                _reflection.Cache(type);


            var typeMataData = _reflection.GetItem(type);

            var table = new Table();
            table.RelatedType = type;

            var constructor = typeMataData.Constructors.GetDefaultConstructor();

            table.Constructor = constructor ??
                               throw new NotSupportedException("thereis no  default constructor be found,the default constructor must be provide ! ");

            _tables.TryAdd(type, table);

            foreach (var item in typeMataData.Properties)
            {
                if (item.Attributes.Contains(typeof(SqlIgnoreAttribute)))
                    continue;

                var column = new Column();

                column.ModelName = column.SqlName = item.Name;

                foreach (var attr in item.Attributes)
                {
                    if (attr is NotNullAttribute)
                    {
                        column.Nullable = false;
                    }
                    else if (AttributeUtls.Constraints.Contains(attr.GetType()))
                    {
                        column.Constraints.Add(attr.GetType(), attr);
                    }
                    else if (attr is SqlColumnNameAttributeAttribute)
                    {
                        column.SqlName = ((SqlColumnNameAttributeAttribute)attr).SqlName;
                    }
                    else if (attr is SqlDataTypeAttribute)
                    {
                        column.SqlType = ((SqlDataTypeAttribute)attr).SqlType;
                    }


                }

                column.Getter = item.Getter;
                column.Setter = item.Setter;
                column.RelatedType = item.PropertyType;

                _tables[type].Columns.Add(column.SqlName, column);
            }

            _tables[type].RelatedType = type;
            _tables[type].Constructor = typeMataData.Constructors.GetDefaultConstructor();

        }

        public bool ContainsTable(Type type)
        {
            return _tables.ContainsKey(type);
        }

        public Table GetTable(Type type)
        {
            Cache(type);
            return _tables.TryGetValue(type, out var result) ? result : null;
        }

        public void Cache<T>()
        {
            Cache(typeof(T));
        }

        public Table GetTable<T>()
        {
            return GetTable(typeof(T));
        }

        public bool ContainsTable<T>()
        {
            return ContainsTable(typeof(T));
        }
    }
}
