namespace Jasmine.Common
{
    /// <summary>
    /// use to build a filter pieline
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IFilterPiplelineBuilder<TContext>
        where TContext:IFilterContext
    {
        /// <summary>
        /// attach a error filter at error pipeline first
        /// </summary>
        /// <param name="filter">error filter</param>
        /// <returns></returns>
        IFilterPiplelineBuilder<TContext> AddFirst(IFilter<TContext> filter);
        /// <summary>
        /// attach a error filter at error pipeline last
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IFilterPiplelineBuilder<TContext> AddLast(IFilter<TContext> filter);

        IFilterPiplelineBuilder<TContext> AddBefore(string name, IFilter<TContext> filter);
        IFilterPiplelineBuilder<TContext> AddAfter(string name, IFilter<TContext> filter);

        IFilterPiplelineBuilder<TContext> Remove(string name);


    }
}
