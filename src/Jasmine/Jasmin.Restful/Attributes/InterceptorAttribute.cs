using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class,Inherited =true,AllowMultiple =true)]
    public abstract class InterceptorAttribute:Attribute
    {
        public InterceptorAttribute(string interceptorName)
        {
            IntereptorName = interceptorName;
        }
        public string IntereptorName { get; }
    }
}
