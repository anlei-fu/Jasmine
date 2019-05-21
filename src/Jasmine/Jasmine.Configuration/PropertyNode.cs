using Jasmine.Configuration.Exceptions;
using System.Collections.Generic;

namespace Jasmine.Configuration
{
    public  class PropertyNode
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Dictionary<string,PropertyNode> Paramters { get; set; }
        public bool IsProperty { get; set; }

        public string GetValue(JasmineConfigurationProvider provider)
        {

            if (IsProperty)
            {
                var index = Name.IndexOf(".");

                if (index == -1)
                    throw new ConfigGroupNotFoundException(Name);

                var group = provider.GetGroup(Name.Substring(0, index));

                if (group == null)
                    throw new ConfigGroupNotFoundException(Name.Substring(0, index));


                var property = group.GetProperty(Name.Substring(index + 1, Name.Length - index - 1));

                if (property == null)
                    throw new PropertyNotFoundException(group.Name, Name);

                var parameters = new Dictionary<string, string>();

                if (Paramters != null)
                {
                    foreach (var item in Paramters)
                    {
                        parameters.Add(item.Key, item.Value.GetValue(provider));
                    }
                }

                return property.GetValue(provider, parameters);
            }
            else
            {
                return Value;
            }

        }
    }
}
