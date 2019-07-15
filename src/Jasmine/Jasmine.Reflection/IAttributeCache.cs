using System;
using System.Collections.Generic;

namespace Jasmine.Reflection
{
    /// <summary>
    /// cache  attributes  mark on  related type
    /// </summary>
    public   interface IAttributeCache:IReadOnlyCollection<Attribute[]>
    {
        /// <summary>
        /// get attr by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Attribute[] GetAttribute(string name);
        /// <summary>
        /// contains attr by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Contains(string name);
        /// <summary>
        /// contains attr by type
        /// </summary>
        /// <param name="attrType"></param>
        /// <returns></returns>
        bool Contains(Type attrType);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : Attribute;
        /// <summary>
        /// get attr by type
        /// </summary>
        /// <param name="attrType"></param>
        /// <returns></returns>
        Attribute[] GetAttribute(Type attrType);
        T[] GetAttribute<T>() where T : Attribute;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attr"></param>
        void Cache(Attribute attr);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        bool ContainsAny(params Type[] attrs);

    }
}
