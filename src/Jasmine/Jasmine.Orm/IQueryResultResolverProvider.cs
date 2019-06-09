using System;

namespace Jasmine.Orm
{
    public  interface IQueryResultResolverProvider
    {
        IQueryResultResolver GetResolver(Type type);
    }
}
