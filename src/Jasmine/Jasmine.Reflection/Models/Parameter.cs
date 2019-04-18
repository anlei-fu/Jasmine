using System;

namespace Jasmine.Reflection.Models
{
    public   class Parameter:AttributeSurpport
    {
        public Type ParameterType { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
