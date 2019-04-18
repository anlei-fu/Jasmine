using Jasmine.Common;
using Jasmine.Reflection;
using System;

namespace Jasmine.Ioc
{
    public class IocServiceMetaData:ServiceMetaDataBase
    {

        public IocServiceMetaData(Type serviceType)
        {
            RelatedType = serviceType;
        }
        public ITypeCache Cache { get; }
        public Type ImplType { get; set; }
        public ServiceScope Scope { get; internal set; }
        public string TypeName => RelatedType.Name;
        public string TypeFullName => RelatedType.FullName;
        public bool LazyLoad { get; internal set; }
        public Func<object,object[],object> InitMethod { get;internal set; }
        public Func<object, object[], object> DestroyMethod { get; internal set; }
        public IocConstructorMetaData ConstrctorMetaData { get; internal set; }
        public IocPropertyMetaData[] Properties { get; set; }
      
    }
}
