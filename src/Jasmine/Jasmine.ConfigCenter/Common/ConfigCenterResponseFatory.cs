using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.ConfigCenter.Common
{
   public class ConfigCenterResponseFatory
    {
        private const string SERVICE_NOT_FOUND = "service not found!";
        public static ConfigCenterServiceResponse CreateServiceNotFound(long requestId)
        {
            return new ConfigCenterServiceResponse()
            {
                ResponseId=requestId,
                Message=SERVICE_NOT_FOUND,
                ResponseCode=ResponseCode.ServiceNotFound

            };
        }
        public static ConfigCenterServiceResponse CreateParameterIncorrectResponse(long requestId,string msg)
        {
            return new ConfigCenterServiceResponse()
            {

            };
        }
    }
}
