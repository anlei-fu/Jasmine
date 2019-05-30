using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public  class JoinTableAttribute:Attribute
    {
    }
}
