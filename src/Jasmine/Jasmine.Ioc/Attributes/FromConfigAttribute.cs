using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Ioc.Attributes
{
  public  class FromConfigAttribute:Attribute
    {
        public FromConfigAttribute(string key)
        {
            PropertyName = key;
        }
        public string PropertyName{ get; }
    }
}
