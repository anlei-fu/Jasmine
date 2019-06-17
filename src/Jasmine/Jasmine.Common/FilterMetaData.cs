using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class FilterMetaData : INameFearture,ITypeFearture
    {
        public string Name { get; set; }
        public Type RelatedType { get; set; }

        public bool HasBeforeFilters => BeforeFilters.Count!= 0;
        public bool HasAfterFilters => AfterFilters.Count != 0;
        public bool HasAroundFilters => AroundFilters.Count != 0;
        public List<FilterMetaData> BeforeFilters { get; set; } = new List<FilterMetaData>();
        public List<FilterMetaData> AfterFilters { get; set; } = new List<FilterMetaData>();
        public List<FilterMetaData> AroundFilters { get; set; } = new List<FilterMetaData>();

       
    }
}
