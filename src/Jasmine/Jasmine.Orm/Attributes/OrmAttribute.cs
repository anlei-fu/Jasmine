using System;

namespace Jasmine.Orm.Attributes
{ 
    /// <summary>
    /// use to imply attribute belong to orm
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public  abstract class OrmAttribute:Attribute
    {
    }
}
