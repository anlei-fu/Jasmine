using Jasmine.Common;
using System;

namespace Jasmine.Ioc
{
    public class IocPropertyMetaData:ServiceMetaDataBase
    {
       public bool AutoWired { get; internal set; }
        public object DefaultValue { get; internal set; }
        public Type Impl { get; internal set; }
        public string ConfigKey { get; internal set; }
    }
}
