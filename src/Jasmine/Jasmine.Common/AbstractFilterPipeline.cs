using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractFilterPipeline<T> : IFilterPipeline<T>,IFilterPipelineBuilder<T>
    {
        

        public IFilter<T> ErrorFilter => throw new System.NotImplementedException();

        public IFilter<T> Root => throw new System.NotImplementedException();

        public IStat<IStatItem> Stat => throw new System.NotImplementedException();

        public int Count => throw new System.NotImplementedException();

        public string Path => throw new System.NotImplementedException();

        public System.Collections.Generic.IEnumerator<IFilter<T>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

       
    }
}
