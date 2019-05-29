using System;

namespace Jasmine.Common.Attributes
{
    public  class MaxConcurrencyAttribute:Attribute
    {
        public MaxConcurrencyAttribute(int concurrency)
        {
            MaxConcurrency = MaxConcurrency;
        }
        public int MaxConcurrency { get; }
    }
}
