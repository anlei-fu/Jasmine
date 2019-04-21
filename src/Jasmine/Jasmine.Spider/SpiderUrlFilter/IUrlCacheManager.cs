using System.Collections.Generic;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public interface IUrlCacheManager
    {
        UrlCache GetCache(long taskId);
        void RemoveCache(long taskId);
        List<UrlCache> GetAllCaches();

    }
}
