using System;

namespace Jasmine.Orm
{
    public interface ITableTemplateCacheProvider
    {
        ITableTemplateCache GetCache<T>();
        ITableTemplateCache GetCache(Type type);

    }
}
