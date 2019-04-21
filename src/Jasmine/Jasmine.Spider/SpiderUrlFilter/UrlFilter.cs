using System;

namespace Jasmine.Spider.SpiderUrlFilter
{
    public class UrlFilter : IUrlFilter
    {
        private BloomFilter<string> _bloomFilter;
        public bool Filt(string url)
        {
           return   _bloomFilter.Add(url);
        }
    }
}
