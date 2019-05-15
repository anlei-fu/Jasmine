namespace Jasmine.Common
{
    public class FilterPipelineBuilder<TContext> : IRequestProcessorBuilder<TContext>
    {
        private FilterPipeline<TContext> _pipeline;
     
        public IRequestProcessorBuilder<TContext> AddErrorFirst(IFilter<TContext> filter)
        {

            if (_pipeline.ErrorFilter == null)
            {
                _pipeline.ErrorFilter = filter;
            }
            else
            {
                filter.Next = _pipeline.ErrorFilter;
                _pipeline.ErrorFilter = filter;
            }

            return this;
          
        }

        public IRequestProcessorBuilder<TContext> AddErrorLast(IFilter<TContext> filter)
        {
            var temp = _pipeline.ErrorFilter;

            if (temp == null)
                _pipeline.ErrorFilter = filter;
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

        public IRequestProcessorBuilder<TContext> AddFirst(IFilter<TContext> filter)
        {
            if (_pipeline.Filter == null)
            {
                _pipeline.Filter = filter;
            }
            else
            {
                filter.Next = _pipeline.Filter;
                _pipeline.ErrorFilter = filter;
            }

            return this;
        }

        public IRequestProcessorBuilder<TContext> AddLast(IFilter<TContext> filter)
        {
            var temp = _pipeline.Filter;

            if (temp == null)
                _pipeline.Filter= filter;
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

        public IRequestProcessor<TContext> Build()
        {
            return _pipeline;
        }

        public IRequestProcessorBuilder<TContext> SetStat()
        {
            throw new System.NotImplementedException();
        }
    }
}
