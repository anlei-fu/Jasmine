using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class AopServiceMetaData:ServiceMetaDataBase
    {
        public IList<Type> BeforeFilters { get; set; }
        public IList<Type> AfterFilters { get; set; }
        public IList<Type> AroundFilters { get; set; }
        public IList<Type> ErrorFilters { get; set; }
    }
}
