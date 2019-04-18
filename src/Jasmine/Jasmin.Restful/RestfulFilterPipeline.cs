using System.Collections;
using System.Collections.Generic;
using Jasmine.Common;

namespace Jasmine.Restful
{
    public class RestfulFilterPipelineManager 
    {
        public int Count => throw new System.NotImplementedException();

        public void AddFilterPipeline(string path, IFilterPipeline<HttpFilterContext> pipeline)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<IFilterPipeline<HttpFilterContext>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public void RemoveFilterPipeline(string path)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
