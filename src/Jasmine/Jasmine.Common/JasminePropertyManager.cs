using System.Collections;
using System.Collections.Generic;

namespace Jasmine.Common
{
    public class JasminePropertyManager:IPropertyManager
    {
        public string Name { get; set; }
        public IDictionary<string, string> _properties { get; set; } = new Dictionary<string, string>();

        public int Count => _properties.Count;

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            foreach (var item in _properties)
            {
                yield return item;
            }
        }

        public string GetValue(string name)
        {
            return _properties.TryGetValue(name, out var value) ? value : null;
        }

        public void SetValue(string key,string value)
        {
            if (!_properties.ContainsKey(key))
                _properties.Add(key, value);
            else
                _properties[key] = value;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.GetEnumerator();
        }
    }
}
