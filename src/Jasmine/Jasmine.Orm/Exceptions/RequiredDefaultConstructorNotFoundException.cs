using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Orm.Exceptions
{
  public  class RequiredDefaultConstructorNotFoundException:Exception
    {
        public RequiredDefaultConstructorNotFoundException(Type type):base($"required none-parameter constructor of {type} not found!")
        {

        }
    }
}
