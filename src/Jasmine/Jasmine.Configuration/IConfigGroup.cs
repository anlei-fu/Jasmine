using Jasmine.Common;
using System.Collections.Generic;

namespace Jasmine.Configuration
{
    public interface IConfigGroup:INameFearture,IReadOnlyCollection<Property>
    {
        Property GetProperty(string name);
        void AddProperty(Property property);
        bool ContainsProperty(string name);
        void RemoveProperty(string name);

    }
}
