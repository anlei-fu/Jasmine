using Jasmine.Extensions;
using Jasmine.Orm.Exceptions;
using Jasmine.Orm.Interfaces;
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
            return GetCursor(context).ReadToEnd<T>();
        }
        public ICursor GetCursor(QueryResultContext context)
        {
            return new DefaultCursor(context);
        }
     
     

    }


}
