using System;
using System.Collections.Generic;
using System.Text;

namespace Jasmine.Restful.Attributes
{
   public class FormAttribute:Attribute
    {
        public FormAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
