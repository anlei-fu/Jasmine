using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public  class DataSourceAttribute:Attribute
    {
        public DataSourceAttribute(DataSourceType dataSource)
        {
            DataSource = dataSource;
        }
        public DataSourceType DataSource { get; }
    }
}
