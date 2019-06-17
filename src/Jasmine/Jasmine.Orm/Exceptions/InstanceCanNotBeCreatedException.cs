using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Orm.Exceptions
{
   public class InstanceCanNotBeCreatedException:Exception
    {
        public InstanceCanNotBeCreatedException(string msg):base(msg)
        {

        }
    }
}
