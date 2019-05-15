using Jasmine.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestFulAopFilterProvider : IAopFilterProvider<HttpFilterContext>
    {
        private ConcurrentDictionary<string, IFilter<HttpFilterContext>> _map = new ConcurrentDictionary<string, IFilter<HttpFilterContext>>();
        public int Count => throw new NotImplementedException();

        public void AddFilter(IFilter<HttpFilterContext> filter)
        {
           if(!_map.TryAdd(filter.Name,filter))
            {

            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IFilter<HttpFilterContext>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IFilter<HttpFilterContext> GetFilter()
        {
            throw new NotImplementedException();
        }

        public IFilter<HttpFilterContext> GetFilter(string name)
        {
            throw new NotImplementedException();
        }

        public void Remove(string name)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
