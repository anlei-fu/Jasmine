using Jasmine.Common;
using Jasmine.Reflection;
using Jasmine.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Jasmine.Restful
{
    public class RestfulRequestMetaData : IAop
    {
        public string Description { get; set; }
        public int MaxConcurrency { get; set; } = 1000;
        public string AlternativeService { get; set; }
        public SerializeMode SerializeMode { get; set; }
        [JsonIgnore]
        public Method Method { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        [JsonIgnore]
        public RestfulRequestParameterMetaData[] Parameters { get; internal set; }

        public string ExampleParameter { get; set; }
        public IList<Type> BeforeFilters { get; } = new List<Type>();

        public IList<Type> AfterFilters { get; } = new List<Type>();

        public IList<Type> AroundFilters { get; } = new List<Type>();

        public IList<Type> ErrorFilters { get; } = new List<Type>();
    }
}
