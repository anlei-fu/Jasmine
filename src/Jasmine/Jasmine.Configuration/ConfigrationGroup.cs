using Jasmine.Configuration.Exceptions;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jasmine.Configuration
{
    public class ConfigrationGroup : IConfigGroup
    {
        public ConfigrationGroup(string name)
        {
            Name = name;
        }
        private ConcurrentDictionary<string, Property> _properties = new ConcurrentDictionary<string, Property>();
        public string Name { get; }

        public int Count => _properties.Count;

        public void AddProperty(Property property)
        {
            if (!_properties.TryAdd(property.Name, property))
            {
                // same key  exists
            }
        }

        public IEnumerator<Property> GetEnumerator()
        {
            foreach (var item in _properties.Values)
            {
                yield return item;
            }
        }

        public Property GetProperty(string name)
        {
            return _properties.TryGetValue(name, out var result) ? result : null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _properties.Values.GetEnumerator();
        }

        public bool ContainsProperty(string name)
        {
            return _properties.ContainsKey(name);
        }

        public void RemoveProperty(string name)
        {
            _properties.TryRemove(name, out var _);
        }
    }
}
