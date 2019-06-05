namespace Jasmine.Orm
{
    public interface ITableTemplateCacheProvider
    {
        ITableTemplateCache GetCache<T>();

    }
}
