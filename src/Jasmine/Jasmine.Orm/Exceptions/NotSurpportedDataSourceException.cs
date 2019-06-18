using System;

namespace Jasmine.Orm.Exceptions
{
    public class NotSurpportedDataSourceException:Exception
    {
        public NotSurpportedDataSourceException(DataSource type):base($" serve {type} is not surpported!")
        {

        }
    }
}
