using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class AopServiceMetaData:ServiceMetaDataBase
    {
        public IList<Type> BeforeFilters { get; set; } = new List<Type>();
        public IList<Type> AfterFilters { get; set; } = new List<Type>();
        public IList<Type> AroundFilters { get; set; } = new List<Type>();
        public IList<Type> ErrorFilters { get; set; } = new List<Type>();
    }
}
