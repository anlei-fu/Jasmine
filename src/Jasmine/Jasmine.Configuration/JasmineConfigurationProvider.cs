using System;
using System.Collections.Concurrent;

namespace Jasmine.Configuration
{
    public class JasmineConfigurationProvider : IConfigrationProvider
    {
        private JasmineConfigurationProvider()
        {

        }

        private ConcurrentDictionary<string, IConfigGroup> _groups = new ConcurrentDictionary<string, IConfigGroup>();

        public static readonly JasmineConfigurationProvider Instance = new JasmineConfigurationProvider();

        public T GetConfig<T>(string config)
        {
            return (T)GetConfig(typeof(T), config);
        }
        public object GetConfig(Type type, string config)
        {
            return JasmineConfigStringConvertor.Convert(type, GetConfig(config));
        }
        public string GetConfig(string config)
        {
            return new PropertyNodeParser().Parse(config).GetValue(this);
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
