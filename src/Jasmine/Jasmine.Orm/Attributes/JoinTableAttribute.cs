﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasmine.Orm.Attributes
{
  public  class JoinTableAttribute:Attribute
    {
        public string ForeignKey { get; }
    }
}
