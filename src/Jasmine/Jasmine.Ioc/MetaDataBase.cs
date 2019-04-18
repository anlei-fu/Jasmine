using Jasmine.Common;
using System;

namespace Jasmine.Ioc
{
    public class MetaDataBase:INameFearture,ITypeFearture
    {
        public string Name { get;internal set; }
        public bool IsAbstract => ImplType != null ||RelatedType.IsAbstract;
        public Type ImplType { get; internal set; }
        public Type RelatedType { get; internal set; }
        public string PropertyKey { get; internal set; }
        public bool IsFromConfig => PropertyKey != null;
        public bool HasDefaultValue => DefaultValue != null;
        public bool HasImplementType => ImplType != null;
        public object DefaultValue { get; internal set; }
      
    }
}
