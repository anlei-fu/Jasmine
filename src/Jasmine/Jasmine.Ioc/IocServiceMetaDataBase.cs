using Jasmine.Common;
using System;

namespace Jasmine.Ioc
{
    public class IocServiceMetaDataBase:ServiceMetaDataBase
    {
       
        public Type Impl { get; internal set; }
        public string ConfigKey { get; internal set; }
        public bool IsFromConfig => ConfigKey != null;
        public bool HasDefaultValue => DefaultValue != null;
        public bool HasImplementType => Impl != null;
        public object DefaultValue { get; internal set; }
      
    }
}
