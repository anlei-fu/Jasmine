using System;

namespace Jasmine.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public abstract  class InterceptorAttribute:Attribute
    {
        public InterceptorAttribute(Type type)
        {
            FilterType = type;
        }
        public  Type FilterType { get; }
    }
}
