namespace Jasmine.Common
{
    public class FilterPipelineBuilder<TContext> : IFilterPipelineBuilder<TContext>
    {
        private FilterPipeline<TContext> _pipeline;
     
        public IFilterPipelineBuilder<TContext> AddErrorFirst(IFilter<TContext> filter)
        {

            if (_pipeline.Error == null)
            {
                _pipeline.Error = filter;
            }
            else
            {
                filter.Next = _pipeline.Error;
                _pipeline.Error = filter;
            }

            return this;
          
        }

        public IFilterPipelineBuilder<TContext> AddErrorLast(IFilter<TContext> filter)
        {
            var temp = _pipeline.Error;

            if (temp == null)
                _pipeline.Error = filter;
            else
            {
                while(temp.Next!=null)
                {
                    temp = temp.Next;
                }

                temp.Next = filter;
            }

            return this;
        }

        public IFilterPipelineBuilder<TContext> AddFirst(IFilter<TContext> filter)
        {
            if (_pipeline.Root == null)
            {
                _pipeline.Root = filter;
            }
            else
            {
                filter.Next = _pipeline.Root;
                _pipeline.Error = filter;
            }

            return this;
        }

        public IFilterPipelineBuilder<TContext> AddLast(IFilter<TContext> filter)
        {
            var temp = _pipeline.Root;

            if (temp == null)
                _pipeline.Root= filter;
            else
            {
                while (temp.Next != null)
                {
                    temp = temp.Next;
                }

                temp.Next = filter;
            }

            return this;
        }

        public IFilterPipeline<TContext> Build()
        {
            return _pipeline;
        }

        public IFilterPipelineBuilder<TContext> SetStat()
        {
            throw new System.NotImplementedException();
        }
    }
}
