﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Orm.Exceptions
{
  public  class BadJoinColumnException:Exception
    {
        public BadJoinColumnException(Type type):base($" basic type {type} can not be a joincolumns!  ")
        {

        }
    }
}
