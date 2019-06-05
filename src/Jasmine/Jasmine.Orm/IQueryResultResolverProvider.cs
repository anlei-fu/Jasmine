using System;

namespace Jasmine.Orm.Interfaces
{
    public  interface IQueryResultResolverProvider
    {
        IQueryResultResolver GetResolver(Type type);
        IQueryResultResolver GetResolver<T>();
    }
}
