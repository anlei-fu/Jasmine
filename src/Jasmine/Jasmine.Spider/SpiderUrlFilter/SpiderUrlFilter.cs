using System;
using System.Collections.Generic;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public class SpiderUrlFilter : ISpiderUrlFilter
    {
        private IUrlCacheManager _cacheManager;
        private int _batchSize;
        public bool CreateNewTask(long taskId)
        {
            throw new NotImplementedException();
        }

        public bool Filt(long taskId, List<string> newUrls)
        {
            var cache = _cacheManager.GetCache(taskId);

            if (cache == null)
                return false;
            else
            {
                cache.Cache(newUrls);

                return true;
            }
        }

        public List<UrlFilterStatics> GetAllTaskStatics()
        {
            var ls = new List<UrlFilterStatics>();
            var caches = _cacheManager.GetAllCaches();

            foreach (var item in caches)
                ls.Add(item.Statics);

            return ls;


        }

        public List<string> GetNewUrl(long taskId)
        {
            var cache = _cacheManager.GetCache(taskId);

            return cache == null ? null : cache.GetUrls(_batchSize);

        }

        public UrlFilterStatics GetStatics(long taskId)
        {
            var cache = _cacheManager.GetCache(taskId);

            return cache == null ? null : cache.Statics;
        }

        public bool RestartTask(long taskId)
        {
            throw new NotImplementedException();
        }

        public bool StopTask(long taskId)
        {
            _cacheManager.RemoveCache(taskId);

            return true;
        }
    }
}
