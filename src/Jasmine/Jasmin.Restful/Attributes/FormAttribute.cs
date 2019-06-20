using System;

namespace Jasmine.Restful.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter,AllowMultiple =false)]
    public class FormAttribute:Attribute
    {
        public FormAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
