using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public  class DataSourceAttribute:Attribute
    {
        public DataSourceAttribute(DataSource dataSource)
        {
            DataSource = dataSource;
        }
        public DataSource DataSource { get; }
    }
}
