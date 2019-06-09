using Jasmine.Orm.Model;
using System;

namespace Jasmine.Orm.Exceptions
{
    public class NotSurpportedDataSourceException:Exception
    {
        public NotSurpportedDataSourceException(DataSourceType type):base($" serve {type} is not surpported!")
        {

        }
    }
}
