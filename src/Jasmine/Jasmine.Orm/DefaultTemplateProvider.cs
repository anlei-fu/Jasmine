using System;
using System.Collections.Concurrent;

namespace Jasmine.Orm
{
    public class DefaultTableTemplateCacheProvider : ITableTemplateCacheProvider
    {
        private DefaultTableTemplateCacheProvider()
        {

        }

        public static readonly ITableTemplateCacheProvider Instance = new DefaultTableTemplateCacheProvider();

        private ConcurrentDictionary<Type, ITableTemplateCache> _map = new ConcurrentDictionary<Type, ITableTemplateCache>();
        public ITableTemplateCache GetCache<T>()
        {
            return GetCache(typeof(T));
        }

        public ITableTemplateCache GetCache(Type type)
        {
            if(!_map.ContainsKey(type))
            {
                _map.TryAdd(type, new DefaultTemplateCache(type));
            }

            return _map[type];
        }
    }
}
