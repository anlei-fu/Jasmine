using Jasmine.Common;
using Jasmine.Reflection.Interfaces;
using System;

namespace Jasmine.Ioc
{
    public class ServiceMetaData:INameFearture ,ITypeFearture
    {

        public ServiceMetaData(Type serviceType)
        {
            RelatedType = serviceType;
        }
        public ITypeCache Cache { get; }
        public Type ImplType { get; set; }
        public string Name { get; internal set; }
        public ServiceScope Scope { get; internal set; }
        public string TypeName => RelatedType.Name;
        public string TypeFullName => RelatedType.FullName;
        public Type RelatedType { get; set; }
        public bool LazyLoad { get; internal set; }
        public Func<object,object[],object> InitMethod { get;internal set; }
        public Func<object, object[], object> DestroyMethod { get; internal set; }
        public ConstructorMetaData ConstrctorMetaData { get; internal set; }
        public PropertyMetaData[] Properties { get; set; }
      
    }
}
