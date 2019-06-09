using System;

namespace Jasmine.Orm.Attributes
{
    public class QueryResultResolverAttribute:Attribute
    {
        public QueryResultResolverAttribute(Type resolverType)
        {
            ResolverType = resolverType;
        }

        public Type ResolverType { get; }
    }
}
