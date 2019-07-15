using Jasmine.Reflection.Implements;
using System;

namespace Jasmine.Reflection
{
    public class TypeMetaData:AttributeFearture
    {
        public string Name => DeclareType.Name;
        public string FullName => DeclareType.FullName;
        public bool IsAbstract=> DeclareType.IsAbstract;
        public bool IsPublic => DeclareType.IsPublic;
        public bool IsGeneric => DeclareType.IsGenericType;
        public Type[] GenericTypes { get; set; }
        public Type DeclareType { get; set; }
        public IFieldCache Fileds { get; set; } = new DefaultFieldCache();
        public IMethodCache Methods { get; set; } = new DefaultMethodCache();
        public IPropertyCache Properties { get; set; } = new DefaultPropertyCache();
        public IConstructorCache Constructors { get; set; } = new DefaultConstructorCache();
    }
}
