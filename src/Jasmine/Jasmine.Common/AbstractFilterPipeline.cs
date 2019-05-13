using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class AbstractFilterPipeline<T> : IFilterPipeline<T>,IFilterPipelineBuilder<T>
    {
        

        public IFilter<T> Error => throw new System.NotImplementedException();

        public IFilter<T> Root => throw new System.NotImplementedException();

        public IStat<IStatItem> Stat => throw new System.NotImplementedException();

        public int Count => throw new System.NotImplementedException();

        public string Path => throw new System.NotImplementedException();

        public IFilterPipelineBuilder<T> AddErrorFirst(IFilter<T> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<T> AddErrorLast(IFilter<T> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<T> AddFirst(IFilter<T> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<T> AddLast(IFilter<T> filter)
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipeline<T> Build()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerator<IFilter<T>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IFilterPipelineBuilder<T> SetStat()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

       
    }
}
