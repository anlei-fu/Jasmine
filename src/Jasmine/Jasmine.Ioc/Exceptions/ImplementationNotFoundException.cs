using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Ioc.Exceptions
{
  public  class ImplementationNotFoundException:Exception
    {
        public ImplementationNotFoundException(string msg):base(msg)
        {

        }
    }
}
