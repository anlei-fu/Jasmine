using System.Collections.Generic;

namespace Jasmine.Common
{
    public  class AopServiceMetaData:ServiceMetaDataBase
    {
        public IList<string> BeforeFilters { get; set; }
        public IList<string> AfterFilters { get; set; }
        public IList<string> AroundFilters { get; set; }
        public IList<string> ErrorFilters { get; set; }
    }
}
