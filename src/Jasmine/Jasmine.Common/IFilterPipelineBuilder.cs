namespace Jasmine.Common
{
    /// <summary>
    /// use to build a filter pieline
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
   public interface IRequestProcessorBuilder<TContext>
    {
        /// <summary>
        /// attach a error filter at error pipeline first
        /// </summary>
        /// <param name="filter">error filter</param>
        /// <returns></returns>
        IRequestProcessorBuilder<TContext> AddErrorFirst(IFilter<TContext> filter);
        /// <summary>
        /// attach a error filter at error pipeline last
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IRequestProcessorBuilder<TContext> AddErrorLast(IFilter<TContext> filter);
        /// <summary>
        /// attach a  filter at  pipeline first
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IRequestProcessorBuilder<TContext> AddFirst(IFilter<TContext> filter);
        /// <summary>
        /// attach a  filter at error pipeline last
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IRequestProcessorBuilder<TContext> AddLast(IFilter<TContext> filter);
        /// <summary>
        /// set metrict
        /// </summary>
        /// <returns></returns>
        IRequestProcessorBuilder<TContext> SetStat();
        /// <summary>
        /// finish build and return
        /// </summary>
        /// <returns></returns>
        IRequestProcessor<TContext> Build();

    }
}
