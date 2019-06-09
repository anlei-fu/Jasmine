using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Orm.Exceptions
{
   public class ColumnNotExistsException:Exception
    {
        public ColumnNotExistsException(string msg):base(msg)
        {

        }
    }
}
