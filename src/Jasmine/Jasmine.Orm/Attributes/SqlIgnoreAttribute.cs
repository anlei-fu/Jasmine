using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class SqlIgnoreAttribute:Attribute
    {
    }
}
