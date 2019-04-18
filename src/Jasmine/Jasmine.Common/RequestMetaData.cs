using Jasmine.Reflection.Models;
using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class RequestMetaData
    {
        public Type ServiceType { get; set; }
        public Type ImplementType { get; set; }
        public Method Method { get; set; }
        public IList<string> BeforeFilters { get; set; }
        public IList<string> AfterFilters { get; set; }
        public IList<string> AroundFilters { get; set; }
        public IList<string> ErrorFilters { get; set; }
      

    }
}
