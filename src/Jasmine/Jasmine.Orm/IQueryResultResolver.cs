using System.Collections.Generic;

namespace Jasmine.Orm
{ 
    public interface IQueryResultResolver
    {
        /// <summary>
        /// resolve sql query result collection  into  a <see cref="IEnumerable{T}"/> by given <see cref="QueryResultContext"/> 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<T> Resolve<T>(QueryResultContext context);
    }
}
