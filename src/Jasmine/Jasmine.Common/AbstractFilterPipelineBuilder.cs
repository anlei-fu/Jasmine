namespace Jasmine.Common
{
    public class AbstractFilterPipelineBuilder<T> : IRequestProcessorBuilder<T>
    {

        public AbstractFilterPipelineBuilder(IRequestProcessor<T> toBuilder)
        {
            _pipeline = toBuilder;
        }

        private IRequestProcessor<T> _pipeline;

        public IRequestProcessorBuilder<T> AddErrorFirst(IFilter<T> filter)
        {
            if (_pipeline.ErrorFilter.Next == null)
                _pipeline.ErrorFilter.Next = filter;
            else
            {
                filter.Next = _pipeline.ErrorFilter.Next;
                _pipeline.ErrorFilter.Next = filter;
            }

            return this;
        }

        public IRequestProcessorBuilder<T> AddErrorLast(IFilter<T> filter)
        {
            if (_pipeline.ErrorFilter.Next == null)
                _pipeline.ErrorFilter.Next = filter;
            else
            {
                var last = _pipeline.ErrorFilter;

                while (last.Next != null)
                    last = last.Next;

                last.Next = filter;
            }

            return this;
        }

        public IRequestProcessorBuilder<T> AddFirst(IFilter<T> filter)
        {
            if (_pipeline.Filter.Next == null)
                _pipeline.Filter.Next = filter;
            else
            {
                filter.Next = _pipeline.Filter.Next;
                _pipeline.Filter.Next = filter;
            }

            return this;
        }

        public IRequestProcessorBuilder<T> AddLast(IFilter<T> filter)
        {
            if (_pipeline.Filter.Next == null)
                _pipeline.Filter.Next = filter;
            else
            {
                var last = _pipeline.Filter;

                while (last.Next != null)
                    last = last.Next;

                last.Next = filter;
            }

            return this;
        }

        public IRequestProcessor<T> Build()
        {
            var result = _pipeline;

            _pipeline = null;

            return result;
        }

        public IRequestProcessorBuilder<T> SetStat()
        {
            throw new System.NotImplementedException();
        }
    }
}
