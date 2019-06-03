using System;
namespace Jasmine.Ioc
{
    /// <summary>
    /// lifetime of service instance
    /// </summary>
    public enum ServiceScope
    {
        /// <summary>
        /// only one instance in whole application lifetime
        /// </summary>
        Singleton,
        /// <summary>
        /// every call creates a new instance <see cref="IServiceProvider.GetService(Type)"/>
        /// </summary>
        Request,
        /// <summary>
        /// clone from existing instance
        /// </summary>
        ProtoType,
    }
}
