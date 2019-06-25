using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Serialization;
using System;
using System.Collections.Generic;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestMetaData:IAop
    {
        public SerializeMode SerializeMode { get; set; }
        public Method Method { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public RpcRequestParameterMetaData[] Parameters { get; internal set; }

        public List<Type> BeforeInterceptors { get; } = new List<Type>();

        public List<Type> AfterInterceptors { get; } = new List<Type>();

        public List<Type> AroundInterceptors { get; } = new List<Type>();

        public List<Type> ErrorInterceptors { get; } = new List<Type>();
    }
}
