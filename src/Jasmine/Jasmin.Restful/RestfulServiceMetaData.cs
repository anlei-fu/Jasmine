﻿using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Serialization;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public   class RestfulServiceMetaData:IAop
    {
        public SerializeMode SerializeMode { get; set; }
        public Method Method { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public RestfulRequestParameterMetaData[] Parameters { get; internal set; }

        public IList<string> BeforeFilters { get; } = new List<string>();

        public IList<string> AfterFilters { get; } = new List<string>();

        public IList<string> AroundFilters { get; } = new List<string>();

        public IList<string> ErrorFilters { get; } = new List<string>();
    }
}
