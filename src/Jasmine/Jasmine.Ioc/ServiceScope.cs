using System;
namespace Jasmine.Ioc
{
    /// <summary>
    /// lifetime of service instance
    /// </summary>
    public enum ServiceScope
    {
        /// <summary>
        /// only instance in of application lifetime
        /// </summary>
        Singleton,
        /// <summary>
        /// every call create a new instance <see cref="IServiceProvider.GetService(Type)"/>
        /// </summary>
        Request,
        /// <summary>
        /// clone from singleton instance
        /// </summary>
        ProtoType,
    }
}
