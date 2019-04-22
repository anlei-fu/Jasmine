using System.Collections.Generic;
using Jasmine.Common;
using Jasmine.Reflection;

namespace Jasmine.Restful
{
    public   class RestfulRequestMetaData:IAop
    {
        public Method Method { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public RestfulRequestParameterMetaData[] Parameters { get; internal set; }

        public IList<string> BeforeFilters { get; }

        public IList<string> AfterFilters { get; }

        public IList<string> AroundFilters { get; }

        public IList<string> ErrorFilters { get; }
    }
}
