﻿using System.Collections.Concurrent;

namespace Jasmine.Configuration
{
    public class JasmineConfigurationProvider : IConfigrationProvider
    {
        private ConcurrentDictionary<string, IConfigGroup> _groups = new ConcurrentDictionary<string, IConfigGroup>();
        public string GetConfig(string parameter)
        {
            return new PropertyNodeParser().Parse(parameter).GetValue(this);
        }

      
        public void LoadConfig(string path)
        {
            var groups = new ConfigurationXmlResolver().Resolve(path);

            foreach (var item in groups)
            {
                _groups.TryAdd(item.Name, item);
            }
        }

        public IConfigGroup GetGroup(string name)
        {
            return _groups.TryGetValue(name, out var result) ? result : null;
        }
    }
}
