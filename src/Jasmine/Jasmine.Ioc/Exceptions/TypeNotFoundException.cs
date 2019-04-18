using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Ioc.Exceptions
{
  public  class TypeNotFoundException:Exception
    {
        public TypeNotFoundException(string msg):base(msg)
        {

        }
    }
}
