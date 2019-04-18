using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class ServiceMetaDataBase : INameFearture, ITypeFearture
    {
        public string Name { get; set; }
        public Type RelatedType { get; set; }
        public string Path { get; set; }
        public IList<string> BeforeFilters { get; set; }
        public IList<string> AfterFilters { get; set; }
        public IList<string> AroundFilters { get; set; }
        public IList<string> ErrorFilters { get; set; }
    }

        
}
