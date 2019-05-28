using Jasmine.Common;

namespace Jasmine.Rpc.Server
{
    public class RpcRequestParameterMetaData: ParameterMetaDataBase
    {
        public bool FromData => DataKey != null;
        public bool FromBody { get; set; }
        public bool FromQueryString => QueryStringKey != null;
        public string QueryStringKey { get; set; }
        public string DataKey { get; set; }
       
    }
}
