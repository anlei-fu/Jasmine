using System;
using System.Collections.Generic;

namespace Jasmine.Rpc
{
    public   class RpcFilterContext
    {
        public RpcContext RpcContext { get; set; }
        public IDictionary<string, object> Datas { get; set; } = new Dictionary<string, object>();
        public Type ReturnType { get; set; }
        public Exception Error { get; set; }
        public object ReturnValue { get; set; }
        public void Init(RpcContext context)
        {
            Datas.Clear();
            Error = null;
            ReturnType = null;
            ReturnValue = null;
            RpcContext = context;
        }
    }
}
