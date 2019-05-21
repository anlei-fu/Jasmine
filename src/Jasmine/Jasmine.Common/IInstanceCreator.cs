using System.Collections.Generic;

namespace Jasmine.Common
{
    public interface IInstanceCreator<TMetaData>:IReadOnlyCollection<IInstanceCreatedListener<TMetaData>>
    {
        /// <summary>
        /// create a instance by <see cref="IocServiceMetaData"/>
        /// </summary>
        /// <param name="metaData"></param>
        /// <param name="node"></param>
        /// <returns>
        /// if sucessed
        ///    return reqired instance
        ///    
        /// throw <see cref="dependencyloopexception"/>
        ///    
        /// </returns>
        object Create(TMetaData metaData,DependencyCheckNode node);
        /// <summary>
        /// add a listener 
        /// <see cref="IInstanceCreatedListener"/>
        /// </summary>
        /// <param name="listener"></param>
        void AddListener(IInstanceCreatedListener<TMetaData> listener);
        /// <summary>
        /// remove a listener
        /// </summary>
        /// <param name="name"></param>
        void RemoveListener(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ContainsListener(string name);
    }
}
