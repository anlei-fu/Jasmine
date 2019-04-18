namespace Jasmine.Ioc
{
    public interface IInstanceCreator
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
        object Create(IocServiceMetaData metaData,DependencyCheckNode node);
        /// <summary>
        /// add a listener 
        /// <see cref="IInstanceCreatedListener"/>
        /// </summary>
        /// <param name="listener"></param>
        void AddListener(IInstanceCreatedListener listener);
        /// <summary>
        /// remove a listener
        /// </summary>
        /// <param name="name"></param>
        void RemoveListener(string name);
    }
}
