using Jasmine.Common;
using System;

namespace Jasmine.Ioc
{
    public class IocServiceMetaDataBase:ServiceMetaDataBase
    {
        public bool IsAbstract => ImplType != null ||RelatedType.IsAbstract;
        public Type ImplType { get; internal set; }
        public string PropertyKey { get; internal set; }
        public bool IsFromConfig => PropertyKey != null;
        public bool HasDefaultValue => DefaultValue != null;
        public bool HasImplementType => ImplType != null;
        public object DefaultValue { get; internal set; }
      
    }
}
