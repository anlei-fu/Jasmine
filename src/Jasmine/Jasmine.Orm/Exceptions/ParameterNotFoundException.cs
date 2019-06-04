using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Orm.Exceptions
{
 public   class ParameterNotFoundException:Exception
    {
        public ParameterNotFoundException(string msg):base(msg)
        {
           
        }
        
    }
}
