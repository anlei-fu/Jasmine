using System;
using System.Reflection;

namespace Jasmine.Reflection
{
    public   class Parameter:AttributeFearture
    {
        public ParameterInfo ParameterInfo { get; set; }
        public Type ParameterType { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }
}
