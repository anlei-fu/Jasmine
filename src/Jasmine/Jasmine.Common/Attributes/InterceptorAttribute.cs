using System;

namespace Jasmine.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public abstract  class InterceptorAttribute:Attribute,INameFearture
    {
        public InterceptorAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
