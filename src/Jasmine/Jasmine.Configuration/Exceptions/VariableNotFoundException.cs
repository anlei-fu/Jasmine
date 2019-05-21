using System;

namespace Jasmine.Configuration.Exceptions
{
    public class VariableNotFoundException:Exception
    {
        public VariableNotFoundException(string name,string property):base($"varible not found in property \"{property}\"")
        {

        }
    }
}
