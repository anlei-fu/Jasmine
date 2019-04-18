using Jasmine.ConfigCenter.Common;
using System;

namespace Jasmine.ConfigCenter.Client
{
    public class JasmineConfigCenterClient : AbstractConfigCenterClient
    {
        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override bool Connect(string host, string user, string id)
        {
            throw new NotImplementedException();
        }

        protected override ConfigCenterServiceResponse call(ConfigCenterServiceRequest request)
        {
            throw new NotImplementedException();
        }

       
    }
}
