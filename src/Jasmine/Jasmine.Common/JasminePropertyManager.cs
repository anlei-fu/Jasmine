using Jasmine.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class JasminePropertyManager:IPropertyManager
    {
        public string Name { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, JasminePropertyManager> SubManagers { get; set; } = new Dictionary<string, JasminePropertyManager>();

        public string GetValue(string name)
        {
            if (name == null)
                throw new ArgumentNullException();

            name = name.Trim();

            var properties = name.Split('.');

            if (properties.Length == 0)
                throw new PropertyNotFoundException(name);

            if (properties.Length == 1)
            {
                return Properties.ContainsKey(properties[0]) ? Properties[properties[0]] :
                                                                                       throw new PropertyNotFoundException(name);
            }

            var manager = this;

            for (int i = 0; i < properties.Length-1; i++)
            {
                if(!manager.SubManagers.ContainsKey(properties[i]))
                    throw new PropertyNotFoundException(name);

                manager = manager.SubManagers[properties[i]];
            }

            return manager.Properties.ContainsKey(properties[properties.Length - 1]) ? manager.Properties[properties[properties.Length - 1]]
                                                                                     : throw new PropertyNotFoundException(name);
        }

        public void SetValue(string key,string value)
        {
            var properties = key.Split('.');

            if(properties.Length==0)
                throw new PropertyNotFoundException(key);

            if (properties.Length == 1)
                 Properties[properties[0]]=value;

            var manager = this;

            for (int i = 0; i < properties.Length - 1; i++)
            {
                if (!manager.SubManagers.ContainsKey(properties[i]))
                    throw new PropertyNotFoundException(key);

                manager = manager.SubManagers[properties[i]];
            }

           manager.Properties[properties[properties.Length - 1]]=value;

        }
    }
}
