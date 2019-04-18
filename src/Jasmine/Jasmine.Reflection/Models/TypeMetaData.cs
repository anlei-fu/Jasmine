using Jasmine.Reflection.Implements;
using Jasmine.Reflection.Interfaces;
using System;

namespace Jasmine.Reflection.Models
{
    public class TypeMetaData:AttributeSurpport
    {
        public string Name => DeclareType.Name;
        public string FullName => DeclareType.FullName;
        public Type DeclareType { get; set; }
        public IFieldCache Fileds { get; set; } = new DefaultFieldCache();
        public IMethodCache Methods { get; set; } = new DefaultMethodCache();
        public IPropertyCache Properties { get; set; } = new DefaultPropertyCache();
        public IConstructorCache Constructors { get; set; } = new DefaultConstructorCache();
    }
}
