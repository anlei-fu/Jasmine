using Jasmine.Configuration.Exceptions;
using System.Collections.Generic;

namespace Jasmine.Configuration
{
    public  class PropertyTemplate
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Dictionary<string,PropertyTemplate> SubTemplates { get; set; }
        public bool IsPropertyTemplates { get; set; }

        public string GetValue(JasmineConfigurationProvider provider)
        {

            if (IsPropertyTemplates)
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

                if (SubTemplates != null)
                {
                    foreach (var item in SubTemplates)
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
