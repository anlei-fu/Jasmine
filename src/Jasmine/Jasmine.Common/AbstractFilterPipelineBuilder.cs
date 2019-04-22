namespace Jasmine.Common
{
    public class AbstractFilterPipelineBuilder<T> : IFilterPipelineBuilder<T>
    {

        public AbstractFilterPipelineBuilder(IFilterPipeline<T> toBuilder)
        {
            _pipeline = toBuilder;
        }

        private IFilterPipeline<T> _pipeline;

        public IFilterPipelineBuilder<T> AddErrorFirst(IFilter<T> filter)
        {
            if (_pipeline.Error.Next == null)
                _pipeline.Error.Next = filter;
            else
            {
                filter.Next = _pipeline.Error.Next;
                _pipeline.Error.Next = filter;
            }

            return this;
        }

        public IFilterPipelineBuilder<T> AddErrorLast(IFilter<T> filter)
        {
            if (_pipeline.Error.Next == null)
                _pipeline.Error.Next = filter;
            else
            {
                var last = _pipeline.Error;

                while (last.Next != null)
                    last = last.Next;

                last.Next = filter;
            }

            return this;
        }

        public IFilterPipelineBuilder<T> AddFirst(IFilter<T> filter)
        {
            if (_pipeline.Root.Next == null)
                _pipeline.Root.Next = filter;
            else
            {
                filter.Next = _pipeline.Root.Next;
                _pipeline.Root.Next = filter;
            }

            return this;
        }

        public IFilterPipelineBuilder<T> AddLast(IFilter<T> filter)
        {
            if (_pipeline.Root.Next == null)
                _pipeline.Root.Next = filter;
            else
            {
                var last = _pipeline.Root;

                while (last.Next != null)
                    last = last.Next;

                last.Next = filter;
            }

            return this;
        }

        public IFilterPipeline<T> Build()
        {
            var result = _pipeline;

            _pipeline = null;

            return result;
        }
    }
}
