using Jasmine.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Restful
{
    public class RestFulAopFilterProvider : IAopFilterProvider<HttpFilterContext>
    {
        private ConcurrentDictionary<string, IFilter<HttpFilterContext>> _map = new ConcurrentDictionary<string, IFilter<HttpFilterContext>>();
        public int Count => throw new NotImplementedException();

        public void AddFilter(IFilter<HttpFilterContext> filter)
        {
           if(!_map.TryAdd(filter.Name,filter))
                throw
        }

        public IEnumerator<IFilter<HttpFilterContext>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IFilter<HttpFilterContext> GetFilter()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
