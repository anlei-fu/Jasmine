using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Restful.Exceptions
{
   public class FilterNotFoundException:Exception
    {
        public FilterNotFoundException(string msg):base(msg)
        {

        }
    }
}
