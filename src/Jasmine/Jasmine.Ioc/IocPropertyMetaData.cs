using Jasmine.Common;
using System;

namespace Jasmine.Ioc
{
    public class IocPropertyMetaData:ServiceMetaDataBase
    {
        public object DefaultValue { get; internal set; }
        public Type Impl { get; internal set; }
        public bool HasImpl => Impl != null;
        public bool FromConfig => ConfigKey != null;
        public bool HasDefaultValue => DefaultValue != null;
        public string ConfigKey { get; internal set; }
        public Action<object, object> Setter { get; internal set; }
    }
}
