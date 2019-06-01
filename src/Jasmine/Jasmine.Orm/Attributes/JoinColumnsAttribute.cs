using System;

namespace Jasmine.Orm.Attributes
{
    /// <summary>
    /// use join quey or insert 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class JoinColumnsAttribute:OrmAttribute
    {

    }
}
