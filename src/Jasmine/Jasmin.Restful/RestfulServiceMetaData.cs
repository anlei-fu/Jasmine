﻿using Jasmine.Common;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public  class RestfulServiceMetaData:AopServiceMetaData
    {
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public IDictionary<string, RestfulRequestMetaData> Requests { get; set; }
    }
}