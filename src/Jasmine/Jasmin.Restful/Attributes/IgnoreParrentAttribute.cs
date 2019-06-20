using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class IgnoreParentAttribute:Attribute
    {
    }
}
