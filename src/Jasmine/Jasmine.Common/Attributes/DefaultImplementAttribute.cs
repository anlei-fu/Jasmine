using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Common.Attributes
{
   public class DefaultImplementAttribute:Attribute
    {
        public DefaultImplementAttribute(Type type)
        {
            Impl = type;
        }
        public Type Impl { get; set; }
    }
}
