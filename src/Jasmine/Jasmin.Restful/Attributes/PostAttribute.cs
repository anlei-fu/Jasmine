using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property,Inherited =true,AllowMultiple =false)]
    public  class PostAttribute:Attribute
    {
    }
}
